import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router';
import { AUTHORS_IMAGES_URL, IMAGES_URL } from '../../const/global';
import { getAuthorById } from '../../services/AuthorsService';
import type { AuthorDTOWithBooks } from '../../types/AuthorsTypes';
import { formatDate } from '../../utils/date';
import { AUTHOR_PLACEHOLDER_IMAGE, BOOK_PLACEHOLDER_IMAGE, handleImageError } from '../../utils/image';

const getAuthorImageUrl = (imagePath?: string | null) => {
  if (!imagePath) {
    return AUTHOR_PLACEHOLDER_IMAGE;
  }

  return `${AUTHORS_IMAGES_URL}${imagePath}`;
};

const getBookImageUrl = (imagePath?: string | null) => {
  if (!imagePath) {
    return BOOK_PLACEHOLDER_IMAGE;
  }

  return `${IMAGES_URL}${imagePath}`;
};

export const AuthorDetailPage = () => {
  const { id } = useParams();
  const [author, setAuthor] = useState<AuthorDTOWithBooks | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const loadAuthor = async () => {
      try {
        setLoading(true);
        setError('');
        const data = await getAuthorById(Number(id));
        setAuthor(data);
      } catch {
        setError('Could not load author details.');
      } finally {
        setLoading(false);
      }
    };

    void loadAuthor();
  }, [id]);

  if (loading) {
    return <div className="px-6 py-8 text-sm text-slate-500">Loading author...</div>;
  }

  if (error || !author) {
    return (
      <div className="mx-auto max-w-3xl px-6 py-8">
        <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-700">{error || 'Author not found.'}</div>
      </div>
    );
  }

  return (
    <div className="mx-auto max-w-3xl px-6 py-8">
      <Link to="/authors" className="mb-6 inline-block text-sm font-medium text-cyan-600">← Back to authors</Link>
      <div className="rounded-2xl border border-slate-200 bg-white p-8 shadow-sm">
        <div className="flex flex-col gap-6 sm:flex-row sm:items-center">
          <img
            src={getAuthorImageUrl(author.imageUrl)}
            alt={author.name}
            onError={(event) => handleImageError(event, AUTHOR_PLACEHOLDER_IMAGE)}
            className="h-48 w-36 rounded-xl object-cover"
          />
          <h1 className="text-3xl font-semibold text-slate-900">{author.name}</h1>
        </div>

        <div className="mt-6">
          <h2 className="text-lg font-semibold text-slate-900">Books</h2>
          {author.books?.length ? (
            <ul className="mt-3 space-y-2">
              {author.books.map((book) => (
                <li key={book.id}>
                  <Link
                    to={`/books/${book.id}`}
                    className="flex items-center gap-4 rounded-lg bg-slate-50 px-4 py-3 text-sm text-slate-700 transition hover:bg-cyan-50 hover:text-cyan-700"
                  >
                    <img
                      src={getBookImageUrl(book.imageUrl)}
                      alt={book.title}
                      onError={(event) => handleImageError(event, BOOK_PLACEHOLDER_IMAGE)}
                      className="h-16 w-12 shrink-0 rounded object-cover"
                    />
                    <div>
                      <p className="font-medium">{book.title}</p>
                      <p className="mt-1 text-xs text-slate-500">Published: {formatDate(book.publishedDate)}</p>
                    </div>
                  </Link>
                </li>
              ))}
            </ul>
          ) : (
            <p className="mt-3 text-sm text-slate-500">No books registered for this author.</p>
          )}
        </div>
      </div>
    </div>
  );
};
