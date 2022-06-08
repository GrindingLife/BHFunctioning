using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHFunctioning.Migrations
{
    public partial class thresholddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Threshold_50",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Threshold_60",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Threshold_70",
                table: "HealthData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Threshold_50",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Threshold_60",
                table: "HealthData");

            migrationBuilder.DropColumn(
                name: "Threshold_70",
                table: "HealthData");
        }
    }
}
