namespace AFTER.Shared.Common
{
    public class EmailConfiguration
    {
        //sendgrid
        public string Username { get; set; }

        public string ApiKey { get; set; }

        public string From { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        //gmail
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
