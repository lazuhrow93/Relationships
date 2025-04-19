using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingColsForConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_connection_character_end_id_character",
                table: "connection");

            migrationBuilder.DropForeignKey(
                name: "fk_connection_character_start_id_character",
                table: "connection");

            migrationBuilder.RenameColumn(
                name: "character_start_id",
                table: "connection",
                newName: "character_two_id");

            migrationBuilder.RenameColumn(
                name: "character_end_id",
                table: "connection",
                newName: "character_one_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_start_id",
                table: "connection",
                newName: "ix_connection_character_two_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_end_id",
                table: "connection",
                newName: "ix_connection_character_one_id");

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_one_id_character",
                table: "connection",
                column: "character_one_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_two_id_character",
                table: "connection",
                column: "character_two_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_connection_character_one_id_character",
                table: "connection");

            migrationBuilder.DropForeignKey(
                name: "fk_connection_character_two_id_character",
                table: "connection");

            migrationBuilder.RenameColumn(
                name: "character_two_id",
                table: "connection",
                newName: "character_start_id");

            migrationBuilder.RenameColumn(
                name: "character_one_id",
                table: "connection",
                newName: "character_end_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_two_id",
                table: "connection",
                newName: "ix_connection_character_start_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_one_id",
                table: "connection",
                newName: "ix_connection_character_end_id");

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_end_id_character",
                table: "connection",
                column: "character_end_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_start_id_character",
                table: "connection",
                column: "character_start_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
