using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Standard.Infrastructure.Migrations
{
    public partial class initi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    DisplayInMain = table.Column<bool>(nullable: false),
                    PictureID = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PictureBinary = table.Column<byte[]>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    SeoFilename = table.Column<string>(nullable: true),
                    AltAttribute = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Lead = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    IsAllowedComment = table.Column<bool>(nullable: false),
                    Hidden = table.Column<bool>(nullable: false),
                    Hit = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentPictures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PictureId = table.Column<long>(nullable: false),
                    ContentId = table.Column<long>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
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

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Description", "DisplayInMain", "IsPrivate", "ParentId", "PictureID", "Priority", "Title" },
                values: new object[] { 1L, "", true, false, null, false, 0, "ورزشی" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Description", "DisplayInMain", "IsPrivate", "ParentId", "PictureID", "Priority", "Title" },
                values: new object[] { 2L, "", true, false, null, false, 0, "فن آوری اطلاعات" });

            migrationBuilder.CreateIndex(
                name: "IX_ContentPictures_ContentId",
                table: "ContentPictures",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPictures_PictureId",
                table: "ContentPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_GroupId",
                table: "Contents",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentPictures");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
