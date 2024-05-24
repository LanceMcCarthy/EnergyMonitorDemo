using System.Collections.ObjectModel;
using System.Text;
using EnergyMonitor.Client.Components.Charts.SystemPower;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Models.Collections;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;
using MQTTnet.Client;
using Telerik.Blazor.Components;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MqttUiService MqttService { get; set; } = default!;

    [Inject]
    public MessagesDataService DataService { get; set; } = default!;

    //private TrimmableCollection<MqttDataItem>? AllData { get; } = new(){ Maximum = 120 };
    private TrimmableCollection<ChartMqttDataItem> SolarPowerData { get; } = new(){ Maximum = 120 };
    private TrimmableCollection<ChartMqttDataItem> LoadPowerData { get; } = new(){ Maximum = 120 };
    private TrimmableCollection<ChartMqttDataItem> BatteryPowerData { get; } = new(){ Maximum = 120 };
    private TrimmableCollection<ChartMqttDataItem> GridPowerData { get; } = new(){ Maximum = 120 };
    private TrimmableCollection<ChartMqttDataItem> BatteryChargeData { get; } = new(){ Maximum = 120 };

    private TelerikChart? BatteryPercentageChartRef { get; set; }
    private TelerikChart? SystemPowerChartRef { get; set; }
    private bool IsSubscribed { get; set; } = true;
    private bool IsDatabaseEnabled { get; set; } = false;
    private double BatteryChargePercentage { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBatteryPowerTotal { get; set; } = "0";
    private string CurrentGridTotal { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "Solar/Battery/Grid";
    private string ChargerSourcePriority { get; set; } = "Solar";

    protected override async Task OnInitializedAsync()
    {
        // Load up last 2 hours of data from the last 12 hours
        var items = await DataService.GetMeasurementsAsync(DateTime.Now.AddHours(-12), DateTime.Now);

        // We only use the most recent 60 items from the database on initial load. For a longer timeline, use the /history page
        foreach (var item in items.Take(60))
        {
            await ProcessDataItem(item.Value, TopicNameHelper.GetTopicName(item.Topic));
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

    private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
    {
        // Processing an incoming MQTT Message

        try
        {
            // *************************************************************************** //
            // ******** Step 1 - Get the topic and payload data out of the message ******* //
            // *************************************************************************** //

            // Read the topic string. I return a strong type that we can easily work with it instead of crazy strings.
            var messageTopic = TopicNameHelper.GetTopicName(e.ApplicationMessage.Topic);

            // Read the payload. Important! It is in the form of an ArraySegment<byte>, so we need to convert to byte[], then to ASCII.
            var decodedPayload = Encoding.ASCII.GetString(e.ApplicationMessage.PayloadSegment.ToArray());

            var item = new MqttDataItem { Topic = e.ApplicationMessage.Topic, Value = decodedPayload, Timestamp = DateTime.Now };


            // *************************************************************************** //
            // ************ Step 2 - Store the item in the long term storage ************* //
            // *************************************************************************** //
            
            if(IsDatabaseEnabled)
            {
                // Save item to database
                await DataService.AddMeasurementAsync(item);
            }

            // *************************************************************************** //
            // ************* Step 3 - Now we can do something with that data ************* //
            // *************************************************************************** //

            // Update individual collections based on topic
            await ProcessDataItem(decodedPayload, messageTopic);

            // Update UI
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"There was a problem processing {e.ApplicationMessage.Topic}: {exception.Message}");
        }
    }

    private Task ProcessDataItem(string decodedPayload, TopicName messageTopic)
    {
        var item = new ChartMqttDataItem { Category = messageTopic, Timestamp = DateTime.Now };

        switch (messageTopic)
        {
            case TopicName.DeviceMode_Inverter1:
                CurrentInverterMode = decodedPayload;
                break;

            case TopicName.ChargerSourcePriority_Inverter1:
                ChargerSourcePriority = decodedPayload;
                break;

            case TopicName.LoadPower_Inverter1:
                item.CurrentValue = Convert.ToDouble(decodedPayload);
                CurrentLoad = $"{item.CurrentValue}";
                LoadPowerData.Add(item);
                break;

            case TopicName.PvPower_Inverter1:
                item.CurrentValue = Convert.ToDouble(decodedPayload);
                CurrentSolar = $"{item.CurrentValue}";
                SolarPowerData.Add(item);
                break;

            case TopicName.BatteryPower_Total:
                item.CurrentValue = Convert.ToDouble(decodedPayload);
                CurrentBatteryPowerTotal = $"{item.CurrentValue}";
                BatteryPowerData.Add(item);
                break;

            case TopicName.GridPower_Inverter1:
                item.CurrentValue = Convert.ToDouble(decodedPayload);
                CurrentGridTotal = $"{item.CurrentValue}";
                GridPowerData.Add(item);
                break;

            case TopicName.BatteryStateOfCharge_Total:
                BatteryChargePercentage = item.CurrentValue = Convert.ToDouble(decodedPayload);
                BatteryChargeData.Add(item);
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
            case TopicName.BatteryAbsorptionChargeVoltage_Inverter1:
            case TopicName.MaxChargeCurrent_Inverter1:
            case TopicName.BatteryFloatChargeVoltage_Inverter1:
            case TopicName.MaxGridChargeCurrent_Inverter1:
            case TopicName.OutputSourcePriority_Inverter1:
            case TopicName.ToGridBatteryVoltage_Inverter1:
            case TopicName.ShutdownBatteryVoltage_Inverter1:
            case TopicName.BackToBatteryVoltage_Inverter1:
            case TopicName.SerialNumber_Inverter1:
            case TopicName.PowerSaving_Inverter1:
            case TopicName.Current_Battery1:
            case TopicName.Voltage_Battery1:
            case TopicName.Power_Battery1:
            case TopicName.StateOfCharge_Battery1:
            case TopicName.Unknown:
            default:
                break;
        }

        return Task.CompletedTask;
    }

    private void ItemResize()
    {
        StateHasChanged();
        SystemPowerChartRef?.Refresh();
        BatteryPercentageChartRef?.Refresh();
    }
}