using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreWithAutoMapper.Migrations
{
    public partial class InitializeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                });

            migrationBuilder.InsertData("Student", new string[]
            {
                "ID", "Name", "Grade"
            }, new object[,]
            {
                { 1, "A", 5.0},
                { 2, "B", 6.0},
                { 3, "C", 7.0},
                { 4, "D", 8.0},
                { 5, "E", 9.0},
                { 6, "F", 10.0},
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
