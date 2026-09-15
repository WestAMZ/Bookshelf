using System.ComponentModel.DataAnnotations;

namespace AuthorsWebAPI.DTOs
{
    public class AdminEditDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
