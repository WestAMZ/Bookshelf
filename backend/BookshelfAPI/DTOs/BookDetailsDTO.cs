namespace Bookshelf.DTOs
{
    public class BookDetailsDTO: BookDTO
    {
        public List<AuthorDTO> Authors { get; set; }
        public List<GenreDTO> Genres { get; set; }
    }
}