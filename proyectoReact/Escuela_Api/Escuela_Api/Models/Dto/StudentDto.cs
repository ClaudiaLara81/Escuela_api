using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escuela_Api.Models.Dto
{
    public class StudentDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombres { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Lugar_Nacimiento { get; set; }
        public string Estado_Civil { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Especialidad { get; set; }
        public string Numero_Matricula { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public string Curp { get; set; }
        public int Estado { get; set; }
    }
}
