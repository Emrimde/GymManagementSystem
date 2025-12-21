using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "ContractDocumentPath",
                table: "TrainerContracts");

            migrationBuilder.RenameColumn(
                name: "IsSigned",
                table: "Employees",
                newName: "IsActive");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("7bb4f95c-ba1c-4298-a034-8bd5ba682a28"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("60103f8f-fdac-4015-b44f-63c4a6b6ddbd"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2025, 12, 21, 10, 44, 56, 894, DateTimeKind.Utc).AddTicks(9759), null },
                    { new Guid("81c6950e-6629-4881-8f22-273a132223e7"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2025, 12, 21, 10, 44, 56, 894, DateTimeKind.Utc).AddTicks(9745), null },
                    { new Guid("c62b9abe-4498-4389-9c45-33322f39d26b"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2025, 12, 21, 10, 44, 56, 894, DateTimeKind.Utc).AddTicks(9741), null },
                    { new Guid("e0eebff7-1125-4861-a270-6183203c94d3"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2025, 12, 21, 10, 44, 56, 894, DateTimeKind.Utc).AddTicks(9748), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("7bb4f95c-ba1c-4298-a034-8bd5ba682a28"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("60103f8f-fdac-4015-b44f-63c4a6b6ddbd"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("81c6950e-6629-4881-8f22-273a132223e7"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("c62b9abe-4498-4389-9c45-33322f39d26b"));

            migrationBuilder.DeleteData(
                table: "MembershipPrices",
                keyColumn: "Id",
                keyValue: new Guid("e0eebff7-1125-4861-a270-6183203c94d3"));

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Employees",
                newName: "IsSigned");

            migrationBuilder.AddColumn<string>(
                name: "ContractDocumentPath",
                table: "TrainerContracts",
                type: "text",
                nullable: true);

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
    }
}
