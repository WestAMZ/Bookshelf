import axios from 'axios';
import type { UserCredentials, AuthenticationResponse } from '../types/AccountTypes';
import { API_URL } from '../const/global.ts';

const api = axios.create({
    baseURL: API_URL,
    timeout: 5000,
    headers: {
        'Content-Type': 'application/json'
    }
});

interface AuthHandlers {
    logIn: (user: AuthenticationResponse) => void;
    logOut: () => void;
}

const register = async (request: UserCredentials, handlers: AuthHandlers) => {
    try {
        const { data } = await api.post<AuthenticationResponse>('account/register', request, {
            headers: {
                Accept: 'text/plain',
                'Content-Type': 'application/json-patch+json'
            }
        });
        handlers.logIn(data);
        return data;
    } catch (error) {
        console.error(error);
        throw error;
    }
};

const login = async (request: UserCredentials, handlers: AuthHandlers) => {
    try {
        const { data } = await api.post<AuthenticationResponse>('account/login', request, {
            headers: {
                Accept: 'text/plain',
                'Content-Type': 'application/json-patch+json'
            }
        });
        handlers.logIn(data);
        return data;
    } catch (error) {
        console.error(error);
        throw error;
    }
};

const logout = (handlers: AuthHandlers) => {
    handlers.logOut();
};

export {
    register,
    login,
    logout
};