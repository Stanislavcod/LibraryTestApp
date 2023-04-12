using Library.BusinessLogic.Services.Contracts;
using Library.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTestApp.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public IActionResult Index() 
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var books = _bookService.GetAll();

                return Ok(books);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult GetById(int id)
        {
            try
            {
                var book = _bookService.GetById(id);

                return Ok(book);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            try
            {
                _bookService.Create(book);

                return Ok();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult EditBook(Book book)
        {
            try
            {
                _bookService.Edit(book);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete()]
        public ActionResult Delete(int id)
        {
            try
            {
                _bookService.Delete(id);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
