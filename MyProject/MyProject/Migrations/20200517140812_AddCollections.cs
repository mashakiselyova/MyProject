using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class AddCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Vocabularies");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "bb1a31f7-6b30-4199-b640-f45fd8a665d0");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_VocabularyId",
                table: "Collections",
                column: "VocabularyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Vocabularies_VocabularyId",
                table: "Collections",
                column: "VocabularyId",
                principalTable: "Vocabularies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Vocabularies_VocabularyId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_VocabularyId",
                table: "Collections");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Vocabularies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "8a2d5023-34c8-4326-8b37-b1a891d9d311");
        }
    }
}
