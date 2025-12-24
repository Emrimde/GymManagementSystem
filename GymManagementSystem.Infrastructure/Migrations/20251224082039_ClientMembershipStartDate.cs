using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientMembershipStartDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("52076f42-2f43-43d2-b048-d245123e508d"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("25791b79-d638-4b12-a9fa-0ccfda7dddd8"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("4c741432-c1d6-4c5d-9efd-1e329e820b05"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("7454a10e-1229-4177-8b80-b21826f810cf"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("f3f8114d-6a57-4cdd-9142-b1482660b630"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClientMemberships");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("7c4edeaf-8294-4694-805f-46b4b1a8680f"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("1f1032ae-8bc0-4163-9ff4-fdb33408c390"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 24, 8, 20, 39, 167, DateTimeKind.Utc).AddTicks(766), null },
                    { new Guid("275afc97-cf3b-4508-8e5d-024169103e6d"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 24, 8, 20, 39, 167, DateTimeKind.Utc).AddTicks(762), null },
                    { new Guid("5c1b5a47-8346-427f-b604-bff437cd72ba"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 24, 8, 20, 39, 167, DateTimeKind.Utc).AddTicks(764), null },
                    { new Guid("61b2a574-fd9c-423a-b86b-a2a102e7e23f"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 24, 8, 20, 39, 167, DateTimeKind.Utc).AddTicks(757), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("7c4edeaf-8294-4694-805f-46b4b1a8680f"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("1f1032ae-8bc0-4163-9ff4-fdb33408c390"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("275afc97-cf3b-4508-8e5d-024169103e6d"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("5c1b5a47-8346-427f-b604-bff437cd72ba"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("61b2a574-fd9c-423a-b86b-a2a102e7e23f"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClientMemberships",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("52076f42-2f43-43d2-b048-d245123e508d"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("25791b79-d638-4b12-a9fa-0ccfda7dddd8"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 21, 13, 18, 4, 660, DateTimeKind.Utc).AddTicks(5669), null },
                    { new Guid("4c741432-c1d6-4c5d-9efd-1e329e820b05"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 21, 13, 18, 4, 660, DateTimeKind.Utc).AddTicks(5682), null },
                    { new Guid("7454a10e-1229-4177-8b80-b21826f810cf"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 21, 13, 18, 4, 660, DateTimeKind.Utc).AddTicks(5665), null },
                    { new Guid("f3f8114d-6a57-4cdd-9142-b1482660b630"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 21, 13, 18, 4, 660, DateTimeKind.Utc).AddTicks(5671), null }
                });
        }
    }
}
