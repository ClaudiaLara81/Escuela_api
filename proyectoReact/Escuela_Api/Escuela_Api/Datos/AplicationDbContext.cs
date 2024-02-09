using Escuela_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Escuela_Api.Datos
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> students {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    Id = 1,
                    Apellido_Paterno = "Lara",
                    Apellido_Materno = "Canche",
                    Nombres = "Claudia Maria",
                    Fecha_Nacimiento = Convert.ToDateTime("1981/10/09T07:00:50.0Z"),
                    Lugar_Nacimiento = "Chetumal",
                    Estado_Civil = "Casada",
                    Genero = "Femenino",
                    Nacionalidad = "Mexicana",
                    Celular = "9831392917",
                    Direccion = "Margarita Maza de Juarez #97",
                    Telefono = "9838324158",
                    Correo = "claudetta910@gmail.com",
                    Especialidad = "Informatica",
                    Numero_Matricula = "123456",
                    Fecha_Inicio = Convert.ToDateTime("2023/09/03T07:30:20.36Z"),
                    Curp = "LACC811009MQRRNL04",
                    Estado = 1

                },
                new Student()
                {
                    Id = 2,
                    Apellido_Paterno = "Dzul",
                    Apellido_Materno = "Lara",
                    Nombres = "Julieta Montserrat",
                    Fecha_Nacimiento = Convert.ToDateTime("2011/07/11T09:15:50.0Z"),
                    Lugar_Nacimiento = "Chetumal",
                    Estado_Civil = "Soltera",
                    Genero = "Femenino",
                    Nacionalidad = "Mexicana",
                    Celular = "9831392917",
                    Direccion = "Margarita Maza de Juarez #97",
                    Telefono = "9838324158",
                    Correo = "JulietaDzul@gmail.com",
                    Especialidad = "Diseñadora de modas",
                    Numero_Matricula = "123457",
                    Fecha_Inicio = Convert.ToDateTime("2023/09/03T07:30:20.36Z"),
                    Curp = "DULJ110711MQRZRLA7",
                    Estado = 1

                }
                );
        }
    }
}
