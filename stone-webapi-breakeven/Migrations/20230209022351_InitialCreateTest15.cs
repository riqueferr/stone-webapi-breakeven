using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stonewebapibreakeven.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTest15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            {
                migrationBuilder.AddColumn<double>(
                    name: "Balance",
                    table: "Wallets",
                    type: "double",
                    nullable: true,
                    defaultValue: 0.0);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
