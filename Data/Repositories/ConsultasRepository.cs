using Data.List;
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

        public IEnumerable<ConsultaList> GetAllWithMedico(Expression<Func<Consultas, bool>> predicate)
        {
            var consultas = Db.Set<Consultas>()
    .Where(predicate)
    .AsNoTracking()
    .Join(
        Db.Set<Usuarios>().AsNoTracking(),
        c => c.IdProfissional,
        u => u.Id,
        (c, u) => new
        {
            Consulta = c,
            ProfissionalNome = u.Nome
        })
    .Join(
        Db.Set<Usuarios>().AsNoTracking(),
        cu => cu.Consulta.IdPaciente,
        p => p.Id,
        (cu, p) => new ConsultaList
        {
            Id = cu.Consulta.Id,
            IdPaciente = cu.Consulta.IdPaciente,
            IdProfissional = cu.Consulta.IdProfissional,
            Data = cu.Consulta.Data,
            Status = cu.Consulta.Status,
            ProfissionalNome = cu.ProfissionalNome,
            PacienteNome = p.Nome + " " + p.Sobrenome
        })
    .ToList();

            return consultas;
        }
    }
}