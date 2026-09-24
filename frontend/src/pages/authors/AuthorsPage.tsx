import { useEffect, useState, type FormEvent } from 'react';
import { Link } from 'react-router';
import { AUTHORS_IMAGES_URL } from '../../const/global';
import { getAllAuthors, getAuthorByName } from '../../services/AuthorsService';
import type { AuthorDTO } from '../../types/AuthorsTypes';
import { AUTHOR_PLACEHOLDER_IMAGE, handleImageError } from '../../utils/image';

const getAuthorImageUrl = (imagePath?: string | null) => {
  if (!imagePath) {
    return AUTHOR_PLACEHOLDER_IMAGE;
  }

  return `${AUTHORS_IMAGES_URL}${imagePath}`;
};

export const AuthorsPage = () => {
  const [authors, setAuthors] = useState<AuthorDTO[]>([]);
  const [query, setQuery] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const loadAuthors = async (name?: string) => {
    setLoading(true);
    setError('');

    try {
      const data = name ? await getAuthorByName(name) : await getAllAuthors();
      setAuthors(data);
    } catch {
      setError('Could not load authors.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    void loadAuthors();
  }, []);

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    void loadAuthors(query.trim());
  };

  return (
    <div className="mx-auto flex max-w-7xl flex-col gap-6 px-6 py-8">
      <div className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <div className="mb-4">
          <h1 className="text-2xl font-semibold text-slate-900">Authors</h1>
          <p className="mt-1 text-sm text-slate-500">Search authors by name and open their full profile.</p>
        </div>

        <form onSubmit={handleSubmit} className="flex flex-col gap-3 sm:flex-row">
          <input
            value={query}
            onChange={(event) => setQuery(event.target.value)}
            placeholder="Search by author name"
            className="flex-1 rounded-lg border border-slate-300 px-3 py-2 text-sm outline-none focus:border-cyan-500"
          />
          <button
            type="submit"
            className="rounded-lg bg-cyan-600 px-4 py-2 text-sm font-semibold text-white transition hover:bg-cyan-500"
          >
            Search
          </button>
        </form>
      </div>

      {error ? (
        <div className="rounded-lg border border-red-200 bg-red-50 p-4 text-sm text-red-700">{error}</div>
      ) : null}

      {loading ? (
        <div className="text-sm text-slate-500">Loading authors...</div>
      ) : (
        <div className="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
          {authors.map((author) => (
            <Link
              key={author.id}
              to={`/authors/${author.id}`}
              className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm transition hover:-translate-y-1 hover:shadow-md"
            >
              <img
                src={getAuthorImageUrl(author.imageUrl)}
                alt={author.name}
                onError={(event) => handleImageError(event, AUTHOR_PLACEHOLDER_IMAGE)}
                className="mb-4 h-56 w-full rounded-xl object-cover"
              />
              <h2 className="text-lg font-semibold text-slate-900">{author.name}</h2>
              <p className="mt-3 text-sm text-cyan-600">View details →</p>
            </Link>
          ))}

          {authors.length === 0 && !error ? (
            <div className="rounded-2xl border border-dashed border-slate-300 bg-slate-50 p-6 text-sm text-slate-500 md:col-span-2 xl:col-span-3">
              No authors found.
            </div>
          ) : null}
        </div>
      )}
    </div>
  );
};
