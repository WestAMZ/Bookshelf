import type { SyntheticEvent } from 'react';

export const BOOK_PLACEHOLDER_IMAGE = 'https://placehold.co/240x320?text=No+Image';
export const AUTHOR_PLACEHOLDER_IMAGE = 'https://placehold.co/240x320?text=No+Image';

export const handleImageError = (
  event: SyntheticEvent<HTMLImageElement>,
  fallbackImageUrl: string
) => {
  const image = event.currentTarget;
  image.onerror = null;
  image.src = fallbackImageUrl;
};
