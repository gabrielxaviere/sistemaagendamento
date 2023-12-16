namespace Data.Models
{
    public partial class ControleAcessos
    {
        public ControleAcessos()
        {
            ControleAcessosPermissao = new HashSet<ControleAcessosPermissao>();
        }

        public int Id { get; set; }
        public int Id_ControleAcessos { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Icone { get; set; }
        public int Ordem { get; set; }

        public virtual ICollection<ControleAcessosPermissao> ControleAcessosPermissao { get; set; }
    }
}
