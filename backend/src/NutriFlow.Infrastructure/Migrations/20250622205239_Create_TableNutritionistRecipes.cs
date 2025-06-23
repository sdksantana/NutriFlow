using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_TableNutritionistRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NutritionistRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PreparationTime = table.Column<string>(type: "text", nullable: false),
                    Calories = table.Column<string>(type: "text", nullable: false),
                    Protein = table.Column<string>(type: "text", nullable: false),
                    Fat = table.Column<string>(type: "text", nullable: false),
                    Carbs = table.Column<string>(type: "text", nullable: false),
                    Meal = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NutritionistId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionistRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionistRecipes_Nutritionists_NutritionistId",
                        column: x => x.NutritionistId,
                        principalTable: "Nutritionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistRecipes_NutritionistId",
                table: "NutritionistRecipes",
                column: "NutritionistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionistRecipes");
        }
    }
}
