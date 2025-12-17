using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MembershipPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("7f1dade8-0315-41b5-be2e-696c71205666"));

            migrationBuilder.DropColumn(
                name: "MemberhsipId",
                table: "MembershipPrices");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("77b40ca5-6014-4e2a-990e-d587953021fd"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "IsVisibleOffer", "MembershipType", "Name" },
                values: new object[,]
                {
                    { new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), true, 0, "Silver Membership" },
                    { new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), true, 1, "Silver Membership" },
                    { new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), true, 0, "Gold Membership" },
                    { new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), true, 1, "Gold Membership" }
                });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("3222b689-8d09-490c-8731-96717a710e07"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 17, 18, 6, 1, 290, DateTimeKind.Utc).AddTicks(9702), null },
                    { new Guid("7191cac9-5b1d-4416-a70d-dc694f9911ae"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 17, 18, 6, 1, 290, DateTimeKind.Utc).AddTicks(9709), null },
                    { new Guid("dabf4ecc-127c-4c63-b7a9-99a2fad90756"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 17, 18, 6, 1, 290, DateTimeKind.Utc).AddTicks(9713), null },
                    { new Guid("ec882a0a-3e04-448f-b2b1-93e7c2fc72ff"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 17, 18, 6, 1, 290, DateTimeKind.Utc).AddTicks(9711), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("77b40ca5-6014-4e2a-990e-d587953021fd"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("3222b689-8d09-490c-8731-96717a710e07"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("7191cac9-5b1d-4416-a70d-dc694f9911ae"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("dabf4ecc-127c-4c63-b7a9-99a2fad90756"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("ec882a0a-3e04-448f-b2b1-93e7c2fc72ff"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"));

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"));

            migrationBuilder.AddColumn<Guid>(
                name: "MemberhsipId",
                table: "MembershipPrices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("7f1dade8-0315-41b5-be2e-696c71205666"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });
        }
    }
}
