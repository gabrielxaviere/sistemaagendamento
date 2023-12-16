namespace App._Core.Models
{
    public class Credential
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string[] roles { get; set; }
        public string pic { get; set; }
        public string fullname { get; set; }
        public int idClienteBase { get; set; }
        public int idInstancia { get; set; }
        public int qtdLicencas { get; set; }

    }
}
