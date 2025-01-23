using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace record_service.Migrations
{
    /// <inheritdoc />
    public partial class domain_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Records");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "domain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "id");
        }
    }
}
