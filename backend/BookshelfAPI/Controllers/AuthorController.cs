using AuthorsWebAPI.DTOs;
using AuthorsWebAPI.Entities;
using AuthorsWebAPI.Filters;
using AuthorsWebAPI.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorsWebAPI.Controllers
{
    [Route("api/author")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class AuthorController : ControllerBase
    {
        public ApplicationDbContext context;
        private readonly IMapper mapper;

        public AuthorController(ApplicationDbContext context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            var authors = await context.Authors.ToListAsync();
            return mapper.Map<List<AuthorDTO>>(authors);
        }
        [HttpGet("{name}")] // :string does not exist
        public async Task<ActionResult<List<AuthorDTO>>> Get(string name)
        {
            var authors = await context.Authors.Where(x => x.Name.Contains(name)).ToListAsync();
            return mapper.Map<List<AuthorDTO>>(authors);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuthorCreateDTO authorCreateDTO) 
        {
            var doesExistAuthorWithSameName = await context.Authors.AnyAsync(x => x.Name == authorCreateDTO.Name);
            if (doesExistAuthorWithSameName) 
            {
                return BadRequest($"Already exists an author with the name {authorCreateDTO.Name}");
            }
            var author = mapper.Map<Author>(authorCreateDTO);
            context.Add(author);
            await context.SaveChangesAsync();

            var authorDTO = mapper.Map<AuthorDTO>(author);

            return CreatedAtRoute("GetAuthor", new { id = author.Id }, authorDTO);
        }

        [HttpPut("{id:int}")] //api/author/{id:int}
        public async Task<ActionResult> Put(AuthorCreateDTO authorCreateDTO, int id) 
        {
            var exists = await context.Authors.AnyAsync(x => x.Id == id);
            if (!exists) 
            {
                return NotFound();
            }


            var author = mapper.Map<Author>(authorCreateDTO);
            author.Id = id;

            context.Update(author);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            var exists = await context.Authors.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound();
            }

            context.Remove(new Author() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id:int}", Name = "GetAuthor")]
        public async Task<ActionResult<AuthorDTOWithBooks>> Get(int id) 
        {
            var author = await context.Authors
                .Include(authorDb => authorDb.AuthorBooks)
                .ThenInclude(authorBookDb => authorBookDb.Book)
                .FirstOrDefaultAsync( authorDb => authorDb.Id == id );

            if (author == null) 
            {
                return NotFound();
            }

            return mapper.Map<AuthorDTOWithBooks>(author);
        }
    }
}
