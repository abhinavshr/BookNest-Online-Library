using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookNest.Migrations
{
    /// <inheritdoc />
    public partial class AutherMigrationDatatypechange : Migration
    {
        /// <inheritdoc />
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.AlterColumn<long>(
        //        name: "Biography",
        //        table: "Authors",
        //        type: "bigint",
        //        nullable: true,
        //        oldClrType: typeof(string),
        //        oldType: "text",
        //        oldNullable: true);
        //}

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Authors\" ALTER COLUMN \"Biography\" TYPE bigint USING \"Biography\"::bigint;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Biography",
                table: "Authors",
                type: "text",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
