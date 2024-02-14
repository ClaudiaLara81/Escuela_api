using Escuela_Api.Datos;
using Escuela_Api.Models;
using Escuela_Api.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Escuela_Api.Repository
{
    public class NumberStudentRepository : Repository<NumberStudent>, INumberStudentRepository
    {
        private readonly AplicationDbContext _db;
        
        public NumberStudentRepository(AplicationDbContext db) :base(db)
        {
            _db = db;
        }
        public async Task<NumberStudent> UpdateStudent(NumberStudent entidad)
        {
            _db.NumberStudents.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
