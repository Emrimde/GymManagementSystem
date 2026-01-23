using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("25100553-7d6a-4112-bf4b-1092ad6f7817"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("26d60112-4074-44a1-883f-50af918b51f5"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("34f45cef-73bd-47c4-beb8-ad8141ab0f6a"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("458136b6-a0bb-43ed-bb84-23d7280d2661"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("a1f27e5b-9a82-4529-9a36-65a2ae661f8d"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("a34d8bb9-60c8-42f1-9dd1-2393341bda26"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("19142462-396a-442a-bb06-97ea12de5054"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 23, 8, 13, 8, 554, DateTimeKind.Utc).AddTicks(9629), null },
                    { new Guid("1cfbded3-04e1-4d32-8ba4-04c82e55741c"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 23, 8, 13, 8, 554, DateTimeKind.Utc).AddTicks(9611), null },
                    { new Guid("735ace4e-a6c9-4ad8-89b5-91ab579eef46"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 23, 8, 13, 8, 554, DateTimeKind.Utc).AddTicks(9631), null },
                    { new Guid("c18dc1eb-3187-469f-ad7a-37538ea8f869"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 23, 8, 13, 8, 554, DateTimeKind.Utc).AddTicks(9618), null }
                });

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"),
                column: "FreePersonalTrainingSessions",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"),
                column: "FreePersonalTrainingSessions",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"),
                column: "FreePersonalTrainingSessions",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"),
                column: "FreePersonalTrainingSessions",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("a34d8bb9-60c8-42f1-9dd1-2393341bda26"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("19142462-396a-442a-bb06-97ea12de5054"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("1cfbded3-04e1-4d32-8ba4-04c82e55741c"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("735ace4e-a6c9-4ad8-89b5-91ab579eef46"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("c18dc1eb-3187-469f-ad7a-37538ea8f869"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("25100553-7d6a-4112-bf4b-1092ad6f7817"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("26d60112-4074-44a1-883f-50af918b51f5"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 22, 15, 41, 43, 196, DateTimeKind.Utc).AddTicks(1966), null },
                    { new Guid("34f45cef-73bd-47c4-beb8-ad8141ab0f6a"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 22, 15, 41, 43, 196, DateTimeKind.Utc).AddTicks(1955), null },
                    { new Guid("458136b6-a0bb-43ed-bb84-23d7280d2661"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 22, 15, 41, 43, 196, DateTimeKind.Utc).AddTicks(1952), null },
                    { new Guid("a1f27e5b-9a82-4529-9a36-65a2ae661f8d"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 22, 15, 41, 43, 196, DateTimeKind.Utc).AddTicks(1947), null }
                });

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"),
                column: "FreePersonalTrainingSessions",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"),
                column: "FreePersonalTrainingSessions",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"),
                column: "FreePersonalTrainingSessions",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"),
                column: "FreePersonalTrainingSessions",
                value: 3);
        }
    }
}
