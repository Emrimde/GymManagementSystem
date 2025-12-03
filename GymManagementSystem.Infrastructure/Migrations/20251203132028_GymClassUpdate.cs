using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GymClassUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("3637bc31-d46b-40cf-83d7-35bff624f8a4"));

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GymClasses");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("ce4e6fa8-7af3-4038-9e9e-6b9008982d73"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("ce4e6fa8-7af3-4038-9e9e-6b9008982d73"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "GymClasses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("3637bc31-d46b-40cf-83d7-35bff624f8a4"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });
        }
    }
}
