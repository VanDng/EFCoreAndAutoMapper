using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore6.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.ID);
                });

            migrationBuilder.InsertData("Class", new string[]
            {
                "ID", "Name"
            }, new object[,]
            {
                { 1, "Math" },
                { 2, "Science" },
                { 3, "Literature" },
                { 4, "Physic" },
            });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Class",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData("Student", new string[]
            {
                 "ID", "Name", "Grade", "ClassId"
            }, new object[,]
            {
                { 1, "A", 5.0, 1},
                { 2, "B", 6.0, 1},
                { 3, "C", 7.0, 2},
                { 4, "D", 8.0, 2},
                { 5, "E", 9.0, 2},
                { 6, "F", 10.0, 3},
            });

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Class");
        }
    }
}
