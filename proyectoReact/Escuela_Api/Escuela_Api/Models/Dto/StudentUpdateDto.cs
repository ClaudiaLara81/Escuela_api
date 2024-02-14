using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escuela_Api.Models.Dto
{
    public class StudentUpdateDto
    {
        [Required]
        public int Id { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombres { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Lugar_Nacimiento { get; set; }
        public string Estado_Civil { get; set; }
        public string Genero { get; set; }
        public string Nacionalidad { get; set; }
        [Required]
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Especialidad { get; set; }
        [Required]
        public string Numero_Matricula { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        [Required] 
        public string Curp { get; set; }
        public int Estado { get; set; }
    }
}
