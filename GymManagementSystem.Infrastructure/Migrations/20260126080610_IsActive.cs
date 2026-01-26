using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
                table: "MembershipFeatures");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropIndex(
                name: "IX_MembershipFeatures_FeatureId",
                table: "MembershipFeatures");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("0f8d8085-4b27-429a-ad6d-dde455b1f18e"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("0d5e6670-f4b5-41af-aa07-ad5d364f04a4"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("3bc869aa-4f81-40d7-8a3a-b71b9de373f8"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("3f74150c-8c0b-4ab1-a37b-8a73275fbd26"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("d2926983-1d35-493e-8724-f08480ef3bb1"));

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "MembershipFeatures");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("e2e9b009-324a-4351-80d3-9ad58c6ddce0"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("29ac279f-7c44-472e-9856-e14081db8abf"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 26, 8, 6, 9, 853, DateTimeKind.Utc).AddTicks(8609), null },
                    { new Guid("2e05626b-9f43-4c13-ac8c-9f110c7e7905"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 26, 8, 6, 9, 853, DateTimeKind.Utc).AddTicks(8616), null },
                    { new Guid("a04118b3-b618-4d5a-81f4-344d05f6b238"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 26, 8, 6, 9, 853, DateTimeKind.Utc).AddTicks(8618), null },
                    { new Guid("e32ef267-39ff-4e36-a0c0-5c3ec337bcac"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 26, 8, 6, 9, 853, DateTimeKind.Utc).AddTicks(8614), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("e2e9b009-324a-4351-80d3-9ad58c6ddce0"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("29ac279f-7c44-472e-9856-e14081db8abf"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("2e05626b-9f43-4c13-ac8c-9f110c7e7905"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("a04118b3-b618-4d5a-81f4-344d05f6b238"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("e32ef267-39ff-4e36-a0c0-5c3ec337bcac"));

            migrationBuilder.AddColumn<Guid>(
                name: "FeatureId",
                table: "MembershipFeatures",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BenefitDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

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
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("0f8d8085-4b27-429a-ad6d-dde455b1f18e"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("0d5e6670-f4b5-41af-aa07-ad5d364f04a4"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 23, 8, 45, 36, 664, DateTimeKind.Utc).AddTicks(8516), null },
                    { new Guid("3bc869aa-4f81-40d7-8a3a-b71b9de373f8"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 23, 8, 45, 36, 664, DateTimeKind.Utc).AddTicks(8507), null },
                    { new Guid("3f74150c-8c0b-4ab1-a37b-8a73275fbd26"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 23, 8, 45, 36, 664, DateTimeKind.Utc).AddTicks(8514), null },
                    { new Guid("d2926983-1d35-493e-8724-f08480ef3bb1"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 23, 8, 45, 36, 664, DateTimeKind.Utc).AddTicks(8512), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFeatures_FeatureId",
                table: "MembershipFeatures",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
                table: "MembershipFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");
        }
    }
}
