import type { AuthorDTO } from "./AuthorsTypes";

export interface BookCreateDTO {
    title: string;
    imageUrl?: string | null;
    publishedDate: string;
    authorIds: number[];
}

export interface BookDTO {
    id: number;
    title: string;
    imageUrl?: string | null;
    publishedDate: string;
    synopsis?: string | null;
}

export interface GenreDTO {
    id: number;
    name: string;
}

export interface BookDetailsDTO extends BookDTO {
    authors: AuthorDTO[];
    genres: GenreDTO[];
}