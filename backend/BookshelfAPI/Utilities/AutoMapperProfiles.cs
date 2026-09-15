using AuthorsWebAPI.DTOs;
using AuthorsWebAPI.Entities;
using AutoMapper;

namespace AuthorsWebAPI.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AuthorCreateDTO, Author>();
            CreateMap<Author, AuthorDTO>();
            CreateMap<Author, AuthorDTOWithBooks>()
                .ForMember(author => author.Books,  options => options.MapFrom(MapAuthorDTOBooks));
            CreateMap<BookCreateDTO, Book>()
                .ForMember(book => book.AuthorBooks, options => options.MapFrom(MapAuthorBooks));
            CreateMap<Book, BookDTO>();

            CreateMap<BookPatchDTO, Book>().ReverseMap();

            CreateMap<Book, BookDTOWithAuthors>()
                .ForMember(bookDTO => bookDTO.Authors, options => options.MapFrom(MapBookAuthorsDTO));
            CreateMap<CommentCreateDTO,Comment>();
            CreateMap<Comment, CommentDTO>();
        }
        private List<AuthorBook> MapAuthorBooks(BookCreateDTO bookCreateDTO, Book book) 
        {
            var result = new List<AuthorBook>();

            if (bookCreateDTO.AuthorIds == null) 
            {
                return result;
            }

            foreach (var authorId in bookCreateDTO.AuthorIds)
            {
                result.Add(new AuthorBook() { AuthorId = authorId });
            }

            return result;
        }

        private List<AuthorDTO> MapBookAuthorsDTO(Book book, BookDTO bookDTO) 
        {
            var result = new List<AuthorDTO>();

            if (book.AuthorBooks == null) { return result; }

            foreach (var authorBook in book.AuthorBooks)
            {
                result.Add(new AuthorDTO()
                {
                    Id = authorBook.AuthorId,
                    Name = authorBook.Author.Name,
                    ImageUrl = authorBook.Author.ImageUrl
                });
            }
            return result;
        }
        private List<BookDTO> MapAuthorDTOBooks(Author author, AuthorDTO authorDTO)
        {
            var result = new List<BookDTO>();

            if (author.AuthorBooks == null) { return result; }

            foreach (var authorBook in author.AuthorBooks)
            {
                result.Add(new BookDTO() 
                {
                    Id = authorBook.BookId,
                    Title = authorBook.Book.Title,
                    ImageUrl = authorBook.Book.ImageUrl
                });
            }

            return result;
        }
    }
}
