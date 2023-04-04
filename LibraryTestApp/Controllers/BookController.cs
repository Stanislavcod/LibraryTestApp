using Library.BusinessLogic.Services.Contracts;
using Library.Model.Models;
using Microsoft.AspNetCore.Mvc;


namespace LibraryTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Route("GetAll")]
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

        [Route("GetById")]
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

        [Route("CreateBook")]
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

        [Route("EditBook")]
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

        [Route("DeleteBook")]
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
