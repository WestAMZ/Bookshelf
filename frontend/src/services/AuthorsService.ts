import axios from 'axios';
import { API_URL } from '../const/global.ts';
import type { AuthorCreateDTO, AuthorDTO, AuthorDTOWithBooks } from '../types/AuthorsTypes';

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

export const getAllAuthors = async () => {
    const { data } = await api.get<AuthorDTO[]>('author');
    return data;
};

export const getAuthorById = async (id: number) => {
    const { data } = await api.get<AuthorDTOWithBooks>(`author/${id}`);
    return data;
};

export const getAuthorByName = async (name: string) => {
    const { data } = await api.get<AuthorDTO[]>(`author/${encodeURIComponent(name)}`);
    return data;
};

export const createAuthor = async (request: AuthorCreateDTO) => {
    const { data } = await api.post<AuthorDTO>('author', request);
    return data;
};

export const updateAuthor = async (id: number, request: AuthorCreateDTO) => {
    const { data } = await api.put<AuthorDTO>(`author/${id}`, request);
    return data;
};

export const deleteAuthor = async (id: number) => {
    const { data } = await api.delete(`author/${id}`);
    return data;
};
