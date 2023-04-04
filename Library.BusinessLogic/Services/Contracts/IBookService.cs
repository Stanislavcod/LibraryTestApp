
using Library.Model.Models;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IBookService
    {
        void Create(Book book);
        IEnumerable<Book> GetAll();
        Book GetByName(string name);
        Book GetById(int id);
        void Edit(Book book);
        void Delete(int id);
    }
}
