namespace BDAProject.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddUserNameToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Posts",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserName",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Posts");
        }
    }
}
