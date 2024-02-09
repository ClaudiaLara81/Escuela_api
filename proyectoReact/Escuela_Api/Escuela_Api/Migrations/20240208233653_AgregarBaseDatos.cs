using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBaseDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApellidoPaterno = table.Column<string>(name: "Apellido_Paterno", type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(name: "Apellido_Materno", type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(name: "Fecha_Nacimiento", type: "datetime2", nullable: false),
                    LugarNacimiento = table.Column<string>(name: "Lugar_Nacimiento", type: "nvarchar(max)", nullable: false),
                    EstadoCivil = table.Column<string>(name: "Estado_Civil", type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroMatricula = table.Column<string>(name: "Numero_Matricula", type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(name: "Fecha_Inicio", type: "datetime2", nullable: false),
                    Curp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students");
        }
    }
}
