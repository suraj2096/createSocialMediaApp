using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaBackend.Migrations
{
    public partial class postidaddincomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_Userid",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Userid",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "postid",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_postid",
                table: "Comment",
                column: "postid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_PostDetail_postid",
                table: "Comment",
                column: "postid",
                principalTable: "PostDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_PostDetail_postid",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_postid",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "postid",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Userid",
                table: "Comment",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_Userid",
                table: "Comment",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
