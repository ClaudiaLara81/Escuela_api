using Escuela_Api.Models;

namespace Escuela_Api.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> UpdateStudent(Student entidad);
    }
}
