using AuthorsWebAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace AuthorsWebAPI.DTOs
{
    public class AuthorCreateDTO
    {
        [Required(ErrorMessage = "The field {0} is required - custom message")]
        [StringLength(maximumLength: 150, ErrorMessage = "Field {0} should not have more than {1} characters")]
        [FirstLetterCapital]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
