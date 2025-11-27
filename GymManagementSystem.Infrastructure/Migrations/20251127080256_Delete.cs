using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAvailabilityTemplates_Trainers_TrainerId",
                table: "TrainerAvailabilityTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerAvailabilityTemplates",
                table: "TrainerAvailabilityTemplates");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("e48c6670-aeb7-4f60-8f5c-e220c8527fd0"));

            migrationBuilder.RenameTable(
                name: "TrainerAvailabilityTemplates",
                newName: "TrainerAvailabilityTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerAvailabilityTemplates_TrainerId",
                table: "TrainerAvailabilityTemplate",
                newName: "IX_TrainerAvailabilityTemplate_TrainerId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CloseTime",
                table: "GeneralGymDetails",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpenTime",
                table: "GeneralGymDetails",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerAvailabilityTemplate",
                table: "TrainerAvailabilityTemplate",
                column: "Id");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("f72219e1-62b9-43af-83ac-0b6628242fd0"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAvailabilityTemplate_Trainers_TrainerId",
                table: "TrainerAvailabilityTemplate",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainerAvailabilityTemplate_Trainers_TrainerId",
                table: "TrainerAvailabilityTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainerAvailabilityTemplate",
                table: "TrainerAvailabilityTemplate");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("f72219e1-62b9-43af-83ac-0b6628242fd0"));

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "GeneralGymDetails");

            migrationBuilder.DropColumn(
                name: "OpenTime",
                table: "GeneralGymDetails");

            migrationBuilder.RenameTable(
                name: "TrainerAvailabilityTemplate",
                newName: "TrainerAvailabilityTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_TrainerAvailabilityTemplate_TrainerId",
                table: "TrainerAvailabilityTemplates",
                newName: "IX_TrainerAvailabilityTemplates_TrainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainerAvailabilityTemplates",
                table: "TrainerAvailabilityTemplates",
                column: "Id");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "ContactNumber", "GymName", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("e48c6670-aeb7-4f60-8f5c-e220c8527fd0"), "123 Fitness St, Muscle City", "#363740", "123456789", "NextLevelGym", "#EEEEEE", "#9AAD00" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainerAvailabilityTemplates_Trainers_TrainerId",
                table: "TrainerAvailabilityTemplates",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
