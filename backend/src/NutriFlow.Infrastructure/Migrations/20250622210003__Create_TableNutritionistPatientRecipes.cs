using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _Create_TableNutritionistPatientRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionistPatientRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NutritionistRecipeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionistPatientRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionistPatientRecipes_NutritionistRecipes_Nutritionist~",
                        column: x => x.NutritionistRecipeId,
                        principalTable: "NutritionistRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NutritionistPatientRecipes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistPatientRecipes_NutritionistRecipeId",
                table: "NutritionistPatientRecipes",
                column: "NutritionistRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistPatientRecipes_PatientId",
                table: "NutritionistPatientRecipes",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionistPatientRecipes");
        }
    }
}
