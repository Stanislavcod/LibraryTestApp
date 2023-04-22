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

        [HttpPost, Authorize(Roles ="Admin")]
        public ActionResult CreateBook([FromForm] Book book)
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var fileStream = file.OpenReadStream())
                {
                    _bookService.Create(book, fileStream);
                }

                return RedirectToAction("Index", "Books");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut, Authorize(Roles ="Admin")]
        public ActionResult EditBook([FromForm] Book book)
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var fileStream = file.OpenReadStream())
                {
                    _bookService.Edit(book, fileStream);
                }
                return HttpContext.Request.Headers["Referer"].ToString().Contains("Index") ?
        RedirectToAction("Index", "Books") :
        Redirect(HttpContext.Request.Headers["Referer"].ToString());
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

        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult Delete([FromForm]int id)
        {
            try
            {
                _bookService.Delete(id);

                return HttpContext.Request.Headers["Referer"].ToString().Contains("Index") ?
        RedirectToAction("Index", "Books") :
        Redirect(HttpContext.Request.Headers["Referer"].ToString());
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
