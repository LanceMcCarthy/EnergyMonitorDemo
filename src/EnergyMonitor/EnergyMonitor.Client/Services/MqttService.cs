using EnergyMonitor.Client.Models;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using static EnergyMonitor.Client.Models.MessageUtilities;

namespace EnergyMonitor.Client.Services;

public class MqttService(IConfiguration config, IServiceProvider serviceProvider) : BackgroundService, IAsyncDisposable
{
    // Service setup
    private MqttFactory? mqttFactory;
    private IMqttClient? mqttClient;
    private readonly string mqttHost = config["MQTT_HOST"] ?? throw new NullReferenceException("A value for the MQTT_HOST environment variable must be set before starting the application.");
    private readonly string mqttPort = config["MQTT_PORT"] ?? string.Empty;

    public delegate Task SubscribedChanged();
    public event SubscribedChanged? SubscriptionChanged;

    // LIVE Values
    public bool IsSubscribed { get; set; }

    // This is required in order to get the scoped DbService in a BackgroundService (we cannot inject it in the CTOR because it is scoped)
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        this.mqttFactory = new MqttFactory();
        this.mqttClient = mqttFactory?.CreateMqttClient();

        if(mqttClient == null)
            throw new NullReferenceException("The MQTT client could not be created.");

        // Add event handler for when a message comes in
        mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;

        // Connect to the MQTT server
        var port = int.TryParse(mqttPort, out var portNumber) ? portNumber : 1883;
        var clientOptions = new MqttClientOptionsBuilder().WithTcpServer(mqttHost, port).Build();
        await mqttClient!.ConnectAsync(clientOptions, CancellationToken.None);

        // start listening for messages
        await mqttClient.SubscribeAsync(
            mqttFactory?.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("solar_assistant/#"); })
                .Build(), 
            CancellationToken.None);

        IsSubscribed = true;
        SubscriptionChanged?.Invoke();
    }

    // On every message, we update the live values and add the item to the database
    private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        // Read the payload. ArraySegment<byte>, so we need to convert to byte[], then to ASCII.
        var decodedPayload = e.ApplicationMessage.PayloadSegment.GetTopicValue();

        // Update live values
        //await ProcessDataItem(decodedPayload, GetTopicName(e.ApplicationMessage.Topic));

        // Create a scope to access the DbService and add item to the DB
        using var scope = serviceProvider.CreateScope();
        var dbService = scope.ServiceProvider.GetRequiredService<MessagesDbService>();
        await dbService.AddMeasurementAsync(new MqttDataItem
        {
            Topic = e.ApplicationMessage.Topic, 
            Value = decodedPayload, 
            Timestamp = DateTime.Now
        });
    }

    // This is called by IHostedService
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (IsSubscribed)
        {
            // Unsubscribe, be nice to the broker
            var options = mqttFactory?.CreateUnsubscribeOptionsBuilder()
                .WithTopicFilter(new MqttTopicFilter { Topic = "solar_assistant/#" })
                .Build();

            await mqttClient!.UnsubscribeAsync(options, CancellationToken.None);

            IsSubscribed = false;
            SubscriptionChanged?.Invoke();
        }
    }

    // Be a good dev
    public async ValueTask DisposeAsync()
    {
        await StopAsync(CancellationToken.None);

        mqttClient?.Dispose();
    }
}
