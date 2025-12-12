using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ClientMembershipTermination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminations_Clients_ClientId",
                table: "Terminations");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminations_Contracts_ContractId",
                table: "Terminations");

            migrationBuilder.DropIndex(
                name: "IX_Terminations_ClientId",
                table: "Terminations");

            migrationBuilder.DropIndex(
                name: "IX_Terminations_ContractId",
                table: "Terminations");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("6847e6c3-281a-4b3b-9043-acb1226154e3"));

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Terminations");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Terminations",
                newName: "ClientMembershipId");

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("aa6b082e-67dc-4291-8ea0-6b04eaceeafa"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_ClientMembershipId",
                table: "Terminations",
                column: "ClientMembershipId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminations_ClientMemberships_ClientMembershipId",
                table: "Terminations",
                column: "ClientMembershipId",
                principalTable: "ClientMemberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Terminations_ClientMemberships_ClientMembershipId",
                table: "Terminations");

            migrationBuilder.DropIndex(
                name: "IX_Terminations_ClientMembershipId",
                table: "Terminations");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("aa6b082e-67dc-4291-8ea0-6b04eaceeafa"));

            migrationBuilder.RenameColumn(
                name: "ClientMembershipId",
                table: "Terminations",
                newName: "ContractId");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Terminations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("6847e6c3-281a-4b3b-9043-acb1226154e3"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_ClientId",
                table: "Terminations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_ContractId",
                table: "Terminations",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminations_Clients_ClientId",
                table: "Terminations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Terminations_Contracts_ContractId",
                table: "Terminations",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
