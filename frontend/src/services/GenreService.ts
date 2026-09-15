import axios from 'axios';
import { API_URL } from '../const/global';

const api = axios.create({
  baseURL: API_URL,
  timeout: 5000,
  headers: {
    'Content-Type': 'application/json'
  }
});

const getStoredToken = () => {
  if (typeof window === 'undefined') {
    return null;
  }

  const storedUser = window.localStorage.getItem('user');

  if (!storedUser) {
    return null;
  }

  try {
    const parsedUser = JSON.parse(storedUser) as { token?: string; accessToken?: string; jwt?: string };
    return parsedUser.token ?? parsedUser.accessToken ?? parsedUser.jwt ?? null;
  } catch {
    return null;
  }
};

api.interceptors.request.use((config) => {
  const token = getStoredToken();

  if (token) {
    config.headers = config.headers ?? {};

    if (typeof config.headers.set === 'function') {
      config.headers.set('Authorization', `Bearer ${token}`);
    } else {
      config.headers.Authorization = `Bearer ${token}`;
    }
  }

  return config;
});

const normalizeGenres = (payload: unknown): string[] => {
  if (Array.isArray(payload)) {
    return payload.flatMap((item) => normalizeGenres(item));
  }

  if (!payload || typeof payload !== 'object') {
    return [];
  }

  const maybePayload = payload as Record<string, unknown>;

  const candidates = [
    maybePayload.genres,
    maybePayload.genre,
    maybePayload.items,
    maybePayload.data,
    maybePayload.result,
    maybePayload.results,
    maybePayload.values
  ];

  for (const candidate of candidates) {
    if (Array.isArray(candidate)) {
      const normalized = candidate.flatMap((item) => normalizeGenres(item));

      if (normalized.length) {
        return normalized;
      }
    }
  }

  const directName = maybePayload.name ?? maybePayload.genreName ?? maybePayload.value;

  if (typeof directName === 'string' && directName.trim()) {
    return [directName.trim()];
  }

  return [];
};

export const getGenres = async () => {
  const endpoints = ['genre', 'genres', 'book/genres', 'book/genre'];

  for (const endpoint of endpoints) {
    try {
      const { data } = await api.get<unknown>(endpoint);
      const genres = normalizeGenres(data);

      if (genres.length) {
        return genres;
      }
    } catch {
      // Ignore and try the next endpoint.
    }
  }

  return [];
};
