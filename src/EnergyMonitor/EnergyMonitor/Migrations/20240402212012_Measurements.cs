using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnergyMonitor.Migrations
{
    /// <inheritdoc />
    public partial class Measurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("02652051-c4b5-4168-974f-72e101e9d8a0"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("02e40ef9-a2bc-457e-a442-e5a46655ef88"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("055faf02-76e0-49d5-8a73-a4d194cbd640"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("082bc3cb-ea15-496b-ade4-a68ea4a86508"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("1319b89f-897b-4f2b-ba16-92dd6cf8785a"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("168764a8-759f-4433-8ee8-6ad9ed08cf0c"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("3033ec35-73e5-4f63-ad5d-96b15a598a74"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("3168695a-4a9a-4ff8-8ef4-3eb5c7f569e1"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("412062ea-acda-4007-8800-6b8f47d0a8b2"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("46e0108d-6ff3-4989-829a-a51546612d65"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("4df42854-b4a7-4a7c-938a-a51da46ba6c4"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("5d5f3a20-625e-48a1-9e40-be62b9577fe5"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("72c179f1-b5a4-4f0a-8323-e0a422ed8fb6"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("a9196d9b-0988-4f35-89ec-a5177fe3ea9a"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("ab07768a-f126-4a60-9d94-c39194a115e3"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("b3992973-daab-49e7-8ed6-c4e4d6db5bb4"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("d7a00e5f-0df2-48da-a46b-3b5cea9f88b3"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("dbc52897-f501-4e60-abe1-0938e94d5ead"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("e4c61d22-249e-4afe-a4a2-304cb62af216"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("eb0d5082-d513-452e-8cfa-9d75bd73bccc"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("f4e728ce-8d48-447c-88e5-20b33b8fb13e"));

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Timestamp", "Topic", "Value" },
                values: new object[,]
                {
                    { new Guid("0561e634-bb84-4996-a75b-d85f4a81ecf3"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9695), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("1cc59aa7-8b7d-4b5f-ad19-01d9f4536fd4"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9679), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("3b26e7ed-e769-4633-a025-cb2d8e85abfa"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9722), "solar_assistant/total/battery_power/state", "650" },
                    { new Guid("3e53deac-0a81-4e4d-9084-d744bc94ba96"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9728), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("416b65e1-b321-4daf-b02f-bac093e5d5bc"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9719), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("461b327f-a341-4663-b147-d113debbce42"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9757), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("4d07ac99-da08-4be8-8a77-1cdc73fdb9c4"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9713), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("5005d2a8-9801-4aa4-aa13-fb678675dcd1"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9715), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("56220a35-c088-4279-9bf4-01756e388b86"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9697), "solar_assistant/inverter_1/charger_source_priority/state", "Solar/Battery/Grid" },
                    { new Guid("697acfc1-20c5-4516-b635-23e91d84f961"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9724), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("7ce2ac67-93a2-4907-a852-0b75b1eda4f4"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9711), "solar_assistant/total/battery_state_of_charge/state", "100" },
                    { new Guid("8432065c-1260-4cc4-962f-960ff118f7d8"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9699), "solar_assistant/inverter_1/device_mode/state", "Solar" },
                    { new Guid("850c520b-dd7d-43ba-857b-39f5b1b0ffed"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9703), "solar_assistant/inverter_1/grid_power/state", "0" },
                    { new Guid("864b3b72-c932-4373-a98e-e75a2d78126b"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9701), "solar_assistant/inverter_1/pv_power/state", "1600" },
                    { new Guid("9f8502cc-821b-4c41-818d-aefb88c7dfb6"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9707), "solar_assistant/total/battery_power/state", "725" },
                    { new Guid("a10e9acf-f87d-4505-9d92-592590637a02"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9620), "solar_assistant/inverter_1/pv_power/state", "1400" },
                    { new Guid("b2de39de-0427-441c-ba61-59b6b3f28643"), new DateTime(2024, 4, 2, 17, 20, 9, 818, DateTimeKind.Local).AddTicks(9705), "solar_assistant/inverter_1/load_power/state", "875" },
                    { new Guid("c5d93440-e38a-416a-8d8a-491792f7060b"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9692), "solar_assistant/total/battery_power/state", "525" },
                    { new Guid("dceb6be7-8eec-4556-8273-d66651221676"), new DateTime(2024, 4, 2, 17, 20, 10, 818, DateTimeKind.Local).AddTicks(9681), "solar_assistant/inverter_1/load_power/state", "875" },
                    { new Guid("e1556e44-3722-4532-82f5-84264af2651b"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9721), "solar_assistant/inverter_1/load_power/state", "1050" },
                    { new Guid("ff5573b6-6210-449f-ae26-7712cfc9efed"), new DateTime(2024, 4, 2, 17, 20, 8, 818, DateTimeKind.Local).AddTicks(9717), "solar_assistant/inverter_1/pv_power/state", "1700" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("0561e634-bb84-4996-a75b-d85f4a81ecf3"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("1cc59aa7-8b7d-4b5f-ad19-01d9f4536fd4"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("3b26e7ed-e769-4633-a025-cb2d8e85abfa"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("3e53deac-0a81-4e4d-9084-d744bc94ba96"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("416b65e1-b321-4daf-b02f-bac093e5d5bc"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("461b327f-a341-4663-b147-d113debbce42"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("4d07ac99-da08-4be8-8a77-1cdc73fdb9c4"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("5005d2a8-9801-4aa4-aa13-fb678675dcd1"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("56220a35-c088-4279-9bf4-01756e388b86"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("697acfc1-20c5-4516-b635-23e91d84f961"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("7ce2ac67-93a2-4907-a852-0b75b1eda4f4"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("8432065c-1260-4cc4-962f-960ff118f7d8"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("850c520b-dd7d-43ba-857b-39f5b1b0ffed"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("864b3b72-c932-4373-a98e-e75a2d78126b"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("9f8502cc-821b-4c41-818d-aefb88c7dfb6"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("a10e9acf-f87d-4505-9d92-592590637a02"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("b2de39de-0427-441c-ba61-59b6b3f28643"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("c5d93440-e38a-416a-8d8a-491792f7060b"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("dceb6be7-8eec-4556-8273-d66651221676"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("e1556e44-3722-4532-82f5-84264af2651b"));

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: new Guid("ff5573b6-6210-449f-ae26-7712cfc9efed"));

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
    }
}
