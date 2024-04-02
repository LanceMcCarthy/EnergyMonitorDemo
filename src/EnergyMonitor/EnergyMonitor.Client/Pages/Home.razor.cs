using System.Collections.ObjectModel;
using System.Text;
using EnergyMonitor.Client.Components.Charts.SystemPower;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;
using MQTTnet.Client;
using Telerik.DataSource.Extensions;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MqttUiService MqttService { get; set; } = default!;//injected

    [Inject]
    public MessagesDataService DataService { get; set; } = default!;//injected

    private ObservableCollection<ChartMqttDataItem> SolarPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> LoadPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> BatteryPowerData { get; } = new();
    private ObservableCollection<ChartMqttDataItem> GridPowerData { get; } = new();
    private ObservableCollection<MqttDataItem> AllData { get; } = new();

    private SystemPowerChart? SystemPowerChartRef { get; set; }
    private bool IsSubscribed { get; set; } = true;
    private double BatteryChargePercentage { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBatteryPowerTotal { get; set; } = "0";
    private string CurrentGridTotal { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "Solar/Battery/Grid";
    private string ChargerSourcePriority { get; set; } = "Solar";

    protected override async Task OnInitializedAsync()
    {
        // Load up database data
        var items = await DataService.GetMeasurementsAsync();

        AllData.AddRange(items);

        foreach (var item in items)
        {
            ProcessDataItem(item.Value, TopicNameHelper.GetTopicName(item.Topic));
        }

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
        // Processing a Message

        //----------------------------------------------------------------------//
        // **  Step 1 - Get the topic and payload data out of the message
        //----------------------------------------------------------------------//

        // Read the topic string. I return a strong type that we can easily work with it instead of crazy strings.
        var messageTopic = TopicNameHelper.GetTopicName(e.ApplicationMessage.Topic);

        // Read the payload. Important! It is in the form of an ArraySegment<byte>, so we need to convert to byte[], then to ASCII.
        var decodedPayload = Encoding.ASCII.GetString(e.ApplicationMessage.PayloadSegment.ToArray());

        // DATABASE SAVE - Add to database
        DataService.AddMeasurementAsync(new MqttDataItem { Topic = e.ApplicationMessage.Topic, Value = decodedPayload, Timestamp = DateTime.Now }).ConfigureAwait(false);

        //----------------------------------------------------------------------//
        // **  Step 2 - Now we can do something with that data.
        //----------------------------------------------------------------------//

        // Create item for database and Grid use
        var item = new MqttDataItem { Topic = $"{messageTopic}", Value = decodedPayload, Timestamp = DateTime.Now };
        
        // Add item to the DataGrid items source and keep in-memory collection data to a manageable amount.
        if (AllData.Count > 200) 
            AllData.RemoveAt(0);

        AllData.Add(item);


        // Update individual collections based on topic
        ProcessDataItem(decodedPayload, messageTopic);

        // Update UI
        InvokeAsync(StateHasChanged);

        return Task.CompletedTask;
    }

    private void ProcessDataItem(string decodedPayload, TopicName messageTopic)
    {
        switch (messageTopic)
        {
            case TopicName.DeviceMode_Inverter1:
                CurrentInverterMode = decodedPayload;
                break;
            
            case TopicName.ChargerSourcePriority_Inverter1:
                ChargerSourcePriority = decodedPayload;
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
            default:
                break;
        }
    }

    private void ItemResize()
    {
        StateHasChanged();
        SystemPowerChartRef?.Refresh();
    }
}