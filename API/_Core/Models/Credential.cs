namespace App._Core.Models
{
    public class Credential
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string accessToken { get; set; }
        public string[] roles { get; set; }
        public string fullname { get; set; }
        public int tipo { get; set; }
        public int? responsavel { get; set; }

    }
}
