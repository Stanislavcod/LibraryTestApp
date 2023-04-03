using Library.BusinessLogic.Services.Contracts;
using Library.Model.DatabaseContext;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.BusinessLogic.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ILogger<Book> _logger;
        private readonly ApplicationDatabaseContext _context;

        public BookService(ApplicationDatabaseContext context, ILogger<Book> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void Create(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }

        public IEnumerable<Book> Get()
        {
            try
            {
                var customers = _context.Books.AsNoTracking().ToList();

                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
                return new List<Book>();
            }
        }

        public Book Get(string name)
        {
            try
            {
                var book = _context.Books.AsNoTracking().FirstOrDefault(book => book.Name == name);

                if (book == null)   
                {
                    throw new Exception("Книга не найдена");
                }

                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
                return new Book();
            }
        }

        public Book Get(int id)
        {
            try
            {
                var book = _context.Books.AsNoTracking().FirstOrDefault(book => book.Id == id);

                if (book == null)
                {
                    throw new Exception("Книга не найдена");
                }

                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                return new Book();
            }
        }

        public void Edit(Book book)
        {
            try
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(book => book.Id == id);

                if (book == null)
                {
                    throw new Exception("Книга не найдена");
                }

                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
    }
}
