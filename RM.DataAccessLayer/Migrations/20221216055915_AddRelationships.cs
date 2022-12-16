using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RM.DataAccessLayer.Migrations
{
    public partial class AddRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicantVacancy",
                columns: table => new
                {
                    ApplicantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VacanciesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantVacancy", x => new { x.ApplicantsId, x.VacanciesID });
                    table.ForeignKey(
                        name: "FK_ApplicantVacancy_Users_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ApplicantVacancy_Vacancies_VacanciesID",
                        column: x => x.VacanciesID,
                        principalTable: "Vacancies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeVacancy",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VacancyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVacancy", x => new { x.EmployeeId, x.VacancyId });
                    table.ForeignKey(
                        name: "FK_EmployeeVacancy_Users_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeeVacancy_Vacancies_VacancyId",
                        column: x => x.VacancyId,
                        principalTable: "Vacancies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantVacancy_VacanciesID",
                table: "ApplicantVacancy",
                column: "VacanciesID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacancy_VacancyId",
                table: "EmployeeVacancy",
                column: "VacancyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantVacancy");

            migrationBuilder.DropTable(
                name: "EmployeeVacancy");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}
