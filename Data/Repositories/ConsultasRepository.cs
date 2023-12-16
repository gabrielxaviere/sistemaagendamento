using Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ConsultasRepository : RepositoryBase<Consultas>
    {
        public ConsultasRepository()
        {
        }

        public IEnumerable<Consultas> GetAll(Expression<Func<Consultas, bool>> predicate)
        {
            return Db.Set<Consultas>().Include("Usuarios").Where(predicate);
        }
    }
}