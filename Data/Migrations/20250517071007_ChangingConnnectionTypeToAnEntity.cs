using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangingConnnectionTypeToAnEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "connection_type",
                table: "connection",
                newName: "relation_type_id");

            migrationBuilder.CreateTable(
                name: "relation_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_relation_type", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_connection_relation_type_id",
                table: "connection",
                column: "relation_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_connection_relation_type_id_relation_type",
                table: "connection",
                column: "relation_type_id",
                principalTable: "relation_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_connection_relation_type_id_relation_type",
                table: "connection");

            migrationBuilder.DropTable(
                name: "relation_type");

            migrationBuilder.DropIndex(
                name: "ix_connection_relation_type_id",
                table: "connection");

            migrationBuilder.RenameColumn(
                name: "relation_type_id",
                table: "connection",
                newName: "connection_type");
        }
    }
}
