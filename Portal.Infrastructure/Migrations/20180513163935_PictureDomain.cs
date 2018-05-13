using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Portal.Infrastructure.Migrations
{
    public partial class PictureDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayInMain",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PictureID",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AltAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    MimeType = table.Column<string>(nullable: true),
                    PictureBinary = table.Column<byte[]>(nullable: true),
                    SeoFilename = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropColumn(
                name: "DisplayInMain",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "Groups");
        }
    }
}
