namespace ServicoEmail
{
    public partial class Usuarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Nullable<int> IdEspecialidade { get; set; }
        public int Tipo { get; set; }
        public int Status { get; set; }
        public Nullable<int> Responsavel { get; set; }

    }
}
