namespace App._Core
{
    public class AuthorizeUserAttribute : Attribute
    {
        public string AccessLevel { get; set; }
    }
}
