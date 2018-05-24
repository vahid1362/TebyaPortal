using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portal.Infrastructure.Migrations
{
    public partial class AddpictureToEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContentId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ContentId",
                table: "Pictures",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Contents_ContentId",
                table: "Pictures",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Contents_ContentId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ContentId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Pictures");
        }
    }
}
