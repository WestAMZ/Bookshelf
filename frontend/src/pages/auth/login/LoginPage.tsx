import { useState, type ChangeEvent, type FormEvent } from 'react';
import { useNavigate } from 'react-router';
import { login } from '../../../services/AuthService';
import { useAuth } from '../../../contexts/AuthContext';
import type { UserCredentials } from '../../../types/AccountTypes';

export const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();
  const { logIn, logOut } = useAuth();

  const emailOnchange = (e: ChangeEvent<HTMLInputElement>) => {
    setEmail(e.target.value);
  };

  const passwordOnchange = (e: ChangeEvent<HTMLInputElement>) => {
    setPassword(e.target.value);
  };

  const loginHandle = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setError('');

    const credentials: UserCredentials = {
      email,
      password
    };

    setIsSubmitting(true);

    try {
      await login(credentials, { logIn, logOut });
      navigate('/');
    } catch {
      setError('Credenciales inválidas o error de conexión.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="min-h-screen bg-slate-950 flex items-center justify-center px-4">
      <div className="w-full max-w-md rounded-2xl border border-slate-800 bg-slate-900/90 p-8 shadow-2xl shadow-black/40">
        <div className="mb-8 text-center">
          <h1 className="text-3xl font-semibold text-white">Sign in</h1>
          <p className="mt-2 text-sm text-slate-400">Access your account to continue</p>
        </div>

        <form onSubmit={loginHandle} className="space-y-4">
          <div>
            <label htmlFor="email" className="mb-2 block text-sm font-medium text-slate-300">
              Email
            </label>
            <input
              id="email"
              type="email"
              value={email}
              onChange={emailOnchange}
              className="w-full rounded-lg border border-slate-700 bg-slate-800 px-4 py-3 text-white outline-none transition focus:border-cyan-500 focus:ring-2 focus:ring-cyan-500/30"
              placeholder="your@email.com"
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
            {isSubmitting ? 'Signing in...' : 'Sign in'}
          </button>

          <div className="pt-2 text-center text-sm text-slate-400">
            <span>Don&apos;t have an account? </span>
            <button
              type="button"
              onClick={() => navigate('/auth/register')}
              className="font-medium text-cyan-400 transition hover:text-cyan-300"
            >
              Create one
            </button>
          </div>

          {error && <p className="text-sm text-red-400">{error}</p>}
        </form>
      </div>
    </div>
  );
};
