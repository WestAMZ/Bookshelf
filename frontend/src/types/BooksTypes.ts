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
}

export interface BookDTOWithAuthors {
    id: number;
    title: string;
    imageUrl?: string | null;
    publishedDate: string;
    authors: AuthorDTO[];
    genres?: string[];
}