import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router';
import { getBookById } from '../../services/BooksService';
import type { BookDTOWithAuthors } from '../../types/BooksTypes';

const getBookImageUrl = (imageUrl?: string | null) => {
  if (!imageUrl) {
    return 'https://via.placeholder.com/240x320?text=No+Image';
  }

  return `https://localhost:7038/images/books/${imageUrl}`;
};

export const BookDetailPage = () => {
  const { id } = useParams();
  const [book, setBook] = useState<BookDTOWithAuthors | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const loadBook = async () => {
      try {
        setLoading(true);
        setError('');
        const data = await getBookById(Number(id));
        setBook(data);
      } catch {
        setError('Could not load book details.');
      } finally {
        setLoading(false);
      }
    };

    void loadBook();
  }, [id]);

  if (loading) {
    return <div className="px-6 py-8 text-sm text-slate-500">Loading book...</div>;
  }

  if (error || !book) {
    return (
      <div className="mx-auto max-w-3xl px-6 py-8">
        <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-700">{error || 'Book not found.'}</div>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-3xl px-6 py-8">
      <Link to="/books" className="mb-6 inline-block text-sm font-medium text-cyan-600">← Back to books</Link>
      <div className="rounded-2xl border border-slate-200 bg-white p-8 shadow-sm">
        <div className="flex flex-col gap-6 md:flex-row">
          <img
            src={getBookImageUrl(book.imageUrl)}
            alt={book.title}
            className="h-72 w-full rounded-2xl object-cover md:w-64"
          />
          <div className="flex-1">
            <h1 className="text-3xl font-semibold text-slate-900">{book.title}</h1>
            <p className="mt-2 text-sm text-slate-500">Published: {book.publishedDate}</p>
            <div className="mt-3 flex flex-wrap gap-2">
              {(book.genres?.length ? book.genres : ['No genres']).map((genre) => (
                <span key={genre} className="rounded-full bg-cyan-50 px-2.5 py-1 text-xs font-medium text-cyan-700">
                  {genre}
                </span>
              ))}
            </div>
          </div>
        </div>

        <div className="mt-6">
          <h2 className="text-lg font-semibold text-slate-900">Authors</h2>
          {book.authors?.length ? (
            <ul className="mt-3 space-y-2">
              {book.authors.map((author) => (
                <li key={author.id} className="rounded-lg bg-slate-50 px-4 py-3 text-sm text-slate-700">
                  {author.firstName} {author.lastName}
                </li>
              ))}
            </ul>
          ) : (
            <p className="mt-3 text-sm text-slate-500">No authors linked to this book.</p>
          )}
        </div>
      </div>
    </div>
  );
};
