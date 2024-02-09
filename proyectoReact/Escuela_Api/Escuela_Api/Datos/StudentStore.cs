using Escuela_Api.Models.Dto;

namespace Escuela_Api.Datos
{
    public static class StudentStore
    {
        public static List<StudentDto> StudentList = new List<StudentDto>
        {
            new StudentDto { Id = 1, Nombres="Claudia", Correo="claudetta910@gmail.com", Celular="9831392917", Direccion= "Margarita maza de juarez #97 esquina 5 de febrero"},
            new StudentDto { Id = 2, Nombres="Julieta", Correo="julietadzul@gmail.com", Celular="9831392917", Direccion= "Margarita maza de juarez #97 esquina 5 de febrero"}
        };
    }
}
