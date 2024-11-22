using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOLID_Principles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReconfigureForeignKeyRelationshipBetweenExpensesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expense_UserId",
                table: "Expense");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserId",
                table: "Expense",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expense_UserId",
                table: "Expense");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UserId",
                table: "Expense",
                column: "UserId",
                unique: true);
        }
    }
}
