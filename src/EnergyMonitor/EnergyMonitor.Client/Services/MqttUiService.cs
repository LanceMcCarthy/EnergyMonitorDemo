﻿using System.Diagnostics;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;

namespace EnergyMonitor.Client.Services
{
    public class MqttUiService(IConfiguration config)
    {
        private MqttFactory? mqttFactory;
        private IMqttClient? mqttClient;

        private readonly string mqttHost = config["MQTT_HOST"] ?? throw new NullReferenceException("A value for the MQTT_HOST environment variable must be set before starting the application.");
        
        private readonly string mqttPort = config["MQTT_PORT"] ?? string.Empty;

        public async Task SetupMqtt(Func<MqttApplicationMessageReceivedEventArgs,Task> messageReceivedDelegate)
        {
            mqttFactory = new MqttFactory();
            mqttClient = mqttFactory.CreateMqttClient();

            // Read the port number. If it's missing or invalid, use the default MQTT port (1883).
            var port = int.TryParse(mqttPort, out var portNumber) 
                ? portNumber 
                : UseDefaultWithWarning();

            // Connect to MQTT server
            var clientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttHost, port)
                .Build();

            // Subscribe to message received event BEFORE connecting
            mqttClient.ApplicationMessageReceivedAsync += messageReceivedDelegate;

            // Connect to the MQTT server
            await mqttClient.ConnectAsync(clientOptions, CancellationToken.None);

            // subscribe to topic (important, this is separated into additional logic so it can be turned on and off)
            await SubscribeAsync();

            Trace.WriteLine("MqttFactory successfully started!", "Energy Monitor");
        }

        public async Task SubscribeAsync()
        {
            // Filter out the messages to only the topics we want.
            var mqttSubscribeOptions = mqttFactory?.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("solar_assistant/#"); })
                .Build();

            // Subscribe with the options
            if (mqttClient is not null && mqttSubscribeOptions is not null)
            {
                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            }
        }

        public async Task UnsubscribeAsync()
        {
            var mqttUnsubscribeOptions = mqttFactory?.CreateUnsubscribeOptionsBuilder()
                .WithTopicFilter(new MqttTopicFilter { Topic = "solar_assistant/#" })
                .Build();
            if (mqttClient is not null && mqttUnsubscribeOptions is not null)
            {
                await mqttClient.UnsubscribeAsync(mqttUnsubscribeOptions, CancellationToken.None);
            }
        }




        private static int UseDefaultWithWarning()
        {
            Trace.WriteLine("[WARNING] - The value for MQTT_PORT is invalid or empty, defaulting to port 1883.", "Energy Monitor");
            return 1883;
        }
    }
}
