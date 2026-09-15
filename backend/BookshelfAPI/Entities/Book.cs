using Bookshelf.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [FirstLetterCapital]
        [StringLength(maximumLength: 250)]
        public string Title { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public List<Comment> Comments { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
        public List<BookGenre> BookGenres { get; set; }

    }
}
