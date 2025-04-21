using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingColsConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "target_character_id");

            migrationBuilder.RenameColumn(
                name: "character_one_id",
                table: "connection",
                newName: "source_character_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_two_id",
                table: "connection",
                newName: "ix_connection_target_character_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_character_one_id",
                table: "connection",
                newName: "ix_connection_source_character_id");

            migrationBuilder.AddForeignKey(
                name: "fk_connection_source_character_id_character",
                table: "connection",
                column: "source_character_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_connection_target_character_id_character",
                table: "connection",
                column: "target_character_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_connection_source_character_id_character",
                table: "connection");

            migrationBuilder.DropForeignKey(
                name: "fk_connection_target_character_id_character",
                table: "connection");

            migrationBuilder.RenameColumn(
                name: "target_character_id",
                table: "connection",
                newName: "character_two_id");

            migrationBuilder.RenameColumn(
                name: "source_character_id",
                table: "connection",
                newName: "character_one_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_target_character_id",
                table: "connection",
                newName: "ix_connection_character_two_id");

            migrationBuilder.RenameIndex(
                name: "ix_connection_source_character_id",
                table: "connection",
                newName: "ix_connection_character_one_id");

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_one_id_character",
                table: "connection",
                column: "character_one_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_connection_character_two_id_character",
                table: "connection",
                column: "character_two_id",
                principalTable: "character",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
