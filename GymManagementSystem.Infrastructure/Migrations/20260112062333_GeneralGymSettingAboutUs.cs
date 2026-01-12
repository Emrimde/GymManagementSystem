using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GeneralGymSettingAboutUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "GeneralGymDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "GeneralGymDetails",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("cad2c5f2-ad52-4efe-9a94-064e07297c5e"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", null, new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("6d50642e-aa3a-4f99-a299-3320b99be894"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 12, 6, 23, 33, 515, DateTimeKind.Utc).AddTicks(5229), null },
                    { new Guid("807e60a0-a4db-48e4-9467-12e3b3e3ede6"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 12, 6, 23, 33, 515, DateTimeKind.Utc).AddTicks(5227), null },
                    { new Guid("c9eef06b-b7df-499a-a683-2eb3d6d6839b"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 12, 6, 23, 33, 515, DateTimeKind.Utc).AddTicks(5231), null },
                    { new Guid("d79d629e-5de6-409e-8cf1-b16b7ea556bb"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 12, 6, 23, 33, 515, DateTimeKind.Utc).AddTicks(5223), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("cad2c5f2-ad52-4efe-9a94-064e07297c5e"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("6d50642e-aa3a-4f99-a299-3320b99be894"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("807e60a0-a4db-48e4-9467-12e3b3e3ede6"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("c9eef06b-b7df-499a-a683-2eb3d6d6839b"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("d79d629e-5de6-409e-8cf1-b16b7ea556bb"));

            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "GeneralGymDetails");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "GeneralGymDetails");

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
        }
    }
}
