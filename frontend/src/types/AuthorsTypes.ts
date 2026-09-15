import type { BookDTO } from "./BooksTypes";

export interface AuthorCreateDTO {
    name: string;
    imageUrl?: string | null;
}

export interface AuthorDTO {
    id: number;
    name: string;
    imageUrl?: string | null;
}

export interface AuthorDTOWithBooks {
    id: number;
    name: string;
    imageUrl?: string | null;
    books: BookDTO[];
}