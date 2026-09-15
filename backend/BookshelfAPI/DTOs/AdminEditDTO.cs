using System.ComponentModel.DataAnnotations;

namespace Bookshelf.DTOs
{
    public class AdminEditDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
