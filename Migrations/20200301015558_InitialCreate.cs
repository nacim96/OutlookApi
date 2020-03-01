using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiOutlook.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagesapiOutlook",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FromAddress = table.Column<string>(nullable: true),
                    FromName = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    msgDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesapiOutlook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attachmentss",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    MessageModelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachmentss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachmentss_MessagesapiOutlook_MessageModelId",
                        column: x => x.MessageModelId,
                        principalTable: "MessagesapiOutlook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachmentss_MessageModelId",
                table: "Attachmentss",
                column: "MessageModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachmentss");

            migrationBuilder.DropTable(
                name: "MessagesapiOutlook");
        }
    }
}
