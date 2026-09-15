
/*==============================================================
    AUTHORS (10)
==============================================================*/
SET IDENTITY_INSERT dbo.Authors ON;
INSERT INTO Authors (Id, Name) VALUES
(1, 'George Orwell'),
(2, 'J.K. Rowling'),
(3, 'Stephen King'),
(4, 'Isaac Asimov'),
(5, 'Jane Austen'),
(6, 'Gabriel García Márquez'),
(7, 'Agatha Christie'),
(8, 'J.R.R. Tolkien'),
(9, 'Yuval Noah Harari'),
(10, 'Brandon Sanderson');
SET IDENTITY_INSERT dbo.Authors OFF;

/*==============================================================
    GENRES (8)
==============================================================*/
SET IDENTITY_INSERT dbo.Genres ON;
INSERT INTO Genres (Id, Name) VALUES
(1, 'Fantasy'),
(2, 'Science Fiction'),
(3, 'Mystery'),
(4, 'Horror'),
(5, 'Romance'),
(6, 'History'),
(7, 'Drama'),
(8, 'Adventure');
SET IDENTITY_INSERT dbo.Genres OFF;


/*==============================================================
    BOOKS (20)
==============================================================*/

-- DELETE FROM Books
SET IDENTITY_INSERT dbo.Books ON;
INSERT INTO Books (Id, Title, PublishedDate, ImageUrl) VALUES
(1, '1984', '1949-06-08', '1984.jpg'),
(2, 'Animal Farm', '1945-08-17', 'animal-farm.jpg'),
(3, 'Harry Potter and the Sorcerer''s Stone', '1997-06-26', 'harry-potter-and-the-sorcerers-stone.jpg'),
(4, 'Harry Potter and the Chamber of Secrets', '1998-07-02', 'harry-potter-and-the-chamber-of-secrets.jpg'),
(5, 'The Shining', '1977-01-28', 'the-shining.jpg'),
(6, 'It', '1986-09-15', 'it.jpg'),
(7, 'Foundation', '1951-06-01', 'foundation.jpg'),
(8, 'I, Robot', '1950-12-02', 'i-robot.jpg'),
(9, 'Pride and Prejudice', '1813-01-28', 'pride-and-prejudice.jpg'),
(10, 'Emma', '1815-12-23', 'emma.jpg'),
(11, 'One Hundred Years of Solitude', '1967-05-30', 'one-hundred-years-of-solitude.jpg'),
(12, 'Love in the Time of Cholera', '1985-09-05', 'love-in-the-time-of-cholera.jpg'),
(13, 'Murder on the Orient Express', '1934-01-01', 'murder-on-the-orient-express.jpg'),
(14, 'Death on the Nile', '1937-11-01', 'death-on-the-nile.jpg'),
(15, 'The Hobbit', '1937-09-21', 'the-hobbit.jpg'),
(16, 'The Lord of the Rings', '1954-07-29', 'the-lord-of-the-rings.jpg'),
(17, 'Sapiens', '2011-01-01', 'sapiens.jpg'),
(18, 'Homo Deus', '2015-01-01', 'homo-deus.jpg'),
(19, 'Mistborn: The Final Empire', '2006-07-17', 'mistborn-the-final-empire.jpg'),
(20, 'The Way of Kings', '2010-08-31', 'the-way-of-kings.jpg');
SET IDENTITY_INSERT dbo.Books OFF;

/*==============================================================
    AUTHORBOOKS
==============================================================*/
-- DELETE FROM AuthorBooks
INSERT INTO AuthorBooks (BookId, AuthorId, [Order]) VALUES
(1,1,1),
(2,1,1),
(3,2,1),
(4,2,1),
(5,3,1),
(6,3,1),
(7,4,1),
(8,4,1),
(9,5,1),
(10,5,1),
(11,6,1),
(12,6,1),
(13,7,1),
(14,7,1),
(15,8,1),
(16,8,1),
(17,9,1),
(18,9,1),
(19,10,1),
(20,10,1);


/*==============================================================
    BOOKGENRES
==============================================================*/

--  DELETE FROM BookGenres
INSERT INTO BookGenres (BookId, GenreId) VALUES
(1,2),
(2,7),
(3,1),
(4,1),
(5,4),
(6,4),
(7,2),
(8,2),
(9,5),
(10,5),
(11,7),
(12,5),
(13,3),
(14,3),
(15,8),
(16,1),
(17,6),
(18,6),
(19,1),
(20,1);


/*==============================================================
    COMMENTS
==============================================================*/

INSERT INTO Comments ([Content], BookId, UserId) VALUES
('Excellent dystopian novel.', 1, 101),
('A timeless classic.', 1, 102),
('Very entertaining.', 3, 103),
('Perfect for young readers.', 3, 104),
('One of my favorite horror books.', 5, 105),
('A little too long but worth it.', 6, 106),
('Great introduction to science fiction.', 7, 107),
('Interesting robot stories.', 8, 108),
('Beautiful love story.', 9, 109),
('Fantastic character development.', 10, 110),
('Masterpiece of magical realism.', 11, 111),
('Very emotional.', 12, 112),
('Could not guess the ending.', 13, 113),
('Classic detective novel.', 14, 114),
('A wonderful adventure.', 15, 115),
('Epic fantasy.', 16, 116),
('Changed my perspective on history.', 17, 117),
('Thought-provoking.', 18, 118),
('Amazing magic system.', 19, 119),
('Outstanding world building.', 20, 120),
('Highly recommended.', 20, 121),
('Would read again.', 15, 122),
('Loved every chapter.', 11, 123),
('Not my favorite but still good.', 8, 124),
('Five stars.', 3, 125);
