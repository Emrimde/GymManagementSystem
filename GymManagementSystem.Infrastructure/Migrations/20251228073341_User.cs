using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("a40a2664-a7ca-4f8a-8b6a-9d6f637c309f"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("7c86adfb-f0a1-41d6-9260-17d044214f6a"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 28, 7, 33, 41, 167, DateTimeKind.Utc).AddTicks(3266), null },
                    { new Guid("82ce6a7f-7e7e-466a-93cd-29d7baa79ada"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 28, 7, 33, 41, 167, DateTimeKind.Utc).AddTicks(3255), null },
                    { new Guid("ace28281-3640-4e45-a2e4-7b57857e8dff"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 28, 7, 33, 41, 167, DateTimeKind.Utc).AddTicks(3268), null },
                    { new Guid("d2485fa1-aa06-4abc-b73c-6049cf7c2665"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 28, 7, 33, 41, 167, DateTimeKind.Utc).AddTicks(3263), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers",
                column: "ClientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Clients_ClientId",
                table: "AspNetUsers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Clients_ClientId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("a40a2664-a7ca-4f8a-8b6a-9d6f637c309f"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("7c86adfb-f0a1-41d6-9260-17d044214f6a"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("82ce6a7f-7e7e-466a-93cd-29d7baa79ada"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("ace28281-3640-4e45-a2e4-7b57857e8dff"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("d2485fa1-aa06-4abc-b73c-6049cf7c2665"));

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetUsers");

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
    }
}
