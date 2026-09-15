import { useState, type ChangeEvent, type FormEvent } from 'react';
import { useNavigate } from 'react-router';
import { register } from '../../../services/AuthService';
import { useAuth } from '../../../contexts/AuthContext';
import type { UserCredentials } from '../../../types/AccountTypes';

interface ValidationErrorItem {
  code?: string;
  description?: string;
}

const extractValidationErrors = (error: unknown): string[] => {
  if (typeof error === 'object' && error !== null && 'response' in error) {
    const axiosError = error as { response?: { data?: unknown } };
    const data = axiosError.response?.data;

    if (Array.isArray(data)) {
      return data
        .map((item) => {
          if (typeof item === 'object' && item !== null) {
            const validationError = item as ValidationErrorItem;
            return validationError.description || validationError.code || '';
          }
          return '';
        })
        .filter(Boolean);
    }

    if (typeof data === 'string') {
      return [data];
    }
  }

  return [];
};

export const RegisterPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errors, setErrors] = useState<string[]>([]);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();
  const { logIn, logOut } = useAuth();

  const emailOnchange = (e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const passwordOnchange = (e: ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const registerHandle = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setErrors([]);

    const credentials: UserCredentials = {
      email,
      password
    };

    setIsSubmitting(true);

    try {
      await register(credentials, { logIn, logOut });
      navigate('/');
    } catch (error: unknown) {
      const validationErrors = extractValidationErrors(error);
      setErrors(validationErrors.length > 0 ? validationErrors : ['Registration could not be completed.']);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-950 flex items-center justify-center px-4">
      <div className="w-full max-w-md rounded-2xl border border-slate-800 bg-slate-900/90 p-8 shadow-2xl shadow-black/40">
        <div className="mb-8 text-center">
          <h1 className="text-3xl font-semibold text-white">Create account</h1>
          <p className="mt-2 text-sm text-slate-400">Sign up to get started</p>
        </div>

        <form onSubmit={registerHandle} className="space-y-4">
          <div>
            <label htmlFor="email" className="mb-2 block text-sm font-medium text-slate-300">
              Email address
            </label>
            <input
              id="email"
              type="email"
              value={email}
              onChange={emailOnchange}
              className="w-full rounded-lg border border-slate-700 bg-slate-800 px-4 py-3 text-white outline-none transition focus:border-cyan-500 focus:ring-2 focus:ring-cyan-500/30"
              placeholder="you@example.com"
              required
            />
          </div>

          <div>
            <label htmlFor="password" className="mb-2 block text-sm font-medium text-slate-300">
              Password
            </label>
            <input
              id="password"
              type="password"
              value={password}
              onChange={passwordOnchange}
              className="w-full rounded-lg border border-slate-700 bg-slate-800 px-4 py-3 text-white outline-none transition focus:border-cyan-500 focus:ring-2 focus:ring-cyan-500/30"
              placeholder="••••••••"
              required
            />
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full rounded-lg bg-cyan-600 px-4 py-3 font-semibold text-white transition hover:bg-cyan-500 disabled:cursor-not-allowed disabled:opacity-70"
          >
            {isSubmitting ? 'Creating account...' : 'Register'}
          </button>

          {errors.length > 0 && (
            <div className="rounded-lg border border-red-500/30 bg-red-500/10 p-3">
              <ul className="list-disc space-y-1 pl-5 text-sm text-red-300">
                {errors.map((message) => (
                  <li key={message}>{message}</li>
                ))}
              </ul>
            </div>
          )}

          <p className="text-center text-sm text-slate-400">
            Already have an account?{' '}
            <a href="/auth/login" className="font-medium text-cyan-400 transition hover:text-cyan-300">
              Go to login
            </a>
          </p>
        </form>
      </div>
    </div>
  );
};
