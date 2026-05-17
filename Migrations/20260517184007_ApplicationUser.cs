using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yourmomENT.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "identity",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Username",
                schema: "identity",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "identity",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "identity",
                table: "AspNetUsers",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UserName",
                schema: "identity",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "identity",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "identity",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
