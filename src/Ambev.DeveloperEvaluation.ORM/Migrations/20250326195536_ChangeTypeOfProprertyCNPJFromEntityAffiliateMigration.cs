using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeOfProprertyCNPJFromEntityAffiliateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Afiliates",
                type: "character(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldFixedLength: true,
                oldMaxLength: 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cnpj",
                table: "Afiliates",
                type: "integer",
                fixedLength: true,
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(14)",
                oldFixedLength: true,
                oldMaxLength: 14);
        }
    }
}
