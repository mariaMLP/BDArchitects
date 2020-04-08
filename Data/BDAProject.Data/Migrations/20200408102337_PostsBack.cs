namespace BDAProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PostsBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserName",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserName",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserName",
                table: "Posts",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserName",
                table: "Posts",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
