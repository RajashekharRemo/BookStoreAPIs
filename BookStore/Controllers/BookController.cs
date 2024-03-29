using BusinessLayer.Interfaces;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookBusiness? _bookBusiness;
        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet("GetAllBooks")]
        public IActionResult GetAll()
        {
            var result = _bookBusiness.GetAllBooks();
            if(result == null) { return NotFound(new {message="No data Found", result=false}); }
            //int count = result.Count();
            return Ok(result);

        }

        [HttpGet("GetBookById")]
        public IActionResult GetById(int id)
        {
            if (id < 0)
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            var book=_bookBusiness.GetBookById(id);
            if(book == null) { return NotFound((new { message = "No data Found", result = false })); }
            return Ok(new { message = "Data Found", result = true, data = book });
        }

        [HttpGet("GetBookByTitle")]
        public IActionResult GetByTitle(string Title)
        {
            if (Title.IsNullOrEmpty())
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            var book = _bookBusiness.GetBookByTitle(Title);
            if (book == null) { return NotFound((new { message = "No data Found", result = false })); }
            return Ok(new { message = "Data Found", result = true, data = book });
        }


        [HttpGet("GetAllBookByAuther")]
        public IActionResult GetByAuther(string Auther)
        {
            if (Auther.IsNullOrEmpty())
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            var books = _bookBusiness.GetBooksByAutherName(Auther);
            if (books == null) { return NotFound((new { message = "No data Found", result = false })); }
            return Ok(new { message = "Data Found", result = true, data = books });
        }


        [HttpGet("GetBookByTitleandAuther")]
        public IActionResult GetByTitleandAuther(string Title, string Auther)
        {
            if (Title.IsNullOrEmpty() && Auther.IsNullOrEmpty())
            {
                return BadRequest(new { message = "Bad request", result = false });
            }

            var book = _bookBusiness.GetBookByTitleandAuther(Title, Auther);
            if (book == null) { return NotFound((new { message = "No data Found", result = false })); }
            return Ok(new { message = "Data Found", result = true, data = book });
        }


        [HttpPost("CreateBook")]
        public IActionResult CreateBook(InsertBook insert)
        {
            if(insert == null)
            {
                return BadRequest(new {});
            }

            bool flag = _bookBusiness.CreateBook(insert);
            if (flag)
            {
                return Ok(new { message = "Created Successfully", result = true });
            }
            else
            {
                return BadRequest(new { message="Not Created", result=false});
            }
        }

        [HttpPut("UpdateBook")]
        public IActionResult UpdateBook(Book book)
        {
            if (book == null)
            {
                return BadRequest(new { message = "Provide details All", result = false });
            }

            bool flag = _bookBusiness.UpdateBook(book);

            if (flag)
            {
                return Ok(new { message = "Book Updated Successful", result = true });
            }
            else
            {
                return NotFound(new { message ="Book Not Found", result=false});
            }
        }


    }
}
