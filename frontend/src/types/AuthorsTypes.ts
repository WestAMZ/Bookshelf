import type { BookDTO } from "./BooksTypes";

export interface AuthorCreateDTO {
    firstName: string;
    lastName: string;
    birthDate: string;
}

export interface AuthorDTO {
    id: number;
    firstName: string;
    lastName: string;
    birthDate: string;
}

export interface AuthorDTOWithBooks {
    id: number;
    firstName: string;
    lastName: string;
    birthDate: string;
    books: BookDTO[];
}