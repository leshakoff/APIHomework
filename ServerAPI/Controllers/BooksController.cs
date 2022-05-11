using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        BooksContext db;

        public BooksController(BooksContext context)
        {
            db = context;
            if (!db.Books.Any())
            {
                db.Books.AddRange(
                    new Book { Title = "Клара и Солнце", Author = "Кадзуо Исигуро", YearOfIssue = 2021 },
                    new Book { Title = "Бора-Бора", Author = "Альберто Васкес-Фигероа", YearOfIssue = 2011 },
                    new Book { Title = "Сказание о Меджекивисе", Author = "Анна Коростелева", YearOfIssue = 2013 },
                    new Book { Title = "Путешествие в Икстлан", Author = "Карлос Кастанеда", YearOfIssue = 2004 }
                    );
                db.SaveChanges();
            }
        }



        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await db.Books.ToListAsync();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut]
        public async Task<ActionResult<Book>> Put(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            if (!db.Books.Any(x => x.Id == book.Id))
            {
                return NotFound();
            }
            db.Update(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }


        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }
    }
}
