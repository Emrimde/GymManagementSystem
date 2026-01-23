using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MembershipFeatureUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
                table: "MembershipFeatures");

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

            migrationBuilder.DropColumn(
                name: "BenefitFrequency",
                table: "MembershipFeatures");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "MembershipFeatures");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "MembershipFeatures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "FeatureDescription",
                table: "MembershipFeatures",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
                table: "MembershipFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
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
                name: "FeatureDescription",
                table: "MembershipFeatures");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeatureId",
                table: "MembershipFeatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BenefitFrequency",
                table: "MembershipFeatures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "MembershipFeatures",
                type: "integer",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MembershipFeatures_Features_FeatureId",
                table: "MembershipFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
