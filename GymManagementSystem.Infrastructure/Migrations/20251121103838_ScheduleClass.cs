using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("b6bdfde2-41f7-4488-a8d6-7d8ed67102cf"));

            migrationBuilder.CreateTable(
                name: "ScheduledClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GymClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartFrom = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartTo = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MaxPeople = table.Column<int>(type: "integer", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledClasses_GymClasses_GymClassId",
                        column: x => x.GymClassId,
                        principalTable: "GymClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("9fb5b8df-f194-4a64-a33a-ec96bde80eb3"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledClasses_GymClassId",
                table: "ScheduledClasses",
                column: "GymClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledClasses");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("9fb5b8df-f194-4a64-a33a-ec96bde80eb3"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("b6bdfde2-41f7-4488-a8d6-7d8ed67102cf"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });
        }
    }
}
