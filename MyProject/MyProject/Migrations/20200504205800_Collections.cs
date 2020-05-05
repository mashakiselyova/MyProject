using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class Collections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Vocabularies_VocabularyId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "VocabularyId",
                table: "Words",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Words",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Vocabularies",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "1326b54f-fb3b-4e70-9bf7-bc6146df13e4");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Vocabularies_VocabularyId",
                table: "Words",
                column: "VocabularyId",
                principalTable: "Vocabularies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Vocabularies_VocabularyId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Vocabularies");

            migrationBuilder.AlterColumn<int>(
                name: "VocabularyId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "e8178bf9-931c-46e8-834f-d5738e0d2c09");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Vocabularies_VocabularyId",
                table: "Words",
                column: "VocabularyId",
                principalTable: "Vocabularies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
