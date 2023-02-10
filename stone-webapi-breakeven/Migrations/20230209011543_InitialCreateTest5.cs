using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stonewebapibreakeven.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTest5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "AccountsBanking",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
