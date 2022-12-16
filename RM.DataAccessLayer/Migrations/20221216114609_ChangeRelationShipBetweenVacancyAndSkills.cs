using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RM.DataAccessLayer.Migrations
{
    public partial class ChangeRelationShipBetweenVacancyAndSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Vacancies_VacancyID",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_VacancyID",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "VacancyID",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "SkillsVacancy",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "int", nullable: false),
                    VacanciesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsVacancy", x => new { x.SkillsId, x.VacanciesID });
                    table.ForeignKey(
                        name: "FK_SkillsVacancy_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsVacancy_Vacancies_VacanciesID",
                        column: x => x.VacanciesID,
                        principalTable: "Vacancies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillsVacancy_VacanciesID",
                table: "SkillsVacancy",
                column: "VacanciesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillsVacancy");

            migrationBuilder.AddColumn<int>(
                name: "VacancyID",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_VacancyID",
                table: "Skills",
                column: "VacancyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Vacancies_VacancyID",
                table: "Skills",
                column: "VacancyID",
                principalTable: "Vacancies",
                principalColumn: "ID");
        }
    }
}
