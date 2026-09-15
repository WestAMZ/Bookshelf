using Bookshelf.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.DTOs
{
    public class BookPatchDTO
    {
        [FirstLetterCapital]
        [StringLength(maximumLength: 250)]
        [Required]
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
