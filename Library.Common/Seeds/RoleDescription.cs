using Library.Common.Contacts;

namespace Library.Common.Seeds
{
    public static class RoleDescription
    {
        public static string Get(RoleType role)
        {
            var description = role switch
            {
                RoleType.User => "Application user",
                RoleType.Admin => "User with additional rights"
            };
            return description;
        }
    }
}
