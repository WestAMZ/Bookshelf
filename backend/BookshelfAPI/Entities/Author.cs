using AuthorsWebAPI.Validations;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthorsWebAPI.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required - custom message")]
        [StringLength(maximumLength: 150, ErrorMessage = "Field {0} should not have more than {1} characters")]
        [FirstLetterCapital]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<AuthorBook> AuthorBooks { get; set; }
    }
}
