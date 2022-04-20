using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore6Demo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false, defaultValueSql: "('Instructor')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InstructorID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_dbo.Department_dbo.Instructor_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Person",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OfficeAssignment",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.OfficeAssignment", x => x.InstructorID);
                    table.ForeignKey(
                        name: "FK_dbo.OfficeAssignment_dbo.Instructor_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Person",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    DepartmentID = table.Column<int>(type: "INTEGER", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_dbo.Course_dbo.Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseInstructor",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    InstructorID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.CourseInstructor", x => new { x.CourseID, x.InstructorID });
                    table.ForeignKey(
                        name: "FK_dbo.CourseInstructor_dbo.Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.CourseInstructor_dbo.Instructor_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_dbo.Enrollment_dbo.Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.Enrollment_dbo.Person_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentID", "Budget", "InstructorID", "Name", "StartDate" },
                values: new object[] { 1, 1000m, null, "教育訓練部", new DateTime(2022, 4, 20, 23, 38, 36, 372, DateTimeKind.Local).AddTicks(1250) });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentID", "Budget", "InstructorID", "Name", "StartDate" },
                values: new object[] { 2, 1000m, null, "人事行政部", new DateTime(2022, 4, 20, 23, 38, 36, 372, DateTimeKind.Local).AddTicks(1276) });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentID", "Budget", "InstructorID", "Name", "StartDate" },
                values: new object[] { 5, 1000m, null, "專案開發部", new DateTime(2022, 4, 20, 23, 38, 36, 372, DateTimeKind.Local).AddTicks(1319) });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentID", "Budget", "InstructorID", "Name", "StartDate" },
                values: new object[] { 13, 1000m, null, "產品開發部", new DateTime(2022, 4, 20, 23, 38, 36, 372, DateTimeKind.Local).AddTicks(1325) });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Credits", "DepartmentID", "Title" },
                values: new object[] { 1, 1, 5, "Entity Framework 6 開發實戰" });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Credits", "DepartmentID", "Title" },
                values: new object[] { 2, 1, 5, "Git新手入門" });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Credits", "DepartmentID", "Title" },
                values: new object[] { 3, 2, 5, "Git進階版控流程" });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Credits", "DepartmentID", "Title" },
                values: new object[] { 4, 5, 1, "ASP.NET MVC 5 開發實戰" });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentID",
                table: "Course",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseID",
                table: "CourseInstructor",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorID",
                table: "CourseInstructor",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorID1",
                table: "Department",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseID1",
                table: "Enrollment",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentID",
                table: "Enrollment",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorID2",
                table: "OfficeAssignment",
                column: "InstructorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInstructor");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "OfficeAssignment");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
