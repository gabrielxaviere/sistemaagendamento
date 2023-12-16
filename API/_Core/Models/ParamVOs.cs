namespace App._Core.Models
{
    public class CreateReturn
    {
        public CreateReturn(int id)
        {
            Id = id;
        }

        public CreateReturn(string codigo)
        {
            Codigo = codigo;
        }

        public CreateReturn(int id, string codigo)
        {
            Id = id;
            Codigo = codigo;
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
    }

    public class UpdatePass
    {
        public int IdUsuario { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
    }
}
