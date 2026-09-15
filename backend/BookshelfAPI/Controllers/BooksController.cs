using Bookshelf.DTOs;
using Bookshelf.Entities;
using Bookshelf.Migrations;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookshelf.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BooksController : ControllerBase
    {
        private ApplicationDbContext context;
        private readonly IMapper mapper;

        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("{id:int}", Name = "GetBook")]
        public async Task<ActionResult<BookDetailsDTO>> Get(int id)
        {
            var book = await context.Books
                .Include(bookDb => bookDb.AuthorBooks)
                .ThenInclude(authorBooksDb => authorBooksDb.Author)
            .Include(bookDb => bookDb.BookGenres)
            .ThenInclude(bookGenresDb => bookGenresDb.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null) 
            {
                return NotFound();
            }

            book.AuthorBooks = book.AuthorBooks.OrderBy(authorBook => authorBook.Order).ToList();
            
            return mapper.Map<BookDetailsDTO>(book);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<BookDetailsDTO>>> Search(
            [FromQuery] string author, 
            [FromQuery] string title, 
            [FromQuery] DateTime? publishedDate, 
            [FromQuery] string genre)
        {
            var query = context.Books
                .Include(bookDb => bookDb.AuthorBooks)
                .ThenInclude(authorBooksDb => authorBooksDb.Author)
                .Include(bookDb => bookDb.BookGenres)
                .ThenInclude(bookGenresDb => bookGenresDb.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.AuthorBooks.Any(ab => ab.Author.Name.Contains(author)));
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }
            if (publishedDate.HasValue)
            {
                query = query.Where(b => b.PublishedDate.HasValue && b.PublishedDate.Value.Date == publishedDate.Value.Date);
            }
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(b => b.BookGenres.Any(bg => bg.Genre.Name.Contains(genre)));
            }

            var books = await query.ToListAsync();
            var result = mapper.Map<List<BookDetailsDTO>>(books);
            return result;
        }
        [HttpPost]
        public async Task<ActionResult> Post(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO.AuthorIds == null)
            {
                return BadRequest("Cannot be created a book wihout Author");
            }

            var authorIds = await context.Authors
                .Where(authorDb => bookCreateDTO.AuthorIds.Contains(authorDb.Id)).Select(x => x.Id).ToListAsync();

            if (bookCreateDTO.AuthorIds.Count != authorIds.Count)
            {
                return BadRequest("Does not exist one or more of sent Authors");
            }

            var book = mapper.Map<Book>(bookCreateDTO);

            if (book.AuthorBooks != null)
            {
                setOrderAuthors(book);
            }

            context.Add(book);
            await context.SaveChangesAsync();

            var bookDTO = mapper.Map<BookDTO>(book);

            return CreatedAtRoute("GetBook", new { id = book.Id }, bookDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, BookCreateDTO bookCreateDTO)
        {
            var bookDb = await context.Books
                .Include(x => x.AuthorBooks)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (bookDb == null)
            {
                return NotFound();
            }

            // making update through mapper
            bookDb = mapper.Map(bookCreateDTO, bookDb);
            setOrderAuthors(bookDb);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<BookPatchDTO> patchDocument)
        {
            if (patchDocument == null) 
            {
                return BadRequest();
            }
            var bookDb = await context.Books.FirstOrDefaultAsync(x=> x.Id == id);

            if (bookDb == null) 
            {
                return NotFound();
            }

            var bookDTO = mapper.Map<BookPatchDTO>(bookDb);

            patchDocument.ApplyTo(bookDTO, ModelState);

            var isValid = TryValidateModel(ModelState);

            if (!isValid) 
            {
                return BadRequest(ModelState);
            }

            mapper.Map(bookDTO, bookDb);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private void setOrderAuthors(Book book)
        {
            if (book.AuthorBooks != null)
            {
                for (int i = 0; i < book.AuthorBooks.Count; i++)
                {
                    book.AuthorBooks[i].Order = i;
                }
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await context.Books.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Author() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
