using Data.Models;

namespace Data.Repositories
{
    public class ControleAcessosRepository : RepositoryBase<ControleAcessos>
    {
        public ControleAcessosRepository(int idUser, int idInstancia)
        {
        }

        public bool GetPermissaoAcesso(string codigoControleAcesso)
        {
            //var ctr = (from controleAcessos in Db.Set<ControleAcessos>().Where(x => x.Codigo == codigoControleAcesso)
            //           join ctrAcessosPermissao in Db.Set<ControleAcessosPermissao>().Where(x => x.Id_Usuarios == this.userId)
            //             on controleAcessos.Id equals ctrAcessosPermissao.Id_ControleAcessos
            //           select new
            //           {
            //               permissao = ctrAcessosPermissao.Acesso
            //           }).FirstOrDefault();

            //if (ctr != null)
            //{
            //    return ctr.permissao;
            //}

            //return false;
            throw new NotImplementedException();
        }
    }
}