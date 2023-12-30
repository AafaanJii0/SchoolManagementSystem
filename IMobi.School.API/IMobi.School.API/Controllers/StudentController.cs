using IMobi.School.BAL;
using IMobi.School.DomainModal.v1;
using IMobi.School.ServiceModal.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMobi.School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentProcess _studentProcess;
        public StudentController(StudentProcess studentProcess)
        {
            _studentProcess = studentProcess;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_studentProcess.GetStudents());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_studentProcess.GetStudentById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentSM studentSM)
        {
            var res = await _studentProcess.AddStudent(studentSM);
            if(res != null)
            {
                return CreatedAtAction(nameof(GetById), new { Id = res.Id }, res);
            }
            return BadRequest("Student could not be added");
        }
    }
}
