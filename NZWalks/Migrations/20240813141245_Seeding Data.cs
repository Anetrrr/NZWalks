using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("176f2d2e-b451-4765-a0ad-ee1136a8d6c9"), "Easy" },
                    { new Guid("334d59cc-fe13-4d37-87b4-20888e9f7d59"), "Medium" },
                    { new Guid("a615a068-f66c-42f5-ad73-579e59acb867"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1bdbcb9e-c9fc-4744-8af3-7af2c9052cd5"), "FLX", "Felix Highway", "felix.nz" },
                    { new Guid("4ad9a282-fbd4-48d5-993a-da7e9e0a500f"), "RST", "Rosantine Hills", "rosant.nz" },
                    { new Guid("d2e50bf4-b711-4740-9621-e15eb0de3096"), "ADM", "Adamanton", "adm.nz" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("176f2d2e-b451-4765-a0ad-ee1136a8d6c9"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("334d59cc-fe13-4d37-87b4-20888e9f7d59"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a615a068-f66c-42f5-ad73-579e59acb867"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1bdbcb9e-c9fc-4744-8af3-7af2c9052cd5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4ad9a282-fbd4-48d5-993a-da7e9e0a500f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d2e50bf4-b711-4740-9621-e15eb0de3096"));
        }
    }
}
