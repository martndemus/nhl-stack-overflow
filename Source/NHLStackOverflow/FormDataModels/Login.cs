namespace NHLStackOverflow.FormDataModels
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class PassLost
    {
        public string Password1 { get; set; }
        public string Password2 { get; set; }
    }

    public class PassLostEmail
    {
        public string Email { get; set; }
    }
}