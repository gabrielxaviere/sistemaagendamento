namespace Data.Models
{
    public partial class ControleAcessosPermissao
    {
        public int Id { get; set; }
        public int Id_Usuarios { get; set; }
        public int Id_ControleAcessos { get; set; }

        public virtual ControleAcessos? Id_ControleAcessosNavigation { get; set; }
        public virtual Usuarios? Id_UsuariosNavigation { get; set; }
    }
}
