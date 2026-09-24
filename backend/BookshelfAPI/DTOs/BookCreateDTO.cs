using Bookshelf.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.DTOs
{
    public class BookCreateDTO
    {
        [FirstLetterCapital]
        [StringLength(maximumLength:250)]
        [Required]
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        [StringLength(maximumLength: 5000)]
        public string Synopsis { get; set; }
        public List<int> AuthorIds { get; set; }
    }
}