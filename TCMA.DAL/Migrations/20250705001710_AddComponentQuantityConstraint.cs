using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCMA.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddComponentQuantityConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Components",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Component_Quantity_Valid",
                table: "Components",
                sql: "([CanAssignQuantity] = 1 AND ([Quantity] IS NULL OR [Quantity] >= 0)) OR ([CanAssignQuantity] = 0 AND [Quantity] IS NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Component_Quantity_Valid",
                table: "Components");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
