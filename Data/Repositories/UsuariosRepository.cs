using Data.Models;
using Data.Repository.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UsuariosRepository : RepositoryBase<Usuarios>
    {
        public UsuariosRepository()
        {
        }

        public IEnumerable<Usuarios> GetAll(Expression<Func<Usuarios, bool>> predicate)
        {
            return Db.Set<Usuarios>().Include("Especialidades").Where(predicate);
        }

        public Usuarios GetUserByEmail(string email)
        {
            return Db.Set<Usuarios>().Where(x => x.Email == email).FirstOrDefault();
        }

        public bool PermissionsValdiate(int idUser, string accessLevel)
        {
            throw new NotImplementedException();
        }

        public Usuarios GetUserToLogon(string email, string senha)
        {
            var usu = Db.Set<Usuarios>().Where(x => x.Email == email).FirstOrDefault();

            if (usu != null)
            {
                if (Criptography.AesDecrypt(usu.Senha) == senha)
                {
                    return usu;
                }
                else
                {
                    return null;
                }
            }

            return usu;
        }
    }
}