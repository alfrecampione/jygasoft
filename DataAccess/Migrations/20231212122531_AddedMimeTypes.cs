using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedMimeTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Categories_CategoryId",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Modalities");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Sports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Faculties",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoMimeType",
                table: "Faculties",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SportId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Disciplines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Photo",
                table: "Athletes",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoMimeType",
                table: "Athletes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_DisciplineId",
                table: "Events",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SportId",
                table: "Events",
                column: "SportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Disciplines_DisciplineId",
                table: "Events",
                column: "DisciplineId",
                principalTable: "Disciplines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Sports_SportId",
                table: "Events",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Categories_CategoryId",
                table: "Sports",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Disciplines_DisciplineId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Sports_SportId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Sports_Categories_CategoryId",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Events_DisciplineId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_SportId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PhotoMimeType",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SportId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "PhotoMimeType",
                table: "Athletes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Sports",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Modalities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Faculties",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "Athletes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_Categories_CategoryId",
                table: "Sports",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
