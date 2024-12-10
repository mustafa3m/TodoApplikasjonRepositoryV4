using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApplikasjonAPIEntityDelTre.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTodoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Todos",
                type: "TEXT",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2147483647);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Todos",
                type: "TEXT",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
