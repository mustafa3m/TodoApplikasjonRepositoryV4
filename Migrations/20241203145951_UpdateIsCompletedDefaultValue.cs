using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApplikasjonAPIEntityDelTre.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsCompletedDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCompleted",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsCompleted",
                value: false);
        }
    }
}
