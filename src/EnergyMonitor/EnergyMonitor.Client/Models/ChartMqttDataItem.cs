namespace EnergyMonitor.Client.Models;

public class ChartMqttDataItem
{
    public string? Category { get; set; }
    public double CurrentValue { get; set; }
    public DateTime Timestamp { get; set; }
}