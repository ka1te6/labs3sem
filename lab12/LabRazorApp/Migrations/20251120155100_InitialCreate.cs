using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LabRazorApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Researchers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Position = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrincipalInvestigatorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiments_Researchers_PrincipalInvestigatorId",
                        column: x => x.PrincipalInvestigatorId,
                        principalTable: "Researchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SampleCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CollectedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ExperimentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Samples_Experiments_ExperimentId",
                        column: x => x.ExperimentId,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Researchers",
                columns: new[] { "Id", "Email", "FullName", "Position" },
                values: new object[,]
                {
                    { 1, "ivanov@lab.org", "Иванов И.И.", "PI" },
                    { 2, "petrov@lab.org", "Петров П.П.", "Postdoc" }
                });

            migrationBuilder.InsertData(
                table: "Experiments",
                columns: new[] { "Id", "Description", "EndDate", "PrincipalInvestigatorId", "StartDate", "Title" },
                values: new object[,]
                {
                    { 1, "Исследование каталитической активности", null, 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Катализация A" },
                    { 2, "Проверка стабильности при 37°C", null, 2, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Стабильность B" }
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "CollectedDate", "ExperimentId", "SampleCode", "Status", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "S-2025-0001", "Collected", "tissue" },
                    { 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "S-2025-0002", "Processed", "solution" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiments_PrincipalInvestigatorId",
                table: "Experiments",
                column: "PrincipalInvestigatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_ExperimentId",
                table: "Samples",
                column: "ExperimentId");

            migrationBuilder.CreateIndex(
                name: "IX_Samples_SampleCode",
                table: "Samples",
                column: "SampleCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropTable(
                name: "Experiments");

            migrationBuilder.DropTable(
                name: "Researchers");
        }
    }
}
