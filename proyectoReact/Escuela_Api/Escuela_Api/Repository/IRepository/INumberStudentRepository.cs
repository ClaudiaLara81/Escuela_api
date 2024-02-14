using Escuela_Api.Models;
using System.Linq.Expressions;

namespace Escuela_Api.Repository.IRepository
{
    public interface INumberStudentRepository : IRepository<NumberStudent>
    {
        Task<NumberStudent> UpdateStudent(NumberStudent entidad);

    }
}
