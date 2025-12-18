using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MembershipFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "MembershipFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureId = table.Column<Guid>(type: "uuid", nullable: false),
                    BenefitFrequency = table.Column<int>(type: "integer", nullable: true),
                    Period = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembershipFeatures_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFeatures_FeatureId",
                table: "MembershipFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFeatures_MembershipId",
                table: "MembershipFeatures",
                column: "MembershipId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipFeatures");

            migrationBuilder.DropTable(
                name: "Features");

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
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("77b40ca5-6014-4e2a-990e-d587953021fd"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

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
    }
}
