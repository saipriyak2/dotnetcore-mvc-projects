using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DL_CompetencyPMS.Migrations
{
    /// <inheritdoc />
    public partial class mgr7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "CaseStudySolutionDetails",
                newName: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CaseStudySolutionDetails",
                newName: "CreatedDate");
        }
    }
}
