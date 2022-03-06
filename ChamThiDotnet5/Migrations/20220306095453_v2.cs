using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChamThiDotnet5.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Exams_ExamId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ExamId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Exams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exam_Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exam_Student_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exam_Student_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_StudentId",
                table: "Exams",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_Student_ExamId",
                table: "Exam_Student",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_Student_StudentId",
                table: "Exam_Student",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Students_StudentId",
                table: "Exams");

            migrationBuilder.DropTable(
                name: "Exam_Student");

            migrationBuilder.DropIndex(
                name: "IX_Exams_StudentId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Exams");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Students",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ExamId",
                table: "Students",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Exams_ExamId",
                table: "Students",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
