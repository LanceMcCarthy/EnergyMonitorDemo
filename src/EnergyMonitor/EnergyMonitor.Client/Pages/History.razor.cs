using CommonHelpers.Collections;
using EnergyMonitor.Client.Models;
using EnergyMonitor.Client.Services;
using Microsoft.AspNetCore.Components;

namespace EnergyMonitor.Client.Pages;

public partial class History
{
    [Inject] public MessagesDbService DbService { get; set; } = default!;

    private ObservableRangeCollection<MqttDataItem> Data { get; } = new();
    public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-1);
    public DateTime EndDate { get; set; } = DateTime.Now;
    public int DebounceDelay { get; set; } = 333;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await UpdateGridAsync();
        }
    }

    private async void OnStartDateChange(object obj)
    {
        await UpdateGridAsync();
    }

    private async void OnEndDateChange(object obj)
    {
        await UpdateGridAsync();
    }

    private async Task UpdateGridAsync()
    {
        Data.Clear();
        Data.AddRange(await DbService.GetMeasurementsAsync(StartDate, EndDate));
    }
}