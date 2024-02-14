using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escuela_Api.Models
{
    public class NumberStudent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentNo { get; set; }
        [Required]
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public string DetailSpecial { get; set; }

    }
}
