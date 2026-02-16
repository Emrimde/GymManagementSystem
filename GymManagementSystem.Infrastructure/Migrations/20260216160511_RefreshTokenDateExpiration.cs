using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenDateExpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("01db6faa-b9b0-4f70-b6cb-789eb1f9b070"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("074753c5-4986-4f84-b97d-1f9293d24967"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("0f8e1e69-d742-4412-9435-297480e86d48"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("2439adac-be86-4ea6-a3fb-7baefad214ce"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("29ddaff6-8103-4f35-a667-7827368eabb4"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("3907917d-d40d-4d44-9b7e-831d98f94fbd"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("3917131c-ff21-4efc-a125-46c6516b735a"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("39724848-5fff-4e09-a4ca-844d010b759d"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("3ff1f0f0-e768-4d8a-9f23-9ba7b9fbbf8f"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("5566dd97-5087-4347-9590-69432a19b4b4"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("5da2d35a-4278-416d-8f77-6388e26cf165"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("6848849c-24cb-44b1-936b-33e6dfa479c9"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("6f23eba6-9be2-4a04-bc22-9059205f79cd"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("a04619ed-3859-49e2-9189-d8e9d2161f0b"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("a9df85df-f059-494d-87f0-23ef1d521ce7"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("b7ba2807-b959-4fc4-bb8b-38ecdb5e8651"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("b9149bd4-98a7-4570-91ff-6083a24d35e0"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("c74953dd-62db-48fd-a63a-c085df1e7132"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("e68bc8ab-c766-47a1-beea-5811f0ff784f"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("edfcde44-e65f-48cd-a02b-e7541d50f130"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01db6faa-b9b0-4f70-b6cb-789eb1f9b070"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("074753c5-4986-4f84-b97d-1f9293d24967"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("0f8e1e69-d742-4412-9435-297480e86d48"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("2439adac-be86-4ea6-a3fb-7baefad214ce"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("29ddaff6-8103-4f35-a667-7827368eabb4"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3907917d-d40d-4d44-9b7e-831d98f94fbd"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3917131c-ff21-4efc-a125-46c6516b735a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("39724848-5fff-4e09-a4ca-844d010b759d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("3ff1f0f0-e768-4d8a-9f23-9ba7b9fbbf8f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5566dd97-5087-4347-9590-69432a19b4b4"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5da2d35a-4278-416d-8f77-6388e26cf165"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6848849c-24cb-44b1-936b-33e6dfa479c9"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6f23eba6-9be2-4a04-bc22-9059205f79cd"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a04619ed-3859-49e2-9189-d8e9d2161f0b"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("a9df85df-f059-494d-87f0-23ef1d521ce7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("b7ba2807-b959-4fc4-bb8b-38ecdb5e8651"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("b9149bd4-98a7-4570-91ff-6083a24d35e0"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c74953dd-62db-48fd-a63a-c085df1e7132"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("e68bc8ab-c766-47a1-beea-5811f0ff784f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("edfcde44-e65f-48cd-a02b-e7541d50f130"));

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDateTime",
                table: "AspNetUsers");

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
    }
}
