using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyProject.Migrations
{
    public partial class ChangeReviewWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReview",
                table: "RevisionWords");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextReview",
                table: "RevisionWords",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "f601064f-8bf0-4370-bf1c-334941440646");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextReview",
                table: "RevisionWords");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReview",
                table: "RevisionWords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0",
                column: "ConcurrencyStamp",
                value: "e96849a7-3d37-4dd4-86f8-3183f17b79de");
        }
    }
}
