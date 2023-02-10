using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace stonewebapibreakeven.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateTest17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsBanking_Wallets_WalletId",
                table: "AccountsBanking");

            migrationBuilder.DropIndex(
                name: "IX_AccountsBanking_WalletId",
                table: "AccountsBanking");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AccountsBanking");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WalletId",
                table: "AccountsBanking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountsBanking_WalletId",
                table: "AccountsBanking",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsBanking_Wallets_WalletId",
                table: "AccountsBanking",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
