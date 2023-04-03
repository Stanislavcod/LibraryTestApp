﻿
namespace Library.Model.Models
{
    class Book
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set;} = string.Empty;
        public DateTime Release { get; set; }
        public byte[] Photo { get; set; }
        public List<User> Users { get; set; } = new();
    }
}
