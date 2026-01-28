using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("3ba6b452-1354-4155-9b5c-f09c2f2e2300"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("5cd7f3c9-8016-4ca2-808e-3b0c7eaf0fae"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("79da32ad-ea71-47b7-849b-427b22f165da"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("80f3ac99-aaa2-4e63-a036-082ead3790fe"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("fe6d488f-b0dc-4bc1-a7c4-2fc2dd3202aa"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("603412c8-b05f-4b24-b36f-e0e864b490ec"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("109b1b5f-6d53-4dec-a59b-d4650a3774ff"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 28, 17, 14, 39, 238, DateTimeKind.Utc).AddTicks(7528), null },
                    { new Guid("1789c117-bfbf-47f9-b36c-71d18a1a21d9"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 28, 17, 14, 39, 238, DateTimeKind.Utc).AddTicks(7526), null },
                    { new Guid("ade15150-5b88-4640-b29c-d3e3458dc65a"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 28, 17, 14, 39, 238, DateTimeKind.Utc).AddTicks(7522), null },
                    { new Guid("f62388fd-ed22-4c97-9a29-270cad65b5eb"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 28, 17, 14, 39, 238, DateTimeKind.Utc).AddTicks(7530), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("603412c8-b05f-4b24-b36f-e0e864b490ec"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("109b1b5f-6d53-4dec-a59b-d4650a3774ff"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("1789c117-bfbf-47f9-b36c-71d18a1a21d9"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("ade15150-5b88-4640-b29c-d3e3458dc65a"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("f62388fd-ed22-4c97-9a29-270cad65b5eb"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("3ba6b452-1354-4155-9b5c-f09c2f2e2300"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("5cd7f3c9-8016-4ca2-808e-3b0c7eaf0fae"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 27, 17, 3, 17, 774, DateTimeKind.Utc).AddTicks(5238), null },
                    { new Guid("79da32ad-ea71-47b7-849b-427b22f165da"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 27, 17, 3, 17, 774, DateTimeKind.Utc).AddTicks(5240), null },
                    { new Guid("80f3ac99-aaa2-4e63-a036-082ead3790fe"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 27, 17, 3, 17, 774, DateTimeKind.Utc).AddTicks(5223), null },
                    { new Guid("fe6d488f-b0dc-4bc1-a7c4-2fc2dd3202aa"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 27, 17, 3, 17, 774, DateTimeKind.Utc).AddTicks(5236), null }
                });
        }
    }
}
