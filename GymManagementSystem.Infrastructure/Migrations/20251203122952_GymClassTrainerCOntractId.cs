using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GymClassTrainerCOntractId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymClasses_Trainers_TrainerId",
                table: "GymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("297e9dfd-8843-4d0a-97b6-b031a57bb64a"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerContractId",
                table: "PersonalBookings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "GymClasses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerContractId",
                table: "GymClasses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("e4a7e3ba-0aa4-4fbe-8499-ee583f6c8ebf"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_TrainerContractId",
                table: "PersonalBookings",
                column: "TrainerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_GymClasses_TrainerContractId",
                table: "GymClasses",
                column: "TrainerContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_GymClasses_TrainerContracts_TrainerContractId",
                table: "GymClasses",
                column: "TrainerContractId",
                principalTable: "TrainerContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GymClasses_Trainers_TrainerId",
                table: "GymClasses",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GymClasses_TrainerContracts_TrainerContractId",
                table: "GymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_GymClasses_Trainers_TrainerId",
                table: "GymClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                table: "PersonalBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings");

            migrationBuilder.DropIndex(
                name: "IX_PersonalBookings_TrainerContractId",
                table: "PersonalBookings");

            migrationBuilder.DropIndex(
                name: "IX_GymClasses_TrainerContractId",
                table: "GymClasses");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("e4a7e3ba-0aa4-4fbe-8499-ee583f6c8ebf"));

            migrationBuilder.DropColumn(
                name: "TrainerContractId",
                table: "PersonalBookings");

            migrationBuilder.DropColumn(
                name: "TrainerContractId",
                table: "GymClasses");

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
                name: "TrainerId",
                table: "GymClasses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("297e9dfd-8843-4d0a-97b6-b031a57bb64a"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.AddForeignKey(
                name: "FK_GymClasses_Trainers_TrainerId",
                table: "GymClasses",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalBookings_Trainers_TrainerId",
                table: "PersonalBookings",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
