using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class ModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_WordEntryId",
                table: "Translations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vocabularies_AspNetUsers_UserId",
                table: "Vocabularies");

            migrationBuilder.DropIndex(
                name: "IX_Vocabularies_UserId",
                table: "Vocabularies");

            migrationBuilder.DropIndex(
                name: "IX_Translations_WordEntryId",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "TimeUntillRevision",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Word",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Vocabularies");

            migrationBuilder.DropColumn(
                name: "WordEntryId",
                table: "Translations");

            migrationBuilder.AddColumn<string>(
                name: "Original",
                table: "Words",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordId",
                table: "Translations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RevisionWords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(nullable: true),
                    TimeUntillRevision = table.Column<TimeSpan>(nullable: false),
                    CollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisionWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RevisionWords_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevisionWords_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "7b497d5c-d9df-4c11-9993-9f290a311b9c");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_WordId",
                table: "Translations",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RevisionWords_CollectionId",
                table: "RevisionWords",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RevisionWords_WordId",
                table: "RevisionWords",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Words_WordId",
                table: "Translations",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Translations_Words_WordId",
                table: "Translations");

            migrationBuilder.DropTable(
                name: "RevisionWords");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Translations_WordId",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "Original",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "Translations");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Words",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeUntillRevision",
                table: "Words",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vocabularies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Vocabularies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WordEntryId",
                table: "Translations",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "1326b54f-fb3b-4e70-9bf7-bc6146df13e4");

            migrationBuilder.CreateIndex(
                name: "IX_Vocabularies_UserId",
                table: "Vocabularies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_WordEntryId",
                table: "Translations",
                column: "WordEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Translations_Words_WordEntryId",
                table: "Translations",
                column: "WordEntryId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabularies_AspNetUsers_UserId",
                table: "Vocabularies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
