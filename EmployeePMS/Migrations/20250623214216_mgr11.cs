using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL_CompetencyPMS.Migrations
{
    /// <inheritdoc />
    public partial class mgr11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesigID",
                table: "UserDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CaseStudySolutionDetails",
                columns: table => new
                {
                    CSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseStudy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessmentID = table.Column<int>(type: "int", nullable: false),
                    soln1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comp1 = table.Column<int>(type: "int", nullable: false),
                    soln2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comp2 = table.Column<int>(type: "int", nullable: false),
                    soln3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comp3 = table.Column<int>(type: "int", nullable: false),
                    soln4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comp4 = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStudySolutionDetails", x => x.CSID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseStudySolutionDetails");

            migrationBuilder.DropColumn(
                name: "DesigID",
                table: "UserDetails");
        }
    }
}
