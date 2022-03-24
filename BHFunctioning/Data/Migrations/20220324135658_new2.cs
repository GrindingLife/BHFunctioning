using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHFunctioning.Data.Migrations
{
    public partial class new2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Healthdata",
                table: "Healthdata");

            migrationBuilder.RenameTable(
                name: "Healthdata",
                newName: "HealthData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthData",
                table: "HealthData",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthData",
                table: "HealthData");

            migrationBuilder.RenameTable(
                name: "HealthData",
                newName: "Healthdata");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Healthdata",
                table: "Healthdata",
                column: "Id");
        }
    }
}
