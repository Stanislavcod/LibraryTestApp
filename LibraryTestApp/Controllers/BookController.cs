using Library.BusinessLogic.Services.Contracts;
using Library.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTestApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IUserService _userService;

        public BookController(IBookService bookService, IUserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(_bookService.GetAll());
        }
        [HttpGet, Authorize]
        public IActionResult UserBook()
        {
            try
            {
                var books = _bookService.GetUserBook(User.Identity.Name);

                return View(books);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult CreateBook([FromForm] Book book)
        {
            try
            {
                _bookService.Create(book);

                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public ActionResult EditBook([FromForm] Book book)
        {
            try
            {
                _bookService.Edit(book);
                return RedirectToAction("Index");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost, Authorize]
        public IActionResult AddUserBook(int bookId)
        {
            var user = _userService.Get(User.Identity.Name);

            _bookService.AddUserBook(bookId, user.Id);

            return RedirectToAction("UserBook");
        }
        [HttpPost, Authorize]
        public IActionResult DeleteUserBook(int bookId)
        {
            _bookService.DeleteUserBook(bookId);

            return RedirectToAction("UserBook");
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            _bookService.Delete(id);

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Search(string author, string title, DateTime? date, bool multi)
        {
            try
            {
                var filterBook = _bookService.Search(author, title, date, multi);

                return View(filterBook);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
