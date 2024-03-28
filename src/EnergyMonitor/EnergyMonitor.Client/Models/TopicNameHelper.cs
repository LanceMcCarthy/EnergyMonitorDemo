using System.Reflection;

namespace EnergyMonitor.Client.Models;

public static class TopicNameHelper
{
    // ******************************************************************* //
    // These are the topics and real-world values
    // ******************************************************************* //
    // solar_assistant/total/battery_power/state: 65
    // solar_assistant/total/battery_state_of_charge/state: 100
    // solar_assistant/total/bus_voltage/state: 240.6
    // solar_assistant/inverter_1/grid_frequency/state: 60.01
    // solar_assistant/inverter_1/pv_current_1/state: 2.4
    // solar_assistant/inverter_1/pv_power/state: 165
    // solar_assistant/inverter_1/battery_voltage/state: 27.2
    // solar_assistant/inverter_1/load_apparent_power/state: 121
    // solar_assistant/inverter_1/pv_current_2/state: 0
    // solar_assistant/inverter_1/temperature/state: 98.4
    // solar_assistant/inverter_1/load_percentage/state: 4
    // solar_assistant/inverter_1/battery_current/state: 2.4
    // solar_assistant/inverter_1/grid_power/state: 0
    // solar_assistant/inverter_1/pv_voltage_1/state: 69.5
    // solar_assistant/inverter_1/pv_voltage_2/state: 0
    // solar_assistant/inverter_1/pv_power_1/state: 165
    // solar_assistant/inverter_1/device_mode/state: Solar/Battery
    // solar_assistant/inverter_1/grid_voltage/state: 119.4
    // solar_assistant/inverter_1/ac_output_frequency/state: 60
    // solar_assistant/inverter_1/ac_output_voltage/state: 120.1
    // solar_assistant/inverter_1/pv_power_2/state: 0
    // solar_assistant/inverter_1/load_power/state: 79
    // ******************************************************************* //

    /// <summary>
    /// Converts the topic string (e.g. 'solar_assistant/inverter_1/battery_current/state') to a more easily workable enum
    /// </summary>
    /// <param name="topic">MQTT topic (e.g. 'solar_assistant/inverter_1/battery_current/state')</param>
    /// <returns>The matching enum equivalent.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the topic has no known match, an out of range exception wil be thrown. If needed, add the nw unknown type to the enum.</exception>
    public static TopicName GetTopicName(string topic)
    {
        return topic switch
        {
            "solar_assistant/total/battery_power/state" => TopicName.BatteryPower_Total,
            "solar_assistant/total/battery_state_of_charge/state" => TopicName.BatteryStateOfCharge_Total,
            "solar_assistant/total/battery_energy_in/state" => TopicName.BatteryEnergyIn_Total,
            "solar_assistant/total/battery_energy_out/state" => TopicName.BatteryEnergyOut_Total,
            "solar_assistant/total/bus_voltage/state" => TopicName.BusVoltage_Total,
            "solar_assistant/total/grid_energy_in/state" => TopicName.GridEnergyIn_Total,
            "solar_assistant/total/grid_energy_out/state" => TopicName.GridEnergyOut_Total,
            "solar_assistant/total/load_energy/state" => TopicName.LoadEnergy_Total,
            "solar_assistant/total/pv_energy/state" => TopicName.PvEnergy_Total,
            "solar_assistant/inverter_1/grid_frequency/state" => TopicName.GridFrequency_Inverter1,
            "solar_assistant/inverter_1/pv_current_1/state" => TopicName.PvCurrent1_Inverter1,
            "solar_assistant/inverter_1/pv_power/state" => TopicName.PvPower_Inverter1,
            "solar_assistant/inverter_1/battery_voltage/state" => TopicName.BatteryVoltage_Inverter1,
            "solar_assistant/inverter_1/load_apparent_power/state" => TopicName.LoadApparentPower_Inverter1,
            "solar_assistant/inverter_1/pv_current_2/state" => TopicName.PvCurrent2_Inverter1,
            "solar_assistant/inverter_1/temperature/state" => TopicName.Temperature_Inverter1,
            "solar_assistant/inverter_1/load_percentage/state" => TopicName.LoadPercentage_Inverter1,
            "solar_assistant/inverter_1/battery_current/state" => TopicName.BatteryCurrent_Inverter1,
            "solar_assistant/inverter_1/grid_power/state" => TopicName.GridPower_Inverter1,
            "solar_assistant/inverter_1/pv_voltage_1/state" => TopicName.PvVoltage1_Inverter1,
            "solar_assistant/inverter_1/pv_voltage_2/state" => TopicName.PvVoltage2_Inverter1,
            "solar_assistant/inverter_1/pv_power_1/state" => TopicName.PvPower1_Inverter1,
            "solar_assistant/inverter_1/device_mode/state" => TopicName.DeviceMode_Inverter1,
            "solar_assistant/inverter_1/grid_voltage/state" => TopicName.GridVoltage_Inverter1,
            "solar_assistant/inverter_1/ac_output_frequency/state" => TopicName.AcOutputFrequency_Inverter1,
            "solar_assistant/inverter_1/ac_output_voltage/state" => TopicName.AcOutputVoltage_Inverter1,
            "solar_assistant/inverter_1/load_power/state" => TopicName.LoadPower_Inverter1,
            "solar_assistant/inverter_1/pv_power_2/state" => TopicName.PvPower2_Inverter1,
            _ => throw new ArgumentOutOfRangeException($"The topic name {topic} has no match. Please update the enum and helper method to support the new topic."),
        };
    }
}