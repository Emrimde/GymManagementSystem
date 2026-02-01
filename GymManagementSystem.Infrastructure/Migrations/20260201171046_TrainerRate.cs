using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrainerRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrainerRateId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_TrainerRateId",
                table: "PersonalBookings",
                column: "TrainerRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_TrainerRates_TrainerRateId",
                table: "PersonalBookings",
                column: "TrainerRateId",
                principalTable: "TrainerRates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_TrainerRates_TrainerRateId",
                table: "PersonalBookings");

            migrationBuilder.DropIndex(
                name: "IX_PersonalBookings_TrainerRateId",
                table: "PersonalBookings");

            migrationBuilder.DropColumn(
                name: "TrainerRateId",
                table: "PersonalBookings");
        }
    }
}
