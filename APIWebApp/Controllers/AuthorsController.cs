using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWebApp.Models;
using APIWebApp.Models.DTO;

namespace APIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly APIContext _context;

        public AuthorsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDtoRead>>> GetAuthors()
        {
            var authorsDtoRead = new List<AuthorDtoRead>();
            var authors = await _context.Authors.ToListAsync();
            foreach (var author in authors)
            {
                authorsDtoRead.Add(
                    new AuthorDtoRead()
                    {
                        id = author.id,
                        fullName = author.fullName,
                        country = author.country,
                        birthYear = author.birthYear
                    });
            }
            return authorsDtoRead;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDtoRead>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorDtoRead = new AuthorDtoRead
            {
                id = author.id,
                fullName = author.fullName,
                country = author.country,
                birthYear = author.birthYear
            };

            return authorDtoRead;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor([FromRoute]int? id, AuthorDtoWrite authorDtoWrite)
        {
            if (id is null)
            {
                return BadRequest("No id");
            }

            var author = await _context.Authors.FindAsync(id);
            if (author is null)
            {
                return BadRequest("Bad id");
            }

            author.fullName = authorDtoWrite.fullName;
            author.country = authorDtoWrite.country;
            author.birthYear = authorDtoWrite.birthYear;

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists((int)id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDtoWrite authorDtoWrite)
        {
            var author = new Author()
            {
                fullName = authorDtoWrite.fullName,
                country = authorDtoWrite.country,
                birthYear = authorDtoWrite.birthYear
            };

            if (author == null)
            {
                return BadRequest("Author object is null");
            }

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            var authorDtoRead = new AuthorDtoRead
            {
                id = author.id,
                fullName = author.fullName,
                country = author.country,
                birthYear = author.birthYear
            };


            return RedirectToAction("GetAuthor", new { id = author.id });
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.id == id);
        }
    }
}
