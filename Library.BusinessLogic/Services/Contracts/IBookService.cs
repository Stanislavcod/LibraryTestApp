
using Library.Model.Models;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IBookService
    {
        void Create(Book book, Stream fileStream);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetUserBook(string userName);
        void AddUserBook(int bookId, int userId);
        Book GetByName(string name);
        Book GetById(int id);
        void Edit(Book book);
        void Delete(int id);
        public IEnumerable<Book> Search(string author, string title, DateTime? date, bool multi);
        public void DeleteUserBook(int bookId);
    }
}
