using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingUniqueComboForConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_connection_source_character_id",
                table: "connection");

            migrationBuilder.CreateTable(
                name: "connection_note",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    connection_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_connection_note", x => x.id);
                    table.ForeignKey(
                        name: "fk_connection_note_connection_id_connection",
                        column: x => x.connection_id,
                        principalTable: "connection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_connection_source_character_id_target_character_id",
                table: "connection",
                columns: new[] { "source_character_id", "target_character_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_connection_note_connection_id",
                table: "connection_note",
                column: "connection_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "connection_note");

            migrationBuilder.DropIndex(
                name: "ix_connection_source_character_id_target_character_id",
                table: "connection");

            migrationBuilder.CreateIndex(
                name: "ix_connection_source_character_id",
                table: "connection",
                column: "source_character_id");
        }
    }
}
