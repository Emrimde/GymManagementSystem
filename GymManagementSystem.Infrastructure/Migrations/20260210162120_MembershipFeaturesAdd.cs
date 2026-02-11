using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MembershipFeaturesAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("05230497-acf5-4602-80fc-af33084d1879"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("0ba319af-3cc5-47c1-8dfd-cbc0387e04f2"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("16869cfb-f964-4e61-bdb7-d5737439bed0"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("23502314-3b3f-4eb5-ba40-f3168bc7a516"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("276452c0-cff0-4aed-876e-42daea7c142c"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("2a3ce557-480a-4196-9d1b-84cfa349c7a6"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("34d2ed7e-9526-481d-8824-8a506ccfa02c"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("35409743-e3e1-40d1-8341-c43e368f79dc"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("41ec34d1-93de-4964-902e-500c03275d8c"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("4e1f9276-97f1-4a44-ae32-b8e814058138"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("521cf410-a121-4ffb-8eeb-9efacb55efa8"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("7becaea1-c021-4f2f-84cb-26d21f1beb81"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("8500b296-5b9a-4f07-8cf7-e45c9e3a636c"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("87060f26-608a-46fc-b8b9-19a6576daad9"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("8d748863-72d0-407a-a795-00f183798728"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("94598c26-1484-4091-983c-9ee62d08a50f"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("c6a3c78e-f3ac-4159-a421-2ae2e0f843b1"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("def8e04a-484c-4839-811a-4e831f6897f0"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("e8c5412b-b5db-4faa-a37c-4930a84d7b67"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("fbea94d5-bdfc-4cca-9b5b-59e53b8a4400"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("05230497-acf5-4602-80fc-af33084d1879"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0ba319af-3cc5-47c1-8dfd-cbc0387e04f2"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("16869cfb-f964-4e61-bdb7-d5737439bed0"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("23502314-3b3f-4eb5-ba40-f3168bc7a516"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("276452c0-cff0-4aed-876e-42daea7c142c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2a3ce557-480a-4196-9d1b-84cfa349c7a6"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("34d2ed7e-9526-481d-8824-8a506ccfa02c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("35409743-e3e1-40d1-8341-c43e368f79dc"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("41ec34d1-93de-4964-902e-500c03275d8c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4e1f9276-97f1-4a44-ae32-b8e814058138"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("521cf410-a121-4ffb-8eeb-9efacb55efa8"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7becaea1-c021-4f2f-84cb-26d21f1beb81"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8500b296-5b9a-4f07-8cf7-e45c9e3a636c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("87060f26-608a-46fc-b8b9-19a6576daad9"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8d748863-72d0-407a-a795-00f183798728"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("94598c26-1484-4091-983c-9ee62d08a50f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c6a3c78e-f3ac-4159-a421-2ae2e0f843b1"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("def8e04a-484c-4839-811a-4e831f6897f0"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e8c5412b-b5db-4faa-a37c-4930a84d7b67"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("fbea94d5-bdfc-4cca-9b5b-59e53b8a4400"));
        }
    }
}
