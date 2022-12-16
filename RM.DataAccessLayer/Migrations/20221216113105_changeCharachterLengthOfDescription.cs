using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RM.DataAccessLayer.Migrations
{
    public partial class changeCharachterLengthOfDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Responsibilities_ResponsibilitiesId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ResponsibilitiesId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ResponsibilitiesId",
                table: "Skills");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_JobCategoryId",
                table: "Vacancies",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_JobCategories_JobCategoryId",
                table: "Vacancies",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_JobCategories_JobCategoryId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_JobCategoryId",
                table: "Vacancies");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibilitiesId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ResponsibilitiesId",
                table: "Skills",
                column: "ResponsibilitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Responsibilities_ResponsibilitiesId",
                table: "Skills",
                column: "ResponsibilitiesId",
                principalTable: "Responsibilities",
                principalColumn: "Id");
        }
    }
}
