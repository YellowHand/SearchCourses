using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoursesApi.Models;

namespace SearchCourses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchCoursesController : ControllerBase
    {
        private readonly CoursesContext _context;

        public SearchCoursesController(CoursesContext context)
        {
            _context = context;
        }

        // GET: api/SearchCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/SearchCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(long id)
        {
            var courses = await _context.Courses.FindAsync(id);

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/SearchCourses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourses(long id, Courses courses)
        {
            if (id != courses.Id)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SearchCourses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Courses>> PostCourses(Courses courses)
        {
            _context.Courses.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.Id }, courses);
        }

        // DELETE: api/SearchCourses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Courses>> DeleteCourses(long id)
        {
            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();

            return courses;
        }

        private bool CoursesExists(long id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
