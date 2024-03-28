// ReSharper disable InconsistentNaming

namespace EnergyMonitor.Client.Models;

/// <summary>
/// Strongly typed equivalent of an MQTT topic.
/// </summary>
public enum TopicName
{
    BatteryPower_Total,
    BatteryStateOfCharge_Total,
    BatteryEnergyIn_Total,
    BatteryEnergyOut_Total,
    BusVoltage_Total,
    GridEnergyIn_Total,
    GridEnergyOut_Total,
    GridFrequency_Inverter1,
    PvCurrent1_Inverter1,
    PvPower_Inverter1,
    BatteryVoltage_Inverter1,
    LoadApparentPower_Inverter1,
    PvCurrent2_Inverter1,
    Temperature_Inverter1,
    LoadPercentage_Inverter1,
    BatteryCurrent_Inverter1,
    GridPower_Inverter1,
    PvVoltage1_Inverter1,
    PvVoltage2_Inverter1,
    PvPower1_Inverter1,
    DeviceMode_Inverter1,
    GridVoltage_Inverter1,
    AcOutputFrequency_Inverter1,
    AcOutputVoltage_Inverter1,
    LoadPower_Inverter1,
    PvPower2_Inverter1
}