using Bookshelf.DTOs;
using Bookshelf.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Controllers
{
    [Route("api/genre")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenreController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            var genres = await context.Genres
                .OrderBy(genre => genre.Name)
                .Select(genre => new GenreDTO
                {
                    Id = genre.Id,
                    Name = genre.Name
                })
                .ToListAsync();

            return genres;
        }
    }
}
