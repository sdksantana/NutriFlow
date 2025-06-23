using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_TableBodyMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Neck = table.Column<decimal>(type: "numeric", nullable: true),
                    Chest = table.Column<decimal>(type: "numeric", nullable: true),
                    Waist = table.Column<decimal>(type: "numeric", nullable: true),
                    Abdomen = table.Column<decimal>(type: "numeric", nullable: true),
                    Hip = table.Column<decimal>(type: "numeric", nullable: true),
                    RightArm = table.Column<decimal>(type: "numeric", nullable: true),
                    LeftArm = table.Column<decimal>(type: "numeric", nullable: true),
                    RightForearm = table.Column<decimal>(type: "numeric", nullable: true),
                    LeftForearm = table.Column<decimal>(type: "numeric", nullable: true),
                    RightThigh = table.Column<decimal>(type: "numeric", nullable: true),
                    LeftThigh = table.Column<decimal>(type: "numeric", nullable: true),
                    RightCalf = table.Column<decimal>(type: "numeric", nullable: true),
                    LeftCalf = table.Column<decimal>(type: "numeric", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorUserName = table.Column<string>(type: "text", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifierUserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyMeasurements_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMeasurements_PatientId",
                table: "BodyMeasurements",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");
        }
    }
}
