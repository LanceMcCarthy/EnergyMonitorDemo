namespace EnergyMonitor.Client.Models;

public class GridMqttDataItem
{
    public string? Topic { get; set; }
    public string? Value { get; set; }
    public DateTime Timestamp { get; set; }
}