using AuthorsWebAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace AuthorsWebAPI.Entities
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
