
using Library.Common.Contacts;

namespace Library.Model.Models
{
    public class Role
    {
        public int Id { get; set; }
        public RoleType Name { get; set; }
        public string NormalName { get; set; } = string.Empty;
    }
}
