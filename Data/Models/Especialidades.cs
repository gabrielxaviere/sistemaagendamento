namespace Data.Models
{
    public partial class Especialidades
    {
        public Especialidades()
        {
            Usuarios = new HashSet<Usuarios>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Usuarios> Usuarios { get; set; }

    }
}
