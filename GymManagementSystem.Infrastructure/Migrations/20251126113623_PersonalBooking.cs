using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PersonalBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainerAvailabilityTemplates_TrainerId",
                table: "TrainerAvailabilityTemplates");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("268a2c2d-f57c-47a1-9b0b-18551819dc56"));

            migrationBuilder.CreateTable(
                name: "PersonalBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalBookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalBookings_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerTimeOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTimeOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerTimeOff_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("e48c6670-aeb7-4f60-8f5c-e220c8527fd0"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAvailabilityTemplates_TrainerId",
                table: "TrainerAvailabilityTemplates",
                column: "TrainerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_ClientId",
                table: "PersonalBookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_TrainerId",
                table: "PersonalBookings",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTimeOff_TrainerId",
                table: "TrainerTimeOff",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalBookings");

            migrationBuilder.DropTable(
                name: "TrainerTimeOff");

            migrationBuilder.DropIndex(
                name: "IX_TrainerAvailabilityTemplates_TrainerId",
                table: "TrainerAvailabilityTemplates");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("e48c6670-aeb7-4f60-8f5c-e220c8527fd0"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("268a2c2d-f57c-47a1-9b0b-18551819dc56"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerAvailabilityTemplates_TrainerId",
                table: "TrainerAvailabilityTemplates",
                column: "TrainerId");
        }
    }
}
