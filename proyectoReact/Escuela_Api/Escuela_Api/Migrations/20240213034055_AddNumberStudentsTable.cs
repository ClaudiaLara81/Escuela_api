using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberStudentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "numberStudents",
                columns: table => new
                {
                    StudentNo = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DetailSpecial = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numberStudents", x => x.StudentNo);
                    table.ForeignKey(
                        name: "FK_numberStudents_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Apellido_Materno", "Apellido_Paterno", "Estado_Civil", "Fecha_Inicio", "Fecha_Nacimiento", "Lugar_Nacimiento", "Numero_Matricula" },
                values: new object[] { "Canche", "Lara", "Casada", new DateTime(2023, 9, 3, 2, 30, 20, 360, DateTimeKind.Local), new DateTime(1981, 10, 9, 2, 0, 50, 0, DateTimeKind.Local), "Chetumal", "123456" });

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Apellido_Materno", "Apellido_Paterno", "Estado_Civil", "Fecha_Inicio", "Fecha_Nacimiento", "Lugar_Nacimiento", "Numero_Matricula" },
                values: new object[] { "Lara", "Dzul", "Soltera", new DateTime(2023, 9, 3, 2, 30, 20, 360, DateTimeKind.Local), new DateTime(2011, 7, 11, 4, 15, 50, 0, DateTimeKind.Local), "Chetumal", "123457" });

            migrationBuilder.CreateIndex(
                name: "IX_numberStudents_StudentId",
                table: "numberStudents",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "numberStudents");

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Apellido_Materno", "Apellido_Paterno", "Estado_Civil", "Fecha_Inicio", "Fecha_Nacimiento", "Lugar_Nacimiento", "Numero_Matricula" },
                values: new object[] { null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Apellido_Materno", "Apellido_Paterno", "Estado_Civil", "Fecha_Inicio", "Fecha_Nacimiento", "Lugar_Nacimiento", "Numero_Matricula" },
                values: new object[] { null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });
        }
    }
}
