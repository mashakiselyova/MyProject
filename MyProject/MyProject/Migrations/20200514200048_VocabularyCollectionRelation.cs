using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class VocabularyCollectionRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Vocabularies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VocabularyId",
                table: "Collections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "8a2d5023-34c8-4326-8b37-b1a891d9d311");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "VocabularyId",
                table: "Collections");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "7b497d5c-d9df-4c11-9993-9f290a311b9c");
        }
    }
}
