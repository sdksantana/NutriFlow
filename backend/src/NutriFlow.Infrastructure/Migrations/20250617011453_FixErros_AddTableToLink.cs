using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixErros_AddTableToLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutritionists_AspNetUsers_UserId",
                table: "Nutritionists");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Nutritionists_UserId",
                table: "Nutritionists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Nutritionists");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "BodyMeasurements");

            migrationBuilder.DropColumn(
                name: "LastModifierUserName",
                table: "BodyMeasurements");

            migrationBuilder.AddColumn<bool>(
                name: "IsMeasurementComplete",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateTable(
                name: "NutritionistPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NutritionistId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionistPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionistPatients_Nutritionists_NutritionistId",
                        column: x => x.NutritionistId,
                        principalTable: "Nutritionists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NutritionistPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NutritionistId",
                table: "AspNetUsers",
                column: "NutritionistId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NutritionistId1",
                table: "AspNetUsers",
                column: "NutritionistId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PatientId",
                table: "AspNetUsers",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PatientId1",
                table: "AspNetUsers",
                column: "PatientId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistPatients_NutritionistId",
                table: "NutritionistPatients",
                column: "NutritionistId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionistPatients_PatientId",
                table: "NutritionistPatients",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nutritionists_NutritionistId",
                table: "AspNetUsers",
                column: "NutritionistId",
                principalTable: "Nutritionists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nutritionists_NutritionistId1",
                table: "AspNetUsers",
                column: "NutritionistId1",
                principalTable: "Nutritionists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId",
                table: "AspNetUsers",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId1",
                table: "AspNetUsers",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nutritionists_NutritionistId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nutritionists_NutritionistId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Patients_PatientId1",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "NutritionistPatients");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NutritionistId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NutritionistId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PatientId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PatientId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsMeasurementComplete",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "NutritionistId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Patients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Nutritionists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "BodyMeasurements",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserName",
                table: "BodyMeasurements",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nutritionists_UserId",
                table: "Nutritionists",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutritionists_AspNetUsers_UserId",
                table: "Nutritionists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
