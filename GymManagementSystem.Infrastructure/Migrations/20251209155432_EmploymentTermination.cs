using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmploymentTermination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("e7887d54-ff1c-4f87-a142-d85682ced258"));

            migrationBuilder.CreateTable(
                name: "EmploymentTerminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSigned = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTerminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentTerminations_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("dfa86f35-8ce6-4063-890b-e5d455318d69"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentTerminations_PersonId",
                table: "EmploymentTerminations",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmploymentTerminations");

            migrationBuilder.DeleteData(
                table: "GeneralGymDetails",
                keyColumn: "Id",
                keyValue: new Guid("dfa86f35-8ce6-4063-890b-e5d455318d69"));

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("e7887d54-ff1c-4f87-a142-d85682ced258"), "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 0m, 150m, 100m, 120m, "NextLevelGym", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });
        }
    }
}
