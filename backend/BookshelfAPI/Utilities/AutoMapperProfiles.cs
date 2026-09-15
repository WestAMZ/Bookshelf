using Bookshelf.DTOs;
using Bookshelf.Entities;
using AutoMapper;

namespace Bookshelf.Utilities
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
            CreateMap<Genre, GenreDTO>();

            CreateMap<BookPatchDTO, Book>().ReverseMap();

            CreateMap<Book, BookDetailsDTO>()
                .ForMember(bookDTO => bookDTO.Authors, options => options.MapFrom(MapBookAuthorsDTO))
                .ForMember(bookDTO => bookDTO.Genres, options => options.MapFrom(MapBookGenresDTO));
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

        private List<AuthorDTO> MapBookAuthorsDTO(Book book, BookDetailsDTO bookDTO) 
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
        private List<GenreDTO> MapBookGenresDTO(Book book, BookDetailsDTO bookDTO)
        {
            if (book.BookGenres == null)
            {
                return new List<GenreDTO>();
            }

            return book.BookGenres
                .Where(bookGenre => bookGenre.Genre != null)
                .Select(bookGenre => new GenreDTO
                {
                    Id = bookGenre.GenreId,
                    Name = bookGenre.Genre.Name
                })
                .ToList();
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
