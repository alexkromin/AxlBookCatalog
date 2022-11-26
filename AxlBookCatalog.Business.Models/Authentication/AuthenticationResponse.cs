using System.Text.Json.Serialization;

namespace AxlBookCatalog.Business.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime DateOfExpirationRefreshToken { get; set; }
    }
}