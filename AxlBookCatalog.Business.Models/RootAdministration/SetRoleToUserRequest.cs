namespace AxlBookCatalog.Business.Models.Authorization
{
    public class SetRoleToUserRequest
    {
        public string Email { get; set; }

        public string RootAdministratorPassword { get; set; }

        public string RoleName { get; set; }
    }
}
