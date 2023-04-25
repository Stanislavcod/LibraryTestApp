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
                _context.Book.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }

        public IEnumerable<Book> GetAll()
        {
            try
            {
                var books = _context.Book.AsNoTracking().ToList();

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                return new List<Book>();
            }
        }
        public IEnumerable<Book> GetUserBook(string userName)
        {
            try
            {
                var books = _context.Book.AsNoTracking().Where(x => x.User.Login == userName).ToList();

                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                return new List<Book>();
            }
        }
        public void AddUserBook(int bookId, int userId)
        {
            try
            {
                var book = _context.Book.Find(bookId);
                book.UserId = userId;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public void DeleteUserBook(int bookId)
        {
            try
            {
                var book = _context.Book.Find(bookId);
                book.UserId = null;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public Book GetByName(string name)
        {
            try
            {
                var book = _context.Book.AsNoTracking().FirstOrDefault(book => book.Name == name);

                if (book == null)
                    throw new Exception("Книга не найдена");

                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
                return new Book();
            }
        }

        public Book GetById(int id)
        {
            try
            {
                var book = _context.Book.AsNoTracking().FirstOrDefault(book => book.Id == id);

                if (book == null)
                    throw new Exception("Книга не найдена");

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
                var currentBook = _context.Book.Find(book.Id);

                if (currentBook == null)
                    throw new Exception("Книга не найдена");

                _context.Entry(currentBook).CurrentValues.SetValues(book);
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
                var book = _context.Book.Find(id);

                if (book == null)
                    throw new Exception("Книга не найдена");

                _context.Book.Remove(book);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");
            }
        }
        public IEnumerable<Book> Search(string author, string title, DateTime? date, bool multi)
        {
            try
            {
                var books = _context.Book.AsQueryable();

                if (!string.IsNullOrEmpty(author))
                {
                    books = books.Where(b => b.Author.Contains(author));
                }

                if (!string.IsNullOrEmpty(title))
                {
                    books = books.Where(b => b.Name.Contains(title));
                }

                if (date.HasValue)
                {
                    books = books.Where(b => b.Release == date.Value);
                }

                if (multi)
                {
                    books = books.Where(b =>
                        (!string.IsNullOrEmpty(author) && b.Author.Contains(author)) ||
                        (!string.IsNullOrEmpty(title) && b.Name.Contains(title)) ||
                        (date.HasValue && b.Release == date.Value)
                    );
                }
                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now}: {ex.Message}");

                return Enumerable.Empty<Book>();
            }
        }
    }
}
