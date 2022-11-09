using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment host;

        public EmployeesController(ApplicationDbContext db,IWebHostEnvironment host)
        {
            this.db = db;
            this.host = host;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Employees.Include(a=>a.Department).ToListAsync());
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await db.Employees.FindAsync(id));
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();
            return  Ok("Added successfully");
        }

        // PUT api/<EmployeesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok("Updated successfully");
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            db.Employees.Remove(await db.Employees.FindAsync(id));
            await db.SaveChangesAsync();
            return Ok("Deleted successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var PostedFile = httpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var path = host.ContentRootPath + "images/" + FileName;

                var stream = new FileStream(path, FileMode.Create);
                PostedFile.CopyTo(stream);
                stream.Close();
                return new JsonResult(FileName);
            }
            catch
            {
                return new JsonResult("1.png");
            }
        }
    }
}
