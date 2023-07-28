#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskEase.Infrastructure.Migrations.ApplicationDbContext
{
    /// <inheritdoc />
    public partial class Added_UserId_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BoardUsers",
                type: "character varying(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "BoardUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldUnicode: false,
                oldMaxLength: 50);
        }
    }
}
