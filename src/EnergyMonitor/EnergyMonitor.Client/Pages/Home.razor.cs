using CommonHelpers.Collections;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MessagesDbService DbService { get; set; } = default!;

    //private ObservableRangeCollection<MqttDataItem>? AllData { get; } = new(){ Maximum = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> SolarPowerData { get; } = new(){ MaximumCount = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> LoadPowerData { get; } = new(){ MaximumCount = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> BatteryPowerData { get; } = new(){ MaximumCount = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> GridPowerData { get; } = new(){ MaximumCount = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> BatteryChargeData { get; } = new(){ MaximumCount = 120 };

    private TelerikChart? BatteryPercentageChartRef { get; set; }
    private TelerikChart? SystemPowerChartRef { get; set; }
    private double BatteryChargePercentage { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBatteryPowerTotal { get; set; } = "0";
    private string CurrentGridTotal { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "Solar/Battery/Grid";
    private string ChargerSourcePriority { get; set; } = "Solar";

    protected override async Task OnInitializedAsync()
    {
        await GetDataAsync();
    }

    protected async Task OnRefreshClick()
    {
        await GetDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task GetDataAsync()
    {
        // Load up last 2 hours of data
        var items = await DbService.GetMeasurementsAsync(DateTime.Now.AddHours(-2), DateTime.Now);

        // We only use the most recent 60 items from the database on initial load. For a longer timeline, use the /history page
        foreach (var item in items.Take(60))
        {
            await ProcessDataItem(item.Value, TopicHelper.GetTopicName(item.Topic));
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

            //case TopicName.PvEnergy_Total:
            //case TopicName.LoadEnergy_Total:
            //case TopicName.BatteryEnergyIn_Total:
            //case TopicName.BatteryEnergyOut_Total:
            //case TopicName.GridEnergyIn_Total:
            //case TopicName.GridEnergyOut_Total:
            //case TopicName.BusVoltage_Total:
            //case TopicName.GridFrequency_Inverter1:
            //case TopicName.PvCurrent1_Inverter1:
            //case TopicName.BatteryVoltage_Inverter1:
            //case TopicName.LoadApparentPower_Inverter1:
            //case TopicName.PvCurrent2_Inverter1:
            //case TopicName.Temperature_Inverter1:
            //case TopicName.LoadPercentage_Inverter1:
            //case TopicName.BatteryCurrent_Inverter1:
            //case TopicName.PvVoltage1_Inverter1:
            //case TopicName.PvVoltage2_Inverter1:
            //case TopicName.PvPower1_Inverter1:
            //case TopicName.GridVoltage_Inverter1:
            //case TopicName.AcOutputFrequency_Inverter1:
            //case TopicName.AcOutputVoltage_Inverter1:
            //case TopicName.PvPower2_Inverter1:
            //case TopicName.BatteryAbsorptionChargeVoltage_Inverter1:
            //case TopicName.MaxChargeCurrent_Inverter1:
            //case TopicName.BatteryFloatChargeVoltage_Inverter1:
            //case TopicName.MaxGridChargeCurrent_Inverter1:
            //case TopicName.OutputSourcePriority_Inverter1:
            //case TopicName.ToGridBatteryVoltage_Inverter1:
            //case TopicName.ShutdownBatteryVoltage_Inverter1:
            //case TopicName.BackToBatteryVoltage_Inverter1:
            //case TopicName.SerialNumber_Inverter1:
            //case TopicName.PowerSaving_Inverter1:
            //case TopicName.Current_Battery1:
            //case TopicName.Voltage_Battery1:
            //case TopicName.Power_Battery1:
            //case TopicName.StateOfCharge_Battery1:
            //case TopicName.Unknown:
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