using Escuela_Api.Datos;
using Escuela_Api.Models;
using Escuela_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Escuela_Api.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly AplicationDbContext _db;
        
        public StudentRepository(AplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<Student> UpdateStudent(Student entidad)
        {
            _db.students.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
