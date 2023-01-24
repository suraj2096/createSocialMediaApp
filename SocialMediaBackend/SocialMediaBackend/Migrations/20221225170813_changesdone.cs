using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialMediaBackend.Migrations
{
    public partial class changesdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_applicationuserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDetail_AspNetUsers_applicationuserId",
                table: "PostDetail");

            migrationBuilder.DropIndex(
                name: "IX_PostDetail_applicationuserId",
                table: "PostDetail");

            migrationBuilder.DropIndex(
                name: "IX_Comment_applicationuserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "applicationuserId",
                table: "PostDetail");

            migrationBuilder.DropColumn(
                name: "applicationuserId",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "PostDetail",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostDetail_Userid",
                table: "PostDetail",
                column: "Userid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PostDetail_AspNetUsers_Userid",
                table: "PostDetail",
                column: "Userid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_Userid",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDetail_AspNetUsers_Userid",
                table: "PostDetail");

            migrationBuilder.DropIndex(
                name: "IX_PostDetail_Userid",
                table: "PostDetail");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Userid",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "PostDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationuserId",
                table: "PostDetail",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Userid",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applicationuserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostDetail_applicationuserId",
                table: "PostDetail",
                column: "applicationuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_applicationuserId",
                table: "Comment",
                column: "applicationuserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_applicationuserId",
                table: "Comment",
                column: "applicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostDetail_AspNetUsers_applicationuserId",
                table: "PostDetail",
                column: "applicationuserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
