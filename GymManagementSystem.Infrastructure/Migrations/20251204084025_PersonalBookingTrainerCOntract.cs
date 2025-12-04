using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PersonalBookingTrainerCOntract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                table: "PersonalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("07224e4c-062d-471e-af6a-2f2aa47792a3"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerContractId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("18244050-bf10-4ab5-9e6d-487b1d91a188"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                table: "PersonalBookings",
                column: "TrainerContractId",
                principalTable: "TrainerContracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                table: "PersonalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("18244050-bf10-4ab5-9e6d-487b1d91a188"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerContractId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("07224e4c-062d-471e-af6a-2f2aa47792a3"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                table: "PersonalBookings",
                column: "TrainerContractId",
                principalTable: "TrainerContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
