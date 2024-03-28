using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using EnergyMonitor.Client.Components.Charts.SystemPower;
using EnergyMonitor.Client.Models;
using Microsoft.AspNetCore.Components;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Packets;
using Telerik.Blazor.Components;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MqttUiService MqttService { get; set; } = default!;//injected
    private SystemPowerChart? SystemPowerChartRef { get; set; }

    private MqttFactory? mqttFactory;
    private IMqttClient? mqttClient;
    private bool IsSubscribed { get; set; } = true;

    private ObservableCollection<ChartMqttDataItem> SolarPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> LoadPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> BatteryPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> GridPowerData { get; } = new();
    private ObservableCollection<GridMqttDataItem> AllData { get; } = new();

    private double BatteryChargePercentage { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBatteryPowerTotal { get; set; } = "0";
    private string CurrentGridTotal { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "...";

    protected override async Task OnInitializedAsync()
    {
        // Setting Up MQTT stuff
        await MqttService.SetupMqtt(OnMessageReceivedAsync);
    }

    public async Task<bool> OnIsSubscribedChanged(bool value)
    {
        if (value)
        {
            await MqttService.SubscribeAsync();
        }
        else
        {
            await MqttService.UnsubscribeAsync();
        }

        return IsSubscribed = value;
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

        // Always add to DataGrid
        
        AllData.Add(new GridMqttDataItem { Topic = $"{messageTopic}", Value = decodedPayload, Timestamp = DateTime.Now });
        if (AllData.Count > 200) AllData.RemoveAt(0);

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
                CurrentSolar = $"{pvCurrent}";
                SolarPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = pvCurrent, Timestamp = DateTime.Now });
                if (SolarPowerData.Count > 100) SolarPowerData.RemoveAt(0);
                break;

            case TopicName.BatteryPower_Total:
                var batteryPower = Convert.ToDouble(decodedPayload);
                CurrentBatteryPowerTotal = $"{batteryPower}";
                BatteryPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = batteryPower, Timestamp = DateTime.Now });
                if (BatteryPowerData.Count > 100) SolarPowerData.RemoveAt(0);
                break;

            case TopicName.GridPower_Inverter1:
                var gridPower = Convert.ToDouble(decodedPayload);
                CurrentGridTotal = $"{gridPower}";
                GridPowerData.Add(new ChartMqttDataItem { Category = messageTopic, CurrentValue = gridPower, Timestamp = DateTime.Now });
                if (GridPowerData.Count > 100) GridPowerData.RemoveAt(0);
                break;

            case TopicName.BatteryStateOfCharge_Total:
                var batteryCharge = Convert.ToDouble(decodedPayload);
                BatteryChargePercentage = batteryCharge;
                break;
            case TopicName.PvEnergy_Total:
            case TopicName.LoadEnergy_Total:
            case TopicName.BatteryEnergyIn_Total:
            case TopicName.BatteryEnergyOut_Total:
            case TopicName.GridEnergyIn_Total:
            case TopicName.GridEnergyOut_Total:
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
        SystemPowerChartRef?.Refresh();
    }
}