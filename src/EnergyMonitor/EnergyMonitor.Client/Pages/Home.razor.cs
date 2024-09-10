using CommonHelpers.Collections;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using static EnergyMonitor.Client.Models.MessageUtilities;

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MessagesDbService DbService { get; set; } = default!;

    //private ObservableRangeCollection<MqttDataItem>? AllData { get; } = new(){ Maximum = 120 };
    private ObservableRangeCollection<ChartMqttDataItem> SolarPowerData { get; } = new(){ MaximumCount = 300 };
    private ObservableRangeCollection<ChartMqttDataItem> LoadPowerData { get; } = new(){ MaximumCount = 300 };
    private ObservableRangeCollection<ChartMqttDataItem> BatteryPowerData { get; } = new(){ MaximumCount = 300 };
    private ObservableRangeCollection<ChartMqttDataItem> GridPowerData { get; } = new(){ MaximumCount = 300 };
    private ObservableRangeCollection<ChartMqttDataItem> BatteryChargeData { get; } = new(){ MaximumCount = 300 };

    private TelerikChart? BatteryPercentageChartRef { get; set; }
    private TelerikChart? SystemPowerChartRef { get; set; }

    private CancellationTokenSource? cts;
    private int LoadDataInterval { get; set; } = 200;
    private bool IsTimerRunning { get; set; }

    private double BatteryChargePercentage { get; set; } = 0;
    private string CurrentSolar { get; set; } = "0";
    private string CurrentLoad { get; set; } = "0";
    private string CurrentBatteryPowerTotal { get; set; } = "0";
    private string CurrentGridTotal { get; set; } = "0";
    private string CurrentInverterMode { get; set; } = "Solar/Battery/Grid";
    private string ChargerSourcePriority { get; set; } = "Solar";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            cts = new CancellationTokenSource();

            IsTimerRunning = true;

            await IntervalDataUpdate();
        }
    }

    private async Task IntervalDataUpdate()
    {
        while (cts?.Token != null)
        {
            await Task.Delay(LoadDataInterval, cts.Token);

            await GetDataAsync();

            StateHasChanged();
        }
    }

    private async Task OnIsTimerRunning(bool val)
    {
        if (val)
        {
            if(cts?.Token == null)
            {
                cts = new CancellationTokenSource();

                await IntervalDataUpdate();
            }
        }
        else
        {
            if(cts != null)
                await cts.CancelAsync();
        }
    }

    private async Task GetDataAsync()
    {
        var items = await DbService.GetMeasurementsAsync(DateTime.Now.AddMinutes(-30), DateTime.Now);

        // We only use the most recent 60 items from the database on initial load. For a longer timeline, use the /history page
        foreach (var item in items.Take(60))
        {
            var topicName = GetTopicName(item.Topic);

            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (topicName)
            {
                case TopicName.LoadPower_Inverter1:
                    LoadPowerData.Add(new ChartMqttDataItem{ Category = topicName, CurrentValue = Convert.ToDouble(item.Value), Timestamp = DateTime.Now });
                    break;
                case TopicName.PvPower_Inverter1:
                    SolarPowerData.Add(new ChartMqttDataItem{ Category = topicName, CurrentValue = Convert.ToDouble(item.Value), Timestamp = DateTime.Now });
                    break;
                case TopicName.BatteryPower_Total:
                    BatteryPowerData.Add(new ChartMqttDataItem{ Category = topicName, CurrentValue = Convert.ToDouble(item.Value), Timestamp = DateTime.Now });
                    break;
                case TopicName.GridPower_Inverter1:
                    GridPowerData.Add(new ChartMqttDataItem{ Category = topicName, CurrentValue = Convert.ToDouble(item.Value), Timestamp = DateTime.Now });
                    break;
                case TopicName.BatteryStateOfCharge_Total:
                    BatteryChargeData.Add(new ChartMqttDataItem{ Category = topicName, CurrentValue = Convert.ToDouble(item.Value), Timestamp = DateTime.Now });
                    break;
            }
        }

        CurrentInverterMode = items.FindLast(d => d.Topic == GetTopic(TopicName.DeviceMode_Inverter1))?.Value!;
        CurrentLoad = items.FindLast(d => d.Topic == GetTopic(TopicName.LoadPower_Inverter1))?.Value!;
        CurrentSolar = items.FindLast(d => d.Topic == GetTopic(TopicName.PvPower_Inverter1))?.Value!;
        CurrentBatteryPowerTotal = items.FindLast(d => d.Topic == GetTopic(TopicName.BatteryPower_Total))?.Value!;
        CurrentGridTotal = items.FindLast(d => d.Topic == GetTopic(TopicName.GridPower_Inverter1))?.Value!;
        BatteryChargePercentage = Convert.ToDouble(items.FindLast(d => d.Topic == GetTopic(TopicName.BatteryStateOfCharge_Total))?.Value!);
    }

    private void ItemResize()
    {
        StateHasChanged();
        SystemPowerChartRef?.Refresh();
        BatteryPercentageChartRef?.Refresh();
    }
}