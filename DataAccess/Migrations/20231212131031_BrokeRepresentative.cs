using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BrokeRepresentative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Representatives_Majors_MajorId",
                table: "Representatives");

            migrationBuilder.DropIndex(
                name: "IX_Representatives_MajorId",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Representatives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Representatives",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Representatives_MajorId",
                table: "Representatives",
                column: "MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Representatives_Majors_MajorId",
                table: "Representatives",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
