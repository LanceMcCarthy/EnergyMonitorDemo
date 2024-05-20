﻿// ReSharper disable InconsistentNaming

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
    LoadEnergy_Total,
    PvEnergy_Total,
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
    PvPower2_Inverter1,
    ChargerSourcePriority_Inverter1,
    BatteryAbsorptionChargeVoltage_Inverter1,
    MaxChargeCurrent_Inverter1,
    BatteryFloatChargeVoltage_Inverter1,
    MaxGridChargeCurrent_Inverter1,
    OutputSourcePriority_Inverter1,
    ToGridBatteryVoltage_Inverter1,
    ShutdownBatteryVoltage_Inverter1,
    BackToBatteryVoltage_Inverter1,
    SerialNumber_Inverter1,
    PowerSaving_Inverter1,
    Current_Battery1,
    StateOfCharge_Battery1,
    Voltage_Battery1,
    Power_Battery1,
    Unknown
}