using AuthorsWebAPI.DTOs;
using AuthorsWebAPI.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthorsWebAPI.Controllers
{
    [ApiController]
    [Route("api/books/{bookId:int}/comments")]
    public class CommentsController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public CommentsController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> Get(int bookId) 
        {
            var bookDoesExists = await context.Books.AnyAsync(bookBd => bookBd.Id == bookId);

            if (!bookDoesExists)
            {
                return NotFound();
            }

            var comments = await context.Comments.Where(commentsBd => commentsBd.BookId == bookId).ToListAsync();
            return mapper.Map<List<CommentDTO>>(comments);
        }
        [HttpPost]
        [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int bookId,CommentCreateDTO commentCreateDTO) 
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var bookDoesExists = await context.Books.AnyAsync(bookBd => bookBd.Id == bookId);

            if (!bookDoesExists) 
            {
                return NotFound();
            }

            var comment = mapper.Map<Comment>(commentCreateDTO);
            comment.BookId = bookId;
            comment.UserId = userId;
            context.Add(comment);
            await context.SaveChangesAsync();

            var commnentDTO = mapper.Map<CommentDTO>(comment);

            return CreatedAtRoute("GetComment", new { id= comment.Id, bookId= comment.BookId }, commnentDTO);
        }

        [HttpGet("{id:int}", Name ="GetComment")]
        public async Task<ActionResult<CommentDTO>> GetById(int id)
        {
            var comment = await context.Comments.SingleOrDefaultAsync(commentDb => commentDb.Id == id);

            if (comment == null) { return NotFound(); }

            return mapper.Map<CommentDTO>(comment);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int bookId, int id, CommentCreateDTO commentCreateDTO) 
        {
            var bookDoesExists = await context.Books.AnyAsync(bookBd => bookBd.Id == bookId);

            if (!bookDoesExists)
            {
                return NotFound();
            }

            var commentDoesExist = await context.Comments.AnyAsync(commentDb => commentDb.Id == id);

            if (!commentDoesExist) 
            {
                return NotFound();
            }

            var comment = mapper.Map<Comment>(commentCreateDTO);
            comment.Id = id;
            comment.BookId = bookId;

            context.Update(comment);
            await context.SaveChangesAsync();
            return NoContent();

        }
    }
}
