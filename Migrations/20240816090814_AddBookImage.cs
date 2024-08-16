using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class AddBookImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loans_fines_loan_fine_id",
                table: "loans");

            migrationBuilder.AlterColumn<long>(
                name: "loan_fine_id",
                table: "loans",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "book_image",
                table: "books",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_loans_fines_loan_fine_id",
                table: "loans",
                column: "loan_fine_id",
                principalTable: "fines",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loans_fines_loan_fine_id",
                table: "loans");

            migrationBuilder.DropColumn(
                name: "book_image",
                table: "books");

            migrationBuilder.AlterColumn<long>(
                name: "loan_fine_id",
                table: "loans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_loans_fines_loan_fine_id",
                table: "loans",
                column: "loan_fine_id",
                principalTable: "fines",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
