using IMobi.School.API.Controllers.Root;
using IMobi.School.BAL.v1.Book;
using IMobi.School.Config;
using IMobi.School.ServiceModal.v1.Book;
using Microsoft.AspNetCore.Mvc;

namespace IMobi.School.API.Controllers.Dummy
{
    [Route("api/v1/[controller]")]
    public class BookController : IMobiRootController
    {
        #region Properties
        private readonly BookProcess _bookProcess;
        #endregion Properties

        #region Constructor
        public BookController(BookProcess bookProcess)
        {
            _bookProcess = bookProcess;
        }
        #endregion Cosntructor

        #region Get All
        [HttpGet]
        public async Task<ActionResult<BookSM>> GetAll()
        {
            try
            {
                var books = await _bookProcess.GetBooks();
                if (books != null)
                    return Ok(books);
                else
                    return NotFound("No Books found");
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion Get All

        #region Get Single
        [HttpGet("{id}")]
        public async Task<ActionResult<BookSM>> GetSingleById(int id)
        {
            try
            {
                var book = await _bookProcess.GetBookById(id);
                if (book != null)
                    return Ok(book);
                else
                    return NotFound($"No book found on id {id}");
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion Get Single

        #region Add
        [HttpPost]
        public async Task<ActionResult<BookSM>> Add([FromBody] BookSM bookSM)
        {
            try
            {
                var book = await _bookProcess.AddBook(bookSM);
                if (book != null)
                    return CreatedAtAction(nameof(GetSingleById), new { book.Id }, book);
                else
                    return BadRequest(book);
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Add
    }
}
