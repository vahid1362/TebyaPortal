using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portal.Infrastructure.Migrations
{
    public partial class ContentPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ContentPictures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<long>(nullable: false),
                    PictureId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentPictures_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentPictures_ContentId",
                table: "ContentPictures",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPictures_PictureId",
                table: "ContentPictures",
                column: "PictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentPictures");

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
    }
}
