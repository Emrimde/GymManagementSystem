using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmploymentTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentDetailsProfile");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("7c4b9d8b-b4f7-4fe5-b873-a8c271c585ef"));

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "EmploymentTemplate",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmploymentTemplateId",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmploymentTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    EmploymentType = table.Column<int>(type: "integer", nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "numeric", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: true),
                    NetRate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTemplates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("3ffdb55d-d0ce-4357-beb3-4e6737c04d17"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentTemplateId",
                table: "Employees",
                column: "EmploymentTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmploymentTemplates_EmploymentTemplateId",
                table: "Employees",
                column: "EmploymentTemplateId",
                principalTable: "EmploymentTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmploymentTemplates_EmploymentTemplateId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmploymentTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmploymentTemplateId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("3ffdb55d-d0ce-4357-beb3-4e6737c04d17"));

            migrationBuilder.DropColumn(
                name: "EmploymentTemplate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmploymentTemplateId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmploymentType",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmploymentDetailsProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmploymentType = table.Column<int>(type: "integer", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "numeric", nullable: true),
                    MonthlySalary = table.Column<decimal>(type: "numeric", nullable: true),
                    NetRate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentDetailsProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentDetailsProfile_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("7c4b9d8b-b4f7-4fe5-b873-a8c271c585ef"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentDetailsProfile_EmployeeId",
                table: "EmploymentDetailsProfile",
                column: "EmployeeId",
                unique: true);
        }
    }
}
