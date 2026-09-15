import { useEffect, useState, type FormEvent } from 'react';
import { Link } from 'react-router';
import { searchBooks } from '../../services/BooksService';
import { getGenres } from '../../services/GenreService';
import type { BookDTOWithAuthors } from '../../types/BooksTypes';

const getBookImageUrl = (imageUrl?: string | null) => {
  if (!imageUrl) {
    return 'https://via.placeholder.com/240x320?text=No+Image';
  }

  return `https://localhost:7038/images/books/${imageUrl}`;
};

export const BooksPage = () => {
  const [books, setBooks] = useState<BookDTOWithAuthors[]>([]);
  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [genre, setGenre] = useState('');
  const [publishedDate, setPublishedDate] = useState('');
  const [genres, setGenres] = useState<string[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const loadBooks = async (filters?: Record<string, string | undefined>) => {
    setLoading(true);
    setError('');

    try {
      const normalizedFilters = Object.fromEntries(
        Object.entries(filters ?? {}).filter(([, value]) => Boolean(value))
      );

      const data = await searchBooks(normalizedFilters);
      setBooks(data);
    } catch {
      setError('Could not load books.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const loadGenres = async () => {
      try {
        const availableGenres = await getGenres();
        setGenres(availableGenres);
      } catch {
        setGenres([]);
      }
    };

    void loadBooks();
    void loadGenres();
  }, []);

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    void loadBooks({
      title: title || undefined,
      author: author || undefined,
      genre: genre || undefined,
      publishedDate: publishedDate || undefined
    });
  };

  return (
    <div className="mx-auto flex max-w-7xl flex-col gap-6 px-6 py-8">
      <div className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="mb-4 flex items-center justify-between">
          <div>
            <h1 className="text-2xl font-semibold text-slate-900">Books</h1>
            <p className="mt-1 text-sm text-slate-500">Search books by title, author, genre or publication date.</p>
          </div>
        </div>

        <form onSubmit={handleSubmit} className="grid gap-4 md:grid-cols-2 xl:grid-cols-4">
          <input
            value={title}
            onChange={(event) => setTitle(event.target.value)}
            placeholder="Title"
            className="rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-cyan-500"
          />
          <input
            value={author}
            onChange={(event) => setAuthor(event.target.value)}
            placeholder="Author"
            className="rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-cyan-500"
          />
          <select
            value={genre}
            onChange={(event) => setGenre(event.target.value)}
            className="rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-cyan-500"
          >
            <option value="">All genres</option>
            {genres.map((availableGenre) => (
              <option key={availableGenre} value={availableGenre}>
                {availableGenre}
              </option>
            ))}
          </select>
          <input
            type="date"
            value={publishedDate}
            onChange={(event) => setPublishedDate(event.target.value)}
            className="rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-cyan-500"
          />
          <button
            type="submit"
            className="rounded-lg bg-cyan-600 px-4 py-2 text-sm font-semibold text-white transition hover:bg-cyan-500 md:col-span-2 xl:col-span-1"
          >
            Search
          </button>
        </form>
      </div>

      {error ? (
        <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-700">{error}</div>
      ) : null}

      {loading ? (
        <div className="text-sm text-slate-500">Loading books...</div>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
          {books.map((book) => (
            <Link
              key={book.id}
              to={`/books/${book.id}`}
              className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-1 hover:shadow-md"
            >
              <img
                src={getBookImageUrl(book.imageUrl)}
                alt={book.title}
                className="mb-4 h-56 w-full rounded-xl object-cover"
              />
              <h2 className="text-lg font-semibold text-slate-900">{book.title}</h2>
              <p className="mt-2 text-sm text-slate-500">Published: {book.publishedDate}</p>
              <p className="mt-2 text-sm text-slate-700">
                Author: {book.authors?.length ? book.authors.map((author) => `${author.firstName} ${author.lastName}`.trim()).join(', ') : 'Unknown'}
              </p>
              <div className="mt-2 flex flex-wrap gap-2">
                {(book.genres?.length ? book.genres : ['No genres']).map((item) => (
                  <span key={item} className="rounded-full bg-cyan-50 px-2.5 py-1 text-xs font-medium text-cyan-700">
                    {item}
                  </span>
                ))}
              </div>
              <p className="mt-3 text-sm text-cyan-600">View details →</p>
            </Link>
          ))}

          {books.length === 0 && !error ? (
            <div className="rounded-2xl border border-dashed border-slate-300 bg-slate-50 p-6 text-sm text-slate-500 md:col-span-2 xl:col-span-3">
              No books found for the selected filters.
            </div>
          ) : null}
        </div>
      )}
    </div>
  );
};
