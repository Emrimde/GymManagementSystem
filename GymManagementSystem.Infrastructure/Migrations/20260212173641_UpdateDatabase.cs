using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("09ec454e-0bdd-49da-987b-9626dda236f5"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("15a3df05-8281-40da-874e-b178e76757ec"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1b1b7a54-38c5-4ca6-8b1b-25d37885f98a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2b9a788c-c753-45cc-ac92-5d172138d50d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3c524385-9b3a-4932-8fe4-67d5b512fdd8"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3ce82aef-9e9c-4b01-bdc7-cf2820039dcd"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("41310350-09f0-4bb3-b94e-f432195f566c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("474738f8-7409-4e82-9dc0-7c5006fa5826"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4b45ff6e-62a2-435c-b857-a9608c99760d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4d1d4d9e-003c-4689-8457-a4ec7fea77fb"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("57516763-6340-4930-ab64-d5410c51f11f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6d72f6e0-1e11-4678-bcc2-d5ac76876c5e"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("79bc0812-122f-46b9-b08b-432badb1f5a7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9129ede4-5969-4e03-86ce-6f3dd1e7ea30"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9a00a14e-4528-46f0-bef6-fa7e78f49c04"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9d90b359-bf7b-4d87-b997-e3b5acc50d7d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a0d431cc-dd31-4ab9-9a41-bdf3666747bd"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bc5809f7-7af0-480b-89e2-d3d00602712a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cfce94cd-b60a-441e-995d-5c394ae6dba7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f371af89-9e6d-4e28-ab11-0f64ab0d83d9"));

            migrationBuilder.DropColumn(
                name: "FreePersonalTrainingSessions",
                table: "Memberships");

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("15f0903e-cc0a-4917-a480-f7f60003be43"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("19cf53d5-bec4-4ded-a9ec-d21ae237a421"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("1b70f2c1-fcb9-48fb-84e1-45061a9ad4c0"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("2a34dfb2-c330-4021-ba98-64a5e10d0fd4"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("2a5c35fa-57a9-42ea-9a30-ee445a068101"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("4ba6dd2f-50e0-4603-b641-9ac6fa38e259"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("7619ff97-103c-4552-8e01-3bb8f87b48cb"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("79a3d8de-2407-471b-9fe6-3fa0bcc1e022"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("91088648-2e2e-4588-8c03-912f3dd24ad7"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("94f1a48c-17a4-4aa9-b56b-1907d7859962"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("971e217d-1a8b-48e5-8ccf-d723037ec32d"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("c40af803-6dd7-4dd3-b011-31646fb9c169"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("cc46bb86-d241-44a1-8344-79816b83854f"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("ccb27066-d95f-4dad-b065-8d1e2bd30f85"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("d5238111-cede-442c-8db4-6c7183f1df3d"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("d811dd2e-6829-49d7-938b-a0196652b30f"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("e434c5d9-5925-4a6f-b9eb-ffccb46acae8"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("e854447b-0d93-4d47-b76b-be45948bc004"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("f81bce1b-1453-41b0-81bf-3a738394f696"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("fde9bc6b-6167-412b-96a8-8000aa912c94"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("15f0903e-cc0a-4917-a480-f7f60003be43"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("19cf53d5-bec4-4ded-a9ec-d21ae237a421"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1b70f2c1-fcb9-48fb-84e1-45061a9ad4c0"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2a34dfb2-c330-4021-ba98-64a5e10d0fd4"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2a5c35fa-57a9-42ea-9a30-ee445a068101"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4ba6dd2f-50e0-4603-b641-9ac6fa38e259"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7619ff97-103c-4552-8e01-3bb8f87b48cb"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("79a3d8de-2407-471b-9fe6-3fa0bcc1e022"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("91088648-2e2e-4588-8c03-912f3dd24ad7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("94f1a48c-17a4-4aa9-b56b-1907d7859962"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("971e217d-1a8b-48e5-8ccf-d723037ec32d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c40af803-6dd7-4dd3-b011-31646fb9c169"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("cc46bb86-d241-44a1-8344-79816b83854f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ccb27066-d95f-4dad-b065-8d1e2bd30f85"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d5238111-cede-442c-8db4-6c7183f1df3d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d811dd2e-6829-49d7-938b-a0196652b30f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e434c5d9-5925-4a6f-b9eb-ffccb46acae8"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e854447b-0d93-4d47-b76b-be45948bc004"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f81bce1b-1453-41b0-81bf-3a738394f696"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("fde9bc6b-6167-412b-96a8-8000aa912c94"));

            migrationBuilder.AddColumn<int>(
                name: "FreePersonalTrainingSessions",
                table: "Memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("09ec454e-0bdd-49da-987b-9626dda236f5"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("15a3df05-8281-40da-874e-b178e76757ec"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("1b1b7a54-38c5-4ca6-8b1b-25d37885f98a"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("2b9a788c-c753-45cc-ac92-5d172138d50d"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("3c524385-9b3a-4932-8fe4-67d5b512fdd8"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("3ce82aef-9e9c-4b01-bdc7-cf2820039dcd"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("41310350-09f0-4bb3-b94e-f432195f566c"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("474738f8-7409-4e82-9dc0-7c5006fa5826"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("4b45ff6e-62a2-435c-b857-a9608c99760d"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("4d1d4d9e-003c-4689-8457-a4ec7fea77fb"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("57516763-6340-4930-ab64-d5410c51f11f"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("6d72f6e0-1e11-4678-bcc2-d5ac76876c5e"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("79bc0812-122f-46b9-b08b-432badb1f5a7"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("9129ede4-5969-4e03-86ce-6f3dd1e7ea30"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("9a00a14e-4528-46f0-bef6-fa7e78f49c04"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("9d90b359-bf7b-4d87-b997-e3b5acc50d7d"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("a0d431cc-dd31-4ab9-9a41-bdf3666747bd"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("bc5809f7-7af0-480b-89e2-d3d00602712a"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("cfce94cd-b60a-441e-995d-5c394ae6dba7"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("f371af89-9e6d-4e28-ab11-0f64ab0d83d9"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") }
                });

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"),
                column: "FreePersonalTrainingSessions",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"),
                column: "FreePersonalTrainingSessions",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"),
                column: "FreePersonalTrainingSessions",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"),
                column: "FreePersonalTrainingSessions",
                value: 2);
        }
    }
}
