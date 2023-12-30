using IMobi.School.API.Controllers.Root;
using IMobi.School.BAL.v1.Book;
using IMobi.School.Config;
using IMobi.School.ServiceModal.v1.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMobi.School.API.Controllers.Dummy
{
    [Route("api/v1/[controller]")]
    public class AuthorController : IMobiRootController
    {
        #region Properties
        private readonly AuthorProcess _authorProcess;
        #endregion Properties

        #region Constructor
        public AuthorController(AuthorProcess authorProcess)
        {
            _authorProcess = authorProcess;
        }
        #endregion Constructor

        #region Get All
        [HttpGet("authors")]
        [Authorize(Roles = "SuperAdmin, SystemAdmin")]
        public async Task<ActionResult<List<AuthorSM>>> GetAll()
        {
            try
            {
                var authors = await _authorProcess.GetAuthors();
                if (authors != null || authors?.Count() > 0)
                    return Ok(authors);
                else
                    return NotFound("No authors found");
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Get All

        #region Get Single
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin, SystemAdmin")]
        public async Task<ActionResult<AuthorSM>> GetSingleById(int id)
        {
            try
            {
                var author = await _authorProcess.GetAuthorById(id);
                if (author != null)
                    return Ok(author);
                else
                    return NotFound($"No author found on id {id}");
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion Get Single

        #region Add
        [HttpPost]
        public async Task<ActionResult<AuthorSM>> Add([FromBody] AuthorSM authorSM)
        {
            try
            {
                var author = await _authorProcess.AddAuthor(authorSM);
                if (author != null)
                    return CreatedAtAction(nameof(GetSingleById), new { author.Id }, author);
                else
                    return BadRequest(author);
            }
            catch (IMobiException ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion Add
    }
}
