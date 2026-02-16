using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("02ba27e2-0ee5-49ab-ba3d-345f21b3c385"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("23530b84-0153-4048-8471-9a37cf1f631d"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("4ab370ee-5de7-4766-8f9d-4f330cc04224"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("4ac113fb-a62b-4f26-89ce-db18b1b65c6b"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("5d5c6e9d-f489-4d55-acdc-6027cd5571d6"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("67184db6-ecb1-41cd-9acb-f6aedd665acc"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("6ac75645-311b-4a21-bfa0-a1d63241d4ee"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("6ed15175-5258-4610-a22b-e97644e16335"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("8e4665b9-e4a8-4e6d-a99e-0269ad7f97e8"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("97ccec05-9a3d-4109-b8b2-74932d4f539b"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("aa87c5b9-9d57-4693-b607-618d34bc76ee"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("bb5f7ba8-2983-4671-ba86-ab1cb1e7c6d5"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("bd98286f-0d46-4ccd-8ba4-41758a69ca8f"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("be096096-e23e-4d58-bda5-029c8e548d60"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("c4658526-88ba-4267-a1c9-e3d9619918a2"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("dc03e7b0-8815-49b5-a11a-5c0267807260"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("ddbd8972-e04b-419e-91df-60e478a18b8a"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("e2c40d19-11e6-4f3e-82fa-786a2a54980a"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("e318bd2f-2d60-46f2-8ad7-10e1fc1fc0d9"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("fe954cb7-6e94-4618-8a1b-1df21bdecac4"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("02ba27e2-0ee5-49ab-ba3d-345f21b3c385"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("23530b84-0153-4048-8471-9a37cf1f631d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4ab370ee-5de7-4766-8f9d-4f330cc04224"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4ac113fb-a62b-4f26-89ce-db18b1b65c6b"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5d5c6e9d-f489-4d55-acdc-6027cd5571d6"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("67184db6-ecb1-41cd-9acb-f6aedd665acc"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6ac75645-311b-4a21-bfa0-a1d63241d4ee"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6ed15175-5258-4610-a22b-e97644e16335"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8e4665b9-e4a8-4e6d-a99e-0269ad7f97e8"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("97ccec05-9a3d-4109-b8b2-74932d4f539b"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("aa87c5b9-9d57-4693-b607-618d34bc76ee"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bb5f7ba8-2983-4671-ba86-ab1cb1e7c6d5"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("bd98286f-0d46-4ccd-8ba4-41758a69ca8f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("be096096-e23e-4d58-bda5-029c8e548d60"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c4658526-88ba-4267-a1c9-e3d9619918a2"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("dc03e7b0-8815-49b5-a11a-5c0267807260"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ddbd8972-e04b-419e-91df-60e478a18b8a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e2c40d19-11e6-4f3e-82fa-786a2a54980a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e318bd2f-2d60-46f2-8ad7-10e1fc1fc0d9"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("fe954cb7-6e94-4618-8a1b-1df21bdecac4"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

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
    }
}
