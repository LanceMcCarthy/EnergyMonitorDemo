using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnergyMonitor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Topic = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Timestamp", "Topic", "Value" },
                values: new object[,]
                {
                    { new Guid("02652051-c4b5-4168-974f-72e101e9d8a0"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7405), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("02e40ef9-a2bc-457e-a442-e5a46655ef88"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7412), "solar_assistant/inverter_1/pv_power/state", "1700" },
                    { new Guid("055faf02-76e0-49d5-8a73-a4d194cbd640"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7400), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("082bc3cb-ea15-496b-ade4-a68ea4a86508"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7414), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("1319b89f-897b-4f2b-ba16-92dd6cf8785a"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7421), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("168764a8-759f-4433-8ee8-6ad9ed08cf0c"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7418), "solar_assistant/total/battery_power/state", "650" },
                    { new Guid("3033ec35-73e5-4f63-ad5d-96b15a598a74"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7376), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("3168695a-4a9a-4ff8-8ef4-3eb5c7f569e1"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7394), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("412062ea-acda-4007-8800-6b8f47d0a8b2"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7372), "solar_assistant/total/battery_power/state", "525" },
                    { new Guid("46e0108d-6ff3-4989-829a-a51546612d65"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7408), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("4df42854-b4a7-4a7c-938a-a51da46ba6c4"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7389), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("5d5f3a20-625e-48a1-9e40-be62b9577fe5"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7398), "solar_assistant/total/battery_power/state", "725" },
                    { new Guid("72c179f1-b5a4-4f0a-8323-e0a422ed8fb6"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7374), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("a9196d9b-0988-4f35-89ec-a5177fe3ea9a"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7416), "solar_assistant/inverter_1/load_power/state", "1050" },
                    { new Guid("ab07768a-f126-4a60-9d94-c39194a115e3"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7367), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("b3992973-daab-49e7-8ed6-c4e4d6db5bb4"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7391), "solar_assistant/inverter_1/pv_power/state", "1600" },
                    { new Guid("d7a00e5f-0df2-48da-a46b-3b5cea9f88b3"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7420), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("dbc52897-f501-4e60-abe1-0938e94d5ead"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7311), "solar_assistant/inverter_1/pv_power/state", "1400" },
                    { new Guid("e4c61d22-249e-4afe-a4a2-304cb62af216"), new DateTime(2024, 4, 2, 17, 16, 30, 465, DateTimeKind.Local).AddTicks(7423), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("eb0d5082-d513-452e-8cfa-9d75bd73bccc"), new DateTime(2024, 4, 2, 17, 16, 31, 465, DateTimeKind.Local).AddTicks(7396), "solar_assistant/inverter_1/load_power/state", "875" },
                    { new Guid("f4e728ce-8d48-447c-88e5-20b33b8fb13e"), new DateTime(2024, 4, 2, 17, 16, 32, 465, DateTimeKind.Local).AddTicks(7370), "solar_assistant/inverter_1/load_power/state", "875" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
