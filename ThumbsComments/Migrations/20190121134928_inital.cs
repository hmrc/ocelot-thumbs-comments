using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThumbsComments.Migrations
{
    public partial class inital : Migration
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void Up(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LineOfBusiness = table.Column<string>(maxLength: 255, nullable: false),
                    ThumbComment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_LineOfBusiness",
                table: "Comments",
                column: "LineOfBusiness",
                unique: true);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void Down(MigrationBuilder migrationBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
