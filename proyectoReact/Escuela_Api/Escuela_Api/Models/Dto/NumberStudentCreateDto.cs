using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Escuela_Api.Models.Dto
{
    public class NumberStudentCreateDto
    {
        [Required]
        public int StudentNo { get; set; }
        [Required]
        public int StudentId { get; set; }
        public string DetailSpecial { get; set; }
    }
}
