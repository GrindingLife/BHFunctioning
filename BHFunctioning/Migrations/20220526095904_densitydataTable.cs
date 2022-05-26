using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHFunctioning.Migrations
{
    public partial class densitydataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthDataFuture");

            migrationBuilder.CreateTable(
                name: "DensityDatas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensityDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensityDatas");

            migrationBuilder.CreateTable(
                name: "HealthDataFuture",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HealthDataFK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Sofas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthDataFuture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthDataFuture_HealthData_HealthDataFK",
                        column: x => x.HealthDataFK,
                        principalTable: "HealthData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthDataFuture_HealthDataFK",
                table: "HealthDataFuture",
                column: "HealthDataFK");
        }
    }
}
