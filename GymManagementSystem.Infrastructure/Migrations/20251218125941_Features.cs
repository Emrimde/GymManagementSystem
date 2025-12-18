using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("8c8b4914-6e5d-4487-af3c-ad4a84155cf3"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("5b505cd1-81f1-4ad4-963c-b4ea37fa9846"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("71c8dfb9-bd63-4f73-a3d3-1039e69a2bf0"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("73893ea9-6bcc-44b6-abc5-60c4802cdbb9"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("bbcf9e17-080e-4c92-b226-2ae4dea2c73e"));

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "BenefitDescription" },
                values: new object[,]
                {
                    { new Guid("265c36b0-c3b6-4a1d-a374-9769e7c0e2b7"), "fitness classes included in the price of the pass" },
                    { new Guid("3daf4cdb-eac4-4b11-b63a-f08a2b2fb5f9"), "one-time one-hour coaching consultation" },
                    { new Guid("4647b50e-5d18-4920-bd4f-953ba60e033d"), "the possibility to book group classes 7 days in advance" },
                    { new Guid("4adcfe6c-8b0a-440e-9eb3-9fd10a78d82f"), "1 hour of personal training every 6 months" },
                    { new Guid("724ec9bc-c40c-43cc-a5d8-6704eb2626d8"), "going to training with a friend 3 times a month" },
                    { new Guid("e00cbd24-7e96-4074-85c9-10438a662c89"), "access to all training areas" }
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("c6fa742d-d972-4402-befa-557476744d6a"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("1fb69ad4-226d-4a81-9d8c-316b691ea51c"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 18, 12, 59, 40, 992, DateTimeKind.Utc).AddTicks(6131), null },
                    { new Guid("20f68550-6270-4c79-a95e-4d85471b679d"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 18, 12, 59, 40, 992, DateTimeKind.Utc).AddTicks(6129), null },
                    { new Guid("bfd3dee5-ca75-4695-a524-82b5a39e8151"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 18, 12, 59, 40, 992, DateTimeKind.Utc).AddTicks(6116), null },
                    { new Guid("cf9b052f-3f1f-4993-99c5-fb3746959872"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 18, 12, 59, 40, 992, DateTimeKind.Utc).AddTicks(6133), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("265c36b0-c3b6-4a1d-a374-9769e7c0e2b7"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("3daf4cdb-eac4-4b11-b63a-f08a2b2fb5f9"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4647b50e-5d18-4920-bd4f-953ba60e033d"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("4adcfe6c-8b0a-440e-9eb3-9fd10a78d82f"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("724ec9bc-c40c-43cc-a5d8-6704eb2626d8"));

            migrationBuilder.DeleteData(
                table: "Features",
                keyColumn: "Id",
                keyValue: new Guid("e00cbd24-7e96-4074-85c9-10438a662c89"));

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("c6fa742d-d972-4402-befa-557476744d6a"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("1fb69ad4-226d-4a81-9d8c-316b691ea51c"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("20f68550-6270-4c79-a95e-4d85471b679d"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("bfd3dee5-ca75-4695-a524-82b5a39e8151"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("cf9b052f-3f1f-4993-99c5-fb3746959872"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("8c8b4914-6e5d-4487-af3c-ad4a84155cf3"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("5b505cd1-81f1-4ad4-963c-b4ea37fa9846"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 18, 12, 9, 3, 265, DateTimeKind.Utc).AddTicks(5429), null },
                    { new Guid("71c8dfb9-bd63-4f73-a3d3-1039e69a2bf0"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 18, 12, 9, 3, 265, DateTimeKind.Utc).AddTicks(5432), null },
                    { new Guid("73893ea9-6bcc-44b6-abc5-60c4802cdbb9"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 18, 12, 9, 3, 265, DateTimeKind.Utc).AddTicks(5434), null },
                    { new Guid("bbcf9e17-080e-4c92-b226-2ae4dea2c73e"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 18, 12, 9, 3, 265, DateTimeKind.Utc).AddTicks(5436), null }
                });
        }
    }
}
