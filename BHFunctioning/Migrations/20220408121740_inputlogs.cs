using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BHFunctioning.Migrations
{
    public partial class inputlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InputLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NEET = table.Column<bool>(type: "bit", nullable: false),
                    Selfharm = table.Column<bool>(type: "bit", nullable: false),
                    Psychosis = table.Column<bool>(type: "bit", nullable: false),
                    Medical = table.Column<bool>(type: "bit", nullable: false),
                    ChildDx = table.Column<bool>(type: "bit", nullable: false),
                    Circadian = table.Column<bool>(type: "bit", nullable: false),
                    Tripartite = table.Column<int>(type: "int", nullable: false),
                    ClinicalStage = table.Column<int>(type: "int", nullable: false),
                    Sofas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InputLogs");
        }
    }
}
