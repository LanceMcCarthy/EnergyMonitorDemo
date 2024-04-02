﻿// <auto-generated />
using System;
using EnergyMonitor.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnergyMonitor.Migrations
{
    [DbContext(typeof(MeasurementsDbContext))]
    [Migration("20240402211633_Initial-Commit")]
    partial class InitialCommit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("EnergyMonitor.Client.Models.MqttDataItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Measurements");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dbc52897-f501-4e60-abe1-0938e94d5ead"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7311),
                            Topic = "solar_assistant/inverter_1/pv_power/state",
                            Value = "1400"
                        },
                        new
                        {
                            Id = new Guid("ab07768a-f126-4a60-9d94-c39194a115e3"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7367),
                            Topic = "solar_assistant/inverter_1/grid_power/state",
                            Value = "0"
                        },
                        new
                        {
                            Id = new Guid("f4e728ce-8d48-447c-88e5-20b33b8fb13e"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7370),
                            Topic = "solar_assistant/inverter_1/load_power/state",
                            Value = "875"
                        },
                        new
                        {
                            Id = new Guid("412062ea-acda-4007-8800-6b8f47d0a8b2"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7372),
                            Topic = "solar_assistant/total/battery_power/state",
                            Value = "525"
                        },
                        new
                        {
                            Id = new Guid("72c179f1-b5a4-4f0a-8323-e0a422ed8fb6"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7374),
                            Topic = "solar_assistant/total/battery_state_of_charge/state",
                            Value = "100"
                        },
                        new
                        {
                            Id = new Guid("3033ec35-73e5-4f63-ad5d-96b15a598a74"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7376),
                            Topic = "solar_assistant/inverter_1/charger_source_priority/state",
                            Value = "Solar/Battery/Grid"
                        },
                        new
                        {
                            Id = new Guid("4df42854-b4a7-4a7c-938a-a51da46ba6c4"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7389),
                            Topic = "solar_assistant/inverter_1/device_mode/state",
                            Value = "Solar"
                        },
                        new
                        {
                            Id = new Guid("b3992973-daab-49e7-8ed6-c4e4d6db5bb4"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7391),
                            Topic = "solar_assistant/inverter_1/pv_power/state",
                            Value = "1600"
                        },
                        new
                        {
                            Id = new Guid("3168695a-4a9a-4ff8-8ef4-3eb5c7f569e1"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7394),
                            Topic = "solar_assistant/inverter_1/grid_power/state",
                            Value = "0"
                        },
                        new
                        {
                            Id = new Guid("eb0d5082-d513-452e-8cfa-9d75bd73bccc"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7396),
                            Topic = "solar_assistant/inverter_1/load_power/state",
                            Value = "875"
                        },
                        new
                        {
                            Id = new Guid("5d5f3a20-625e-48a1-9e40-be62b9577fe5"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7398),
                            Topic = "solar_assistant/total/battery_power/state",
                            Value = "725"
                        },
                        new
                        {
                            Id = new Guid("055faf02-76e0-49d5-8a73-a4d194cbd640"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7400),
                            Topic = "solar_assistant/total/battery_state_of_charge/state",
                            Value = "100"
                        },
                        new
                        {
                            Id = new Guid("02652051-c4b5-4168-974f-72e101e9d8a0"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7405),
                            Topic = "solar_assistant/inverter_1/charger_source_priority/state",
                            Value = "Solar/Battery/Grid"
                        },
                        new
                        {
                            Id = new Guid("46e0108d-6ff3-4989-829a-a51546612d65"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7408),
                            Topic = "solar_assistant/inverter_1/device_mode/state",
                            Value = "Solar"
                        },
                        new
                        {
                            Id = new Guid("02e40ef9-a2bc-457e-a442-e5a46655ef88"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7412),
                            Topic = "solar_assistant/inverter_1/pv_power/state",
                            Value = "1700"
                        },
                        new
                        {
                            Id = new Guid("082bc3cb-ea15-496b-ade4-a68ea4a86508"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7414),
                            Topic = "solar_assistant/inverter_1/grid_power/state",
                            Value = "0"
                        },
                        new
                        {
                            Id = new Guid("a9196d9b-0988-4f35-89ec-a5177fe3ea9a"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7416),
                            Topic = "solar_assistant/inverter_1/load_power/state",
                            Value = "1050"
                        },
                        new
                        {
                            Id = new Guid("168764a8-759f-4433-8ee8-6ad9ed08cf0c"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7418),
                            Topic = "solar_assistant/total/battery_power/state",
                            Value = "650"
                        },
                        new
                        {
                            Id = new Guid("d7a00e5f-0df2-48da-a46b-3b5cea9f88b3"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7420),
                            Topic = "solar_assistant/total/battery_state_of_charge/state",
                            Value = "100"
                        },
                        new
                        {
                            Id = new Guid("1319b89f-897b-4f2b-ba16-92dd6cf8785a"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7421),
                            Topic = "solar_assistant/inverter_1/charger_source_priority/state",
                            Value = "Solar/Battery/Grid"
                        },
                        new
                        {
                            Id = new Guid("e4c61d22-249e-4afe-a4a2-304cb62af216"),
                            Timestamp = new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7423),
                            Topic = "solar_assistant/inverter_1/device_mode/state",
                            Value = "Solar"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}