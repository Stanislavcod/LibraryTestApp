
using Library.Model.Models;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IBookService
    {
        void Create(Book book, Stream fileStream);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetByUser(int id);
        Book GetByName(string name);
        Book GetById(int id);
        void Edit(Book book, Stream fileStream);
        void Delete(int id);
    }
}
