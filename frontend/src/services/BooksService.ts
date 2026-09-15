import axios from 'axios';
import { API_URL } from '../const/global';
import type { BookCreateDTO, BookDTO, BookDTOWithAuthors } from '../types/BooksTypes';

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

export const getBookById = async (id: number) => {
    const { data } = await api.get<BookDTOWithAuthors>(`book/${id}`);
    return data;
};

export const createBook = async (request: BookCreateDTO) => {
    const { data } = await api.post<BookDTO>('book', request);
    return data;
};

export const updateBook = async (id: number, request: BookCreateDTO) => {
    const { data } = await api.put<BookDTO>(`book/${id}`, request);
    return data;
};

export const patchBook = async (id: number, operations: Array<{ op: string; path: string; value?: unknown }>) => {
    const { data } = await api.patch<BookDTO>(`book/${id}`, operations);
    return data;
};

export const deleteBook = async (id: number) => {
    const { data } = await api.delete(`book/${id}`);
    return data;
};

export const searchBooks = async (params?: Record<string, string | number | boolean | undefined>) => {
    const { data } = await api.get<BookDTOWithAuthors[]>('book/search', { params });
    return data;
};