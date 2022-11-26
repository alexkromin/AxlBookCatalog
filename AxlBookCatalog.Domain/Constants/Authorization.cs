namespace AxlBookCatalog.Domain.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            User
        }

        public const string DefaultUsername = "user";
        public const string DefaultEmail = "user@secureapi.com";
        public const string DefaultPassword = "Pa$$w0rd.";
        public const Roles DefaultRole = Roles.User;
    }
}
