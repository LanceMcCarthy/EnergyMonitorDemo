﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using EnergyMonitor.Client.Models;
using MQTTnet;
using MQTTnet.Client;
using Telerik.Blazor.Components;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    private IMqttClient? mqttClient;
    private TelerikChart DashboardChartRef { get; set; } = null!;
    private ObservableCollection<ChartMqttDataItem> SolarPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> LoadPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> BatteryPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> GridPowerData { get; } = new();
    private double ArcGaugeValue { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBattery { get; set; } = "0";
    private string CurrentGrid { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "...";

    protected override async Task OnInitializedAsync()
    {
        // Get the required MQTT server values from environment
        var host = Configuration["MQTT_HOST"];
        var port = Configuration["MQTT_PORT"];

        if (string.IsNullOrEmpty(host))
        {
            throw new ArgumentNullException("MQTT_HOST", "A value for the MQTT_HOST environment variable must be set before starting the application.");
        }

        if (!int.TryParse(port, out var portNumber))
        {
            Trace.WriteLine(
                "[WARNING] - The value for MQTT_PORT is invalid or empty, double check this is intended. If your MQTT server operates on a specific port, you need to set the MQTT_PORT environment variable.",
                "Energy Monitor");
        }

        // Let's get our feet wet!
        await SetupMqtt(host, portNumber);
    }

    private async Task SetupMqtt(string host, int port)
    {
        var mqttFactory = new MqttFactory();
        mqttClient = mqttFactory.CreateMqttClient();

        // ***** Operation 1 - Connect to MQTT server ***** //

        var clientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(host, port)
            .Build();

        //    1.a - Subscribe to message received event BEFORE connecting
        mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;

        //    1.b - Connect to the MQTT server
        await mqttClient.ConnectAsync(clientOptions, CancellationToken.None);


        // ***** Operation 2 - Subscribe to a topic ***** //

        //    2.a - Filter out the messages to only the topics we want.
        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(f => { f.WithTopic("solar_assistant/#"); })
            .Build();

        //    2.b - Subscribe to the server for those topics
        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);


        Trace.WriteLine("MqttFactory successfully started!", "Energy Monitor");
    }

    private Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        // ****** Processing a Message ****** //

        // **  Step 1 - Get the topic and payload data out of the message

        // Read the topic string. I return a strong type that we can easily work with it instead of crazy strings.
        var messageTopic = TopicNameHelper.GetTopicName(e.ApplicationMessage.Topic);

        // Read the payload. Important! It is in the form of an ArraySegment<byte>, so we need to convert to byte[], then to ASCII.
        var decodedPayload = Encoding.ASCII.GetString(e.ApplicationMessage.PayloadSegment.ToArray());


        // **  Step 2 - Now we can do something with that data.

        // Update the relevant UI element, collection, etc
        switch (messageTopic)
        {
            case TopicName.DeviceMode_Inverter1:
                CurrentInverterMode = decodedPayload;
                break;

            case TopicName.LoadPower_Inverter1:
                var loadCurrent = Convert.ToDouble(decodedPayload);
                CurrentLoad = $"{loadCurrent}";
                LoadPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = loadCurrent, Timestamp = DateTime.Now });
                if (LoadPowerData.Count > 100) LoadPowerData.RemoveAt(0);
                break;

            case TopicName.PvPower_Inverter1:
                var pvCurrent = Convert.ToDouble(decodedPayload);
                ArcGaugeValue = pvCurrent;
                CurrentSolar = $"{pvCurrent}";
                SolarPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = pvCurrent, Timestamp = DateTime.Now });
                if (SolarPowerData.Count > 100) SolarPowerData.RemoveAt(0);
                break;

            case TopicName.BatteryPower_Total:
                var batteryPower = Convert.ToDouble(decodedPayload);
                CurrentBattery = $"{batteryPower}";
                BatteryPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = batteryPower, Timestamp = DateTime.Now });
                if (BatteryPowerData.Count > 100) SolarPowerData.RemoveAt(0);
                break;

            case TopicName.GridPower_Inverter1:
                var gridPower = Convert.ToDouble(decodedPayload);
                CurrentGrid = $"{gridPower}";
                GridPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = gridPower, Timestamp = DateTime.Now });
                if (GridPowerData.Count > 100) GridPowerData.RemoveAt(0);
                break;

            case TopicName.BatteryStateOfCharge_Total:
            case TopicName.BusVoltage_Total:
            case TopicName.GridFrequency_Inverter1:
            case TopicName.PvCurrent1_Inverter1:
            case TopicName.BatteryVoltage_Inverter1:
            case TopicName.LoadApparentPower_Inverter1:
            case TopicName.PvCurrent2_Inverter1:
            case TopicName.Temperature_Inverter1:
            case TopicName.LoadPercentage_Inverter1:
            case TopicName.BatteryCurrent_Inverter1:
            case TopicName.PvVoltage1_Inverter1:
            case TopicName.PvVoltage2_Inverter1:
            case TopicName.PvPower1_Inverter1:
            case TopicName.GridVoltage_Inverter1:
            case TopicName.AcOutputFrequency_Inverter1:
            case TopicName.AcOutputVoltage_Inverter1:
            case TopicName.PvPower2_Inverter1:
                break;
            default:
                break;
        }

        // TODO - ED - Do we really need to update the entire UI just for one value change?
        // Why can't we just update individual elements in the switch statement?
        InvokeAsync(StateHasChanged);

        return Task.CompletedTask;
    }

    private void ItemResize()
    {
        StateHasChanged();
        DashboardChartRef.Refresh();
    }
}