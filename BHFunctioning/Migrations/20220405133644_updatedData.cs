using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHFunctioning.Migrations
{
    public partial class updatedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Alert",
                table: "HealthData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Constant",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Down",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Mean",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StandardDeviation",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Up",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alert",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Constant",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Down",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Mean",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "StandardDeviation",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Up",
                table: "HealthData");
        }
    }
}
