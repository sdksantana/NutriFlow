using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix_RemoveUserIFromTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NutritionistId1",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId1",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NutritionistId1",
                table: "AspNetUsers",
                column: "NutritionistId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PatientId1",
                table: "AspNetUsers",
                column: "PatientId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nutritionists_NutritionistId1",
                table: "AspNetUsers",
                column: "NutritionistId1",
                principalTable: "Nutritionists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId1",
                table: "AspNetUsers",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
