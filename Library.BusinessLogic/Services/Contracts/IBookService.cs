
using Library.Model.Models;

namespace Library.BusinessLogic.Services.Contracts
{
    public interface IBookService
    {
        void Create(Book book);
        IEnumerable<Book> Get();
        Book Get(string name);
        Book Get(int id);
        void Edit(Book book);
        void Delete(int id);
    }
}
