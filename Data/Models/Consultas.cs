namespace Data.Models
{
    public partial class Consultas
    {
        public Consultas()
        {

        }
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int IdProfissional { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }

        public virtual Usuarios? Usuarios { get; set; }

    }
}
