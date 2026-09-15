import { createBrowserRouter, Navigate } from "react-router";
import { MainLayout } from "./layouts/main/MainLayout";
import { HomPage } from "./pages/home/HomPage";
import { BooksPage } from "./pages/books/BooksPage";
import { BookDetailPage } from "./pages/books/BookDetailPage";
import { RegisterPage } from "./pages/auth/register/RegisterPage";
import { LoginPage } from "./pages/auth/login/LoginPage";
import { lazy, type ReactNode } from "react";
import { useAuth } from "./contexts/AuthContext";
import { AuthorsPage } from "./pages/authors/AuthorsPage";
import { AuthorDetailPage } from "./pages/authors/AuthorDetailPage";

const AuthLayout = lazy(() => import("./layouts/auth/AuthLayout"));

const ProtectedRoute = ({ children }: { children: ReactNode }) => {
    const { isLoggedIn } = useAuth();

    return isLoggedIn ? <>{children}</> : <Navigate to="/auth/login" replace />;
};

export const AppRouter = createBrowserRouter([
    {
        path: '/',
        element: (
            <ProtectedRoute>
                <MainLayout />
            </ProtectedRoute>
        ),
        children: [
            {
                index: true,
                element: <HomPage />
            },
            {
                path: 'books',
                element: <BooksPage />
            },
            {
                path: 'books/:id',
                element: <BookDetailPage />
            },
            {
                path: 'authors',
                element: <AuthorsPage />
            },
            {
                path: 'authors/:id',
                element: <AuthorDetailPage />
            }
        ]
    },
    {
        path: '/auth',
        element: <AuthLayout />,
        children: [
            {
                index: true,
                element: <Navigate to="login" />
            },
            {
                path: 'login',
                element: <LoginPage />
            },
            {
                path: 'register',
                element: <RegisterPage />
            }
        ]
    }
]);