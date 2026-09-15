using Bookshelf.Validations;
using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
