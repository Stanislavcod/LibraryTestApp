
namespace Library.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; } 
        public byte[]? PasswordSalt { get; set; }
        public string Email { get; set; } =string.Empty;
        public List<Book> Books { get; set; } = new();
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
