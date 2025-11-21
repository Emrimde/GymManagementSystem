using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduledClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("eca1cb7a-3e97-4d19-bf8a-5036db822a93"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("b6bdfde2-41f7-4488-a8d6-7d8ed67102cf"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("b6bdfde2-41f7-4488-a8d6-7d8ed67102cf"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("eca1cb7a-3e97-4d19-bf8a-5036db822a93"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });
        }
    }
}
