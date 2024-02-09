using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EscuelaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTableStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Apellido_Materno", "Apellido_Paterno", "Celular", "Correo", "Curp", "Direccion", "Especialidad", "Estado", "Estado_Civil", "Fecha_Inicio", "Fecha_Nacimiento", "Genero", "Lugar_Nacimiento", "Nacionalidad", "Nombres", "Numero_Matricula", "Telefono" },
                values: new object[,]
                {
                    { 1, "Canche", "Lara", "9831392917", "claudetta910@gmail.com", "LACC811009MQRRNL04", "Margarita Maza de Juarez #97", "Informatica", 1, "Casada", new DateTime(2023, 9, 3, 2, 30, 20, 360, DateTimeKind.Local), new DateTime(1981, 10, 9, 2, 0, 50, 0, DateTimeKind.Local), "Femenino", "Chetumal", "Mexicana", "Claudia Maria", "123456", "9838324158" },
                    { 2, "Lara", "Dzul", "9831392917", "JulietaDzul@gmail.com", "DULJ110711MQRZRLA7", "Margarita Maza de Juarez #97", "Diseñadora de modas", 1, "Soltera", new DateTime(2023, 9, 3, 2, 30, 20, 360, DateTimeKind.Local), new DateTime(2011, 7, 11, 4, 15, 50, 0, DateTimeKind.Local), "Femenino", "Chetumal", "Mexicana", "Julieta Montserrat", "123457", "9838324158" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
