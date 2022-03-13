using Microsoft.EntityFrameworkCore.Migrations;

namespace ChamThiDotnet5.Migrations
{
    public partial class v2_add_submittedFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmittedFolder",
                table: "Exam_Student",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedFolder",
                table: "Exam_Student");
        }
    }
}
