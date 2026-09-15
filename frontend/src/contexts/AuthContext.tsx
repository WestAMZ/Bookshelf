import React, { useState, useEffect, useContext, type ReactNode } from 'react';
import type { AuthenticationResponse } from '../types/AccountTypes';

interface AuthContextType {
    authUser: AuthenticationResponse;
    isLoggedIn: boolean;
    logOut: () => void;
    logIn: (user: AuthenticationResponse) => void;
}

const emptyUser: AuthenticationResponse = {
    email: '',
    token: '',
    expiration: ''
};

const defaultAuthContext: AuthContextType = {
    authUser: emptyUser,
    isLoggedIn: false,
    logOut: () => undefined,
    logIn: () => undefined
};

const AuthContext = React.createContext<AuthContextType>(defaultAuthContext);

export function useAuth() {
    return useContext(AuthContext);
}

interface AuthProviderProps {
    children: ReactNode;
}

const getStoredAuthUser = (): AuthenticationResponse => {
    if (typeof window === 'undefined') {
        return emptyUser;
    }

    const storedUser = window.localStorage.getItem('user');

    if (!storedUser) {
        return emptyUser;
    }

    try {
        return JSON.parse(storedUser) as AuthenticationResponse;
    } catch {
        window.localStorage.removeItem('user');
        return emptyUser;
    }
};

export function AuthProvider({ children }: AuthProviderProps) {
    const [authUser, setAuthUser] = useState<AuthenticationResponse>(() => getStoredAuthUser());
    const [isLoggedIn, setIsLoggedIn] = useState(() => Boolean(getStoredAuthUser().token));

    const logIn = (user: AuthenticationResponse) => {
        window.localStorage.setItem('user', JSON.stringify(user));
        setAuthUser(user);
        setIsLoggedIn(Boolean(user.token));
    };

    const logOut = () => {
        window.localStorage.removeItem('user');
        setAuthUser(emptyUser);
        setIsLoggedIn(false);
    };

    useEffect(() => {
        const storedUser = getStoredAuthUser();

        if (storedUser.token) {
            setAuthUser(storedUser);
            setIsLoggedIn(true);
        } else {
            logOut();
        }
    }, []);

    const value: AuthContextType = {
        authUser,
        isLoggedIn,
        logOut,
        logIn
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}