using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGymClassEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("01683efa-4a26-474a-a419-b2271991d590"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("075a50d5-b76b-4f65-8f7e-9d2458ad7228"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1e052f2a-7748-4ee8-9333-7e4dbdbac86d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("1fe777cf-a998-42aa-88fe-a4e4c56bf5d7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4550925c-e8f0-410d-acd8-754eefc1d0e2"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5031c763-2799-47b9-8887-9dbf3357ec85"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("51fbd366-56e7-495d-a74c-0ebc21008597"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("60607a97-5daa-401a-a6c2-e7bc373279ea"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("68e85323-7dc8-46cf-96d5-82655318ba5a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("711b98cd-e968-400f-bcf4-e1e900dab7aa"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("73cf3c77-c8c6-436d-bb27-5379ada37968"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("81a8ad7f-dea6-4f4b-b25b-22e5c1ba7fd5"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("830ce236-a38d-41ac-9df3-c740ed4d15e1"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("9b9a2add-08f5-4be0-a8aa-b4669cdcf72c"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("c631048b-a858-4b9c-98ff-5174cf238703"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d6bdd0d0-3e73-462c-ac92-699a8cfbe6db"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d8540068-3581-4bcf-8f18-208b379c2782"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f045425e-405a-4394-8b23-0af519f7627d"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f6cd5fd1-ac43-4814-8860-87d415f79895"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("fe3db96e-5e52-4beb-a597-11ec46039326"));

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "GymClasses");

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("106a1d5c-d2db-4cb3-9491-864609774aa2"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("44dcc47c-3541-4a36-bfd1-a9854f5e264b"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("4b3cb4d8-0dc9-4512-80fb-0aee8e280179"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("5d22d383-8d37-4611-bdf5-cb9f28795f9a"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("66936248-f7d1-4960-828e-e43eeb0e722f"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("6db3585f-0e8b-41fe-9a55-eecac4d30b37"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("7cbc14a6-0280-47fc-9940-906d1ace7203"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("8158b370-0fac-4413-9c2c-92208ab738f7"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("8b958f37-2c9b-453f-befb-9ce1d825c423"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("972da1b1-9ff2-4576-ad81-4c5077c8122e"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("979897fe-b919-4ce2-9ad8-e95c1a4f2d53"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("ab5184ad-0920-4c52-9cef-3e8a67ef8c07"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("be59dd89-97f0-47ef-b4ac-2211d415efaa"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("d0166ebe-caa0-470e-b514-e2bd9852d68f"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("d14cc8dd-2c10-4744-8dcc-0c1841619603"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("df1c1ae5-c460-4f44-8e3a-2a5d240180e6"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("f828619e-1b85-4987-b7df-5ebb25d4f8fa"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("f9fdb13a-b388-4411-a7db-eaa813c70e6e"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("fad9f6a0-ebfb-4c1e-971f-73ff6ebf956a"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("ff65f22c-a657-4e09-a47f-18f2d19e5d54"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("106a1d5c-d2db-4cb3-9491-864609774aa2"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("44dcc47c-3541-4a36-bfd1-a9854f5e264b"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("4b3cb4d8-0dc9-4512-80fb-0aee8e280179"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("5d22d383-8d37-4611-bdf5-cb9f28795f9a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("66936248-f7d1-4960-828e-e43eeb0e722f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("6db3585f-0e8b-41fe-9a55-eecac4d30b37"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("7cbc14a6-0280-47fc-9940-906d1ace7203"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8158b370-0fac-4413-9c2c-92208ab738f7"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("8b958f37-2c9b-453f-befb-9ce1d825c423"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("972da1b1-9ff2-4576-ad81-4c5077c8122e"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("979897fe-b919-4ce2-9ad8-e95c1a4f2d53"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ab5184ad-0920-4c52-9cef-3e8a67ef8c07"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("be59dd89-97f0-47ef-b4ac-2211d415efaa"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d0166ebe-caa0-470e-b514-e2bd9852d68f"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("d14cc8dd-2c10-4744-8dcc-0c1841619603"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("df1c1ae5-c460-4f44-8e3a-2a5d240180e6"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f828619e-1b85-4987-b7df-5ebb25d4f8fa"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("f9fdb13a-b388-4411-a7db-eaa813c70e6e"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("fad9f6a0-ebfb-4c1e-971f-73ff6ebf956a"));

            migrationBuilder.DeleteData(
                table: "MembershipFeatures",
                keyColumn: "Id",
                keyValue: new Guid("ff65f22c-a657-4e09-a47f-18f2d19e5d54"));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "GymClasses",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.InsertData(
                table: "MembershipFeatures",
                columns: new[] { "Id", "FeatureDescription", "MembershipId" },
                values: new object[,]
                {
                    { new Guid("01683efa-4a26-474a-a419-b2271991d590"), "Can invite a friend 3 times per month", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("075a50d5-b76b-4f65-8f7e-9d2458ad7228"), "Group classes included in the membership price", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("1e052f2a-7748-4ee8-9333-7e4dbdbac86d"), "Can invite a friend 6 times per month", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("1fe777cf-a998-42aa-88fe-a4e4c56bf5d7"), "Full access to all training zones", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("4550925c-e8f0-410d-acd8-754eefc1d0e2"), "Full access to all training zones", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("5031c763-2799-47b9-8887-9dbf3357ec85"), "Group classes included in the membership price", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("51fbd366-56e7-495d-a74c-0ebc21008597"), "Group classes included in the membership price", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("60607a97-5daa-401a-a6c2-e7bc373279ea"), "Can book group classes up to 7 days in advance", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("68e85323-7dc8-46cf-96d5-82655318ba5a"), "Can invite a friend 6 times per month", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("711b98cd-e968-400f-bcf4-e1e900dab7aa"), "Can book group classes up to 14 days in advance", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("73cf3c77-c8c6-436d-bb27-5379ada37968"), "Full access to all training zones", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("81a8ad7f-dea6-4f4b-b25b-22e5c1ba7fd5"), "Group classes included in the membership price", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("830ce236-a38d-41ac-9df3-c740ed4d15e1"), "Can book group classes up to 7 days in advance", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("9b9a2add-08f5-4be0-a8aa-b4669cdcf72c"), "Includes 1 free personal training session every 6 months", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("c631048b-a858-4b9c-98ff-5174cf238703"), "Full access to all training zones", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("d6bdd0d0-3e73-462c-ac92-699a8cfbe6db"), "Can book group classes up to 14 days in advance", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") },
                    { new Guid("d8540068-3581-4bcf-8f18-208b379c2782"), "Includes 2 free personal training sessions every 6 months", new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf") },
                    { new Guid("f045425e-405a-4394-8b23-0af519f7627d"), "Includes 1 free personal training session every 6 months", new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0") },
                    { new Guid("f6cd5fd1-ac43-4814-8860-87d415f79895"), "Can invite a friend 3 times per month", new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf") },
                    { new Guid("fe3db96e-5e52-4beb-a597-11ec46039326"), "Includes 2 free personal training sessions every 6 months", new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875") }
                });
        }
    }
}
