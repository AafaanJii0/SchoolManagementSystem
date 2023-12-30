using IMobi.School.API.Controllers.Root;
using IMobi.School.DAL.Context;
using IMobi.School.DAL.Seeder;
using Microsoft.AspNetCore.Mvc;

namespace IMobi.School.API.Controllers.Seeder
{
    [Route("api/v1/[controller]")]
    public class SeederController : IMobiRootController
    {
        private readonly AppDbContext _appDbContext;
        public SeederController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        [Route("Init")]
        public async Task<ActionResult<bool>> SeedData()
        {
            Seeder<AppDbContext> seeder = new Seeder<AppDbContext>();
            var res = await seeder.SeedDummyData(_appDbContext);
            return Ok(res);
        }
    }
}
