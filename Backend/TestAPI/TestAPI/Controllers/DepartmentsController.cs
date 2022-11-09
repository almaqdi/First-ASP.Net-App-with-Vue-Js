using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public DepartmentsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Departments.ToListAsync());
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await db.Departments.FindAsync(id));
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<JsonResult> Post(Department department)
        {
            await db.Departments.AddAsync(department);
            await db.SaveChangesAsync();
            return new JsonResult("Added successfully");
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut]
        public async Task<JsonResult> Put([FromBody] Department department)
        {
            db.Entry(department).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return new JsonResult("Updated successfully");
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(Guid id)
        {
            db.Departments.Remove(await db.Departments.FindAsync(id));
            await db.SaveChangesAsync();
            return new JsonResult("Deleted successfully");
        }
    }
}
