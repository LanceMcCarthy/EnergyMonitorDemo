using CommonHelpers.Collections;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;
using Telerik.Blazor.Components;
using static EnergyMonitor.Client.Models.MessageUtilities;
// ReSharper disable ArrangeTypeMemberModifiers

namespace EnergyMonitor.Client.Pages;

public partial class Home
{
    [Inject]
    public MessagesDbService DbService { get; set; } = default!;

    [Inject]
    Blazored.LocalStorage.ILocalStorageService LocalStorage { get; set; } = default!;

    TelerikChart? BatteryPercentageChartRef { get; set; }
    TelerikChart? SystemPowerChartRef { get; set; }
    TelerikTileLayout? TileLayoutInstance { get; set; }

    ObservableRangeCollection<ChartMqttDataItem> SolarPowerData { get; } = new(){ MaximumCount = 300 };
    ObservableRangeCollection<ChartMqttDataItem> LoadPowerData { get; } = new(){ MaximumCount = 300 };
    ObservableRangeCollection<ChartMqttDataItem> BatteryPowerData { get; } = new(){ MaximumCount = 300 };
    ObservableRangeCollection<ChartMqttDataItem> GridPowerData { get; } = new(){ MaximumCount = 300 };
    ObservableRangeCollection<ChartMqttDataItem> BatteryChargeData { get; } = new(){ MaximumCount = 300 };

    static readonly List<string> TimeRanges = ["1h", "6h", "12h", "24h"];
    string ActiveTimeRange { get; set; } = TimeRanges[0];
    DateTime StartDateTime { get; set; } = DateTime.Now.AddHours(-1);
    DateTime EndDateTime { get; set; } = DateTime.Now;
    
    CancellationTokenSource? cts;
    int LoadDataInterval { get; set; } = 2000;
    bool IsTimerRunning { get; set; }
    const string LocalStorageKey = "tile-layout-state";

    string SolarPower { get; set; } = "0";
    string LoadPower { get; set; } = "0";
    string BatteryPower { get; set; } = "0";
    string GridPower { get; set; } = "0";
    string InverterMode { get; set; } = "Solar/Battery/Grid";
    string ChargerSourcePriority { get; set; } = "Solar";
    double BatteryChargeLevel { get; set; } = 0;

    string BatteryVoltage { get; set; } = "0";
    string BackToBatteryVoltage { get; set; } = "0";
    string Pv1Voltage { get; set; } = "0";
    string BusVoltage { get; set; } = "0";
    string OutputVoltage { get; set; } = "0";

    string OutputFrequency { get; set; } = "0";
    string GridFrequency { get; set; } = "0";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadState();

            cts = new CancellationTokenSource();

            IsTimerRunning = true;

            await IntervalDataUpdate();
        }

        await base.OnAfterRenderAsync(firstRender);
    }
    
    async Task IntervalDataUpdate()
    {
        while (cts?.Token != null)
        {
            await Task.Delay(LoadDataInterval, cts.Token);

            await GetDataAsync();
        }
    }

    async Task ToggleTimer()
    {
        if (!IsTimerRunning)
        {
            if (cts?.Token == null)
            {
                cts = new CancellationTokenSource();

                await IntervalDataUpdate();
            }
        }
        else
        {
            if (cts != null)
                await cts.CancelAsync();
        }
    }

    private async Task OnTimeRangeChanged(string newValue)
    {
        ActiveTimeRange = newValue;

        SolarPowerData.Clear();
        LoadPowerData.Clear();
        BatteryPowerData.Clear();
        GridPowerData.Clear();
        BatteryChargeData.Clear();

        StartDateTime = newValue switch
        {
            "1h" => DateTime.Now.AddHours(-1),
            "6h" => DateTime.Now.AddHours(-6),
            "12h" => DateTime.Now.AddHours(-12),
            "24h" => DateTime.Now.AddHours(-24),
            _ => StartDateTime
        };

        // regardless if timer is running or not, we want to refresh the data
        await GetDataAsync();
    }

    async Task GetDataAsync()
    {
        var items = await DbService.GetMeasurementsAsync(StartDateTime, EndDateTime);

        LoadPower = items.GetNewestValue(TopicName.LoadPower_Inverter1, "0");
        SolarPower = items.GetNewestValue(TopicName.PvPower_Inverter1, "0");
        GridPower = items.GetNewestValue(TopicName.GridPower_Inverter1, "0");
        BatteryPower = items.GetNewestValue(TopicName.BatteryPower_Total, "0");

        BatteryChargeLevel = Convert.ToDouble(items.GetNewestValue(TopicName.BatteryStateOfCharge_Total, "0"));

        InverterMode = items.GetNewestValue(TopicName.DeviceMode_Inverter1, "unknown");
        ChargerSourcePriority = items.GetNewestValue(TopicName.ChargerSourcePriority_Inverter1, "unknown");

        GridFrequency = items.GetNewestValue(TopicName.GridFrequency_Inverter1, "0");
        OutputFrequency = items.GetNewestValue(TopicName.AcOutputFrequency_Inverter1, "0");

        OutputVoltage = items.GetNewestValue(TopicName.AcOutputVoltage_Inverter1, "0");
        BatteryVoltage  = items.GetNewestValue(TopicName.BatteryVoltage_Inverter1, "0");
        BackToBatteryVoltage = items.GetNewestValue(TopicName.BackToBatteryVoltage_Inverter1, "0");
        Pv1Voltage = items.GetNewestValue(TopicName.PvVoltage1_Inverter1, "0");
        BusVoltage = items.GetNewestValue(TopicName.BusVoltage_Total, "0");

        // We only use the most recent 60 items from the database on initial load. For a longer timeline, use the /history page
        foreach (var item in items.Take(60))
        {
            if (item.Topic == null)
                continue;

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

        await InvokeAsync(StateHasChanged);
    }

    async Task ItemResize()
    {
        StateHasChanged();
        SystemPowerChartRef?.Refresh();
        BatteryPercentageChartRef?.Refresh();
        await SaveState();
    }

    async Task OnReorder()
    {
        await SaveState();
    }

    async Task SaveState()
    {
        TileLayoutState? state = TileLayoutInstance?.GetState();
        if (state is null) return;
        await LocalStorage.SetItemAsync(LocalStorageKey, state);
    }

    async Task LoadState()
    {
        TileLayoutState? state = await LocalStorage.GetItemAsync<TileLayoutState>(LocalStorageKey);
        if (state is null) return;
        TileLayoutInstance?.SetState(state);
    }


    async Task ClearTileLayout()
    {
        await LocalStorage.RemoveItemAsync(LocalStorageKey);

        TileLayoutInstance?.SetState(new() { ItemStates= new[]
        {
            new TileLayoutItemState { Order = 0, ColSpan = 1, RowSpan = 1 },
            new TileLayoutItemState { Order = 1, ColSpan = 1, RowSpan = 1 },
            new TileLayoutItemState { Order = 2, ColSpan = 1, RowSpan = 1 },
            new TileLayoutItemState { Order = 3, ColSpan = 1, RowSpan = 1 },
            new TileLayoutItemState { Order = 4, ColSpan = 1, RowSpan = 1 },
            new TileLayoutItemState { Order = 5, ColSpan = 4, RowSpan = 2 },
            new TileLayoutItemState { Order = 6, ColSpan = 1, RowSpan = 2 }
        } });
    }
}