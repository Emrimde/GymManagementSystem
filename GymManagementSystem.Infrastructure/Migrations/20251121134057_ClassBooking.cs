using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClassBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("9fb5b8df-f194-4a64-a33a-ec96bde80eb3"));

            migrationBuilder.CreateTable(
                name: "ClassBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduledClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassBookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassBookings_ScheduledClasses_ScheduledClassId",
                        column: x => x.ScheduledClassId,
                        principalTable: "ScheduledClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("be81bc89-7246-4e5a-9ccc-7f4f7f4125a7"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_ClientId",
                table: "ClassBookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_ScheduledClassId",
                table: "ClassBookings",
                column: "ScheduledClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassBookings");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("be81bc89-7246-4e5a-9ccc-7f4f7f4125a7"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("9fb5b8df-f194-4a64-a33a-ec96bde80eb3"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });
        }
    }
}
