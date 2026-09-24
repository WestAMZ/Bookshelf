using Bookshelf.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (await context.Books.AnyAsync())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var users = await SeedUsersAsync(userManager);

            var authors = new List<Author>
            {
                new() { Name = "George Orwell", ImageUrl = "george_orwell.jpg" },
                new() { Name = "J.K. Rowling", ImageUrl = "jk_rowling.jpg" },
                new() { Name = "Stephen King", ImageUrl = "stephen_king.jpg" },
                new() { Name = "Isaac Asimov", ImageUrl = "isaac_asimov.jpg" },
                new() { Name = "Jane Austen", ImageUrl = "jane_austen.jpg" },
                new() { Name = "Gabriel García Márquez", ImageUrl = "gabriel_garcia_marquez.jpg" },
                new() { Name = "Agatha Christie", ImageUrl = "agatha_christie.jpg" },
                new() { Name = "J.R.R. Tolkien", ImageUrl = "jrr_tolkien.jpg" },
                new() { Name = "Yuval Noah Harari", ImageUrl = "yuval_noah_harari.jpg" },
                new() { Name = "Brandon Sanderson", ImageUrl = "brandon_sanderson.jpg" }
            };

            var genres = new List<Genre>
            {
                new() { Name = "Fantasy" },
                new() { Name = "Science Fiction" },
                new() { Name = "Mystery" },
                new() { Name = "Horror" },
                new() { Name = "Romance" },
                new() { Name = "History" },
                new() { Name = "Drama" },
                new() { Name = "Adventure" }
            };

            var books = new List<Book>
            {
                new() { Title = "1984", PublishedDate = new DateTime(1949, 6, 8), ImageUrl = "1984.jpg", Synopsis = "In the superstate of Oceania, Winston Smith works at the Ministry of Truth, where he rewrites historical records to match the Party's current version of reality. Increasingly alienated by a society controlled by surveillance, propaganda, and fear, Winston begins questioning the Party and secretly pursues a forbidden relationship with Julia. His search for truth brings him into conflict with the Party's mechanisms of power and exposes the consequences of rebellion against an authoritarian regime." },
                new() { Title = "Animal Farm", PublishedDate = new DateTime(1945, 8, 17), ImageUrl = "animal-farm.jpg", Synopsis = "On Manor Farm, the animals overthrow their human owner with the hope of creating a society based on equality and freedom. The pigs, led by Napoleon and supported by Squealer, gradually take control and reshape the farm's rules to consolidate their authority. As the animals' original ideals are replaced by propaganda, privilege, and oppression, the revolution becomes a study of how political power can be corrupted." },
                new() { Title = "Harry Potter and the Sorcerer's Stone", PublishedDate = new DateTime(1997, 6, 26), ImageUrl = "harry-potter-and-the-sorcerers-stone.jpg", Synopsis = "Harry Potter, an orphan who has grown up with his neglectful relatives, discovers on his eleventh birthday that he is a wizard. He is invited to attend Hogwarts School of Witchcraft and Wizardry, where he makes friends with Ron Weasley and Hermione Granger and learns about the magical world. During his first year, Harry and his friends uncover a mystery involving the philosopher's stone and a threat connected to the dark wizard Voldemort." },
                new() { Id = 4, Title = "Harry Potter and the Chamber of Secrets", PublishedDate = new DateTime(1998, 7, 2), ImageUrl = "harry-potter-and-the-chamber-of-secrets.jpg", Synopsis = "During his second year at Hogwarts, Harry Potter returns to find the school threatened by a mysterious force that has opened the Chamber of Secrets. Students are attacked and the school community is filled with fear as rumors spread about the chamber and its history. With Ron and Hermione, Harry investigates the mystery, discovers the connection to Tom Riddle's past, and confronts the creature hidden within the chamber." },
                new() { Id = 5, Title = "The Shining", PublishedDate = new DateTime(1977, 1, 28), ImageUrl = "the-shining.jpg", Synopsis = "Jack Torrance accepts a winter caretaker position at the isolated Overlook Hotel in Colorado, hoping the quiet setting will help him work on his writing and rebuild his family life. Jack, his wife Wendy, and their son Danny become trapped at the hotel when heavy snow cuts them off from the outside world. Danny's unusual psychic ability allows him to sense the hotel's disturbing history, while supernatural forces increasingly threaten the family." },
                new() { Id = 6, Title = "It", PublishedDate = new DateTime(1986, 9, 15), ImageUrl = "it.jpg", Synopsis = "Seven-year-old Georgie Denbrough disappears in the town of Derry, Maine, and his disappearance becomes connected to a terrifying presence that returns periodically to prey on children. Years later, a group of childhood friends known as the Losers' Club confronts the entity and later reunites as adults when the threat returns. Their struggle forces them to face both the supernatural evil in Derry and the fears and memories that have followed them into adulthood." },
                new() { Id = 7, Title = "Foundation", PublishedDate = new DateTime(1951, 6, 1), ImageUrl = "foundation.jpg", Synopsis = "In a distant future, mathematician Hari Seldon develops psychohistory, a science that uses mathematics to predict the broad movements of human societies. His calculations indicate that the Galactic Empire is approaching a long period of decline and chaos. Seldon establishes the Foundation on the remote planet Terminus to preserve human knowledge and shorten the coming dark age, while political pressures and rival powers challenge the new society." },
                new() { Id = 8, Title = "I, Robot", PublishedDate = new DateTime(1950, 12, 2), ImageUrl = "i-robot.jpg", Synopsis = "I, Robot is a collection of interconnected science-fiction stories about robots and the humans who design, use, and regulate them. Through characters such as robopsychologist Susan Calvin, the stories explore the practical and ethical problems created by increasingly sophisticated machines. The recurring Three Laws of Robotics become the basis for investigations into situations where apparently simple rules produce unexpected and complex behavior." },
                new() { Id = 9, Title = "Pride and Prejudice", PublishedDate = new DateTime(1813, 1, 28), ImageUrl = "pride-and-prejudice.jpg", Synopsis = "Elizabeth Bennet, one of five daughters in a family whose financial future depends on advantageous marriages, meets the wealthy and reserved Mr. Darcy. Their first impressions of each other are marked by misunderstanding and prejudice, while Elizabeth becomes involved with several relationships and family complications. As Elizabeth and Darcy learn more about one another, their assumptions are challenged and their relationship gradually changes." },
                new() { Id = 10, Title = "Emma", PublishedDate = new DateTime(1815, 12, 23), ImageUrl = "emma.jpg", Synopsis = "Emma Woodhouse is a wealthy young woman who enjoys arranging marriages among people around her, despite having no intention of marrying herself. She becomes convinced that her judgment is superior and attempts to guide the romantic lives of her friends, particularly Harriet Smith. Her plans repeatedly lead to misunderstandings, and Emma must gradually recognize her own mistakes and reconsider her feelings and relationships." },
                new() { Id = 11, Title = "One Hundred Years of Solitude", PublishedDate = new DateTime(1967, 5, 30), ImageUrl = "one-hundred-years-of-solitude.jpg", Synopsis = "The novel follows several generations of the Buendía family in the fictional town of Macondo, tracing the community's rise, transformation, and eventual decline. Family relationships, political conflicts, war, love, solitude, and recurring patterns of behavior shape the history of the town. Magical and extraordinary events are presented alongside everyday life, creating a multigenerational portrait in which history and memory repeatedly echo across generations." },
                new() { Id = 12, Title = "Love in the Time of Cholera", PublishedDate = new DateTime(1985, 9, 5), ImageUrl = "love-in-the-time-of-cholera.jpg", Synopsis = "In a Caribbean city during the late nineteenth and early twentieth centuries, Florentino Ariza falls deeply in love with Fermina Daza. She eventually marries Dr. Juvenal Urbino, a respected physician, while Florentino maintains his love for her for decades. After Urbino's death in old age, Florentino renews his declaration of love, and the two confront the passage of time, memory, and the meaning of enduring love." },
                new() { Id = 13, Title = "Murder on the Orient Express", PublishedDate = new DateTime(1934, 1, 1), ImageUrl = "murder-on-the-orient-express.jpg", Synopsis = "Aboard the luxurious Orient Express, detective Hercule Poirot is traveling across Europe when a passenger is found murdered during the journey. Heavy snow has stopped the train, leaving the murderer among the confined passengers. Poirot interviews the travelers and examines the evidence, uncovering a complicated web of relationships and motives before revealing the solution to the crime." },
                new() { Id = 14, Title = "Death on the Nile", PublishedDate = new DateTime(1937, 11, 1), ImageUrl = "death-on-the-nile.jpg", Synopsis = "Hercule Poirot travels along the Nile on a pleasure cruise in Egypt, where wealthy heiress Linnet Ridgeway is enjoying her honeymoon. The trip becomes a murder investigation after Linnet is killed and the circumstances make it clear that several passengers have possible motives. Poirot examines the relationships, secrets, and conflicting accounts among the travelers to identify the murderer." },
                new() { Id = 15, Title = "The Hobbit", PublishedDate = new DateTime(1937, 9, 21), ImageUrl = "the-hobbit.jpg", Synopsis = "Bilbo Baggins is a comfort-loving hobbit whose quiet life changes when the wizard Gandalf and a company of dwarves arrive at his home. They recruit Bilbo to accompany them on a journey to the Lonely Mountain, where the dwarves hope to reclaim their ancestral treasure from the dragon Smaug. Along the way, Bilbo encounters trolls, elves, goblins, giant spiders, and a mysterious creature named Gollum, while discovering unexpected courage and resourcefulness." },
                new() { Id = 16, Title = "The Lord of the Rings", PublishedDate = new DateTime(1954, 7, 29), ImageUrl = "the-lord-of-the-rings.jpg", Synopsis = "The Lord of the Rings follows Frodo Baggins after he learns that the ring inherited from his uncle Bilbo is the One Ring, a powerful artifact created by the dark lord Sauron. Frodo sets out from the Shire and eventually joins the Fellowship of the Ring, whose members seek to destroy the ring in the fires of Mount Doom. The journey leads them through Middle-earth as the Fellowship is divided and each character faces the growing threat of Sauron's return." },
                new() { Id = 17, Title = "Sapiens", PublishedDate = new DateTime(2011, 1, 1), ImageUrl = "sapiens.jpg", Synopsis = "Sapiens traces the history of Homo sapiens from the emergence of early humans through the Agricultural and Scientific Revolutions and into the modern era. Harari examines how shared myths, institutions, economic systems, and technologies allowed large numbers of humans to cooperate. The book connects major transformations in human society with changes in culture, politics, economics, and the environment." },
                new() { Id = 18, Title = "Homo Deus", PublishedDate = new DateTime(2015, 1, 1), ImageUrl = "homo-deus.jpg", Synopsis = "Homo Deus examines possible directions for humanity after modern societies have made significant progress in reducing famine, epidemic disease, and large-scale war. Harari considers how technologies such as artificial intelligence, biotechnology, and data-driven systems could change human priorities and institutions. The book explores ideas about immortality, happiness, and human enhancement while questioning what might happen if technological systems become increasingly capable of making decisions and processing information." },
                new() { Id = 19, Title = "Mistborn: The Final Empire", PublishedDate = new DateTime(2006, 7, 17), ImageUrl = "mistborn-the-final-empire.jpg", Synopsis = "In a world where ash falls from the sky and a powerful immortal ruler called the Lord Ruler has controlled the Final Empire for centuries, the skaa live under severe oppression. Vin, a young thief with hidden magical abilities, is recruited by Kelsier, a charismatic Mistborn who plans an ambitious rebellion against the empire. As Vin learns to use Allomancy and becomes involved with Kelsier's crew, she discovers that defeating the Lord Ruler requires confronting the history and mysteries behind his power." },
                new() { Id = 20, Title = "The Way of Kings", PublishedDate = new DateTime(2010, 8, 31), ImageUrl = "the-way-of-kings.jpg", Synopsis = "Kaladin, a young man from Alethkar, is forced into a life of slavery after a series of tragic events and eventually becomes a bridgeman in the army. At the same time, highprince Dalinar Kholin experiences visions that lead him to question the traditions and purpose of the Alethi war effort. The story also follows Shallan Davar, a young woman seeking knowledge under the guidance of the scholar Jasnah Kholin. Their paths unfold within the vast world of Roshar, where ancient forces and the return of supernatural powers threaten the established order." }
            };

            foreach (var author in authors)
            {
                author.Id = 0;
            }

            foreach (var genre in genres)
            {
                genre.Id = 0;
            }

            foreach (var book in books)
            {
                book.Id = 0;
            }

            context.Authors.AddRange(authors);
            context.Genres.AddRange(genres);
            context.Books.AddRange(books);
            await context.SaveChangesAsync();

            context.AuthorBooks.AddRange(Enumerable.Range(0, 20).Select(bookIndex => new AuthorBook
            {
                BookId = books[bookIndex].Id,
                AuthorId = authors[bookIndex / 2].Id,
                Order = 1
            }));
            context.BookGenres.AddRange(new[]
            {
                (1, 2), (2, 7), (3, 1), (4, 1), (5, 4), (6, 4), (7, 2), (8, 2), (9, 5), (10, 5),
                (11, 7), (12, 5), (13, 3), (14, 3), (15, 8), (16, 1), (17, 6), (18, 6), (19, 1), (20, 1)
            }.Select(pair => new BookGenre
            {
                BookId = books[pair.Item1 - 1].Id,
                GenreId = genres[pair.Item2 - 1].Id
            }));

            var comments = new[]
            {
                ("Excellent dystopian novel.", 1, 101), ("A timeless classic.", 1, 102), ("Very entertaining.", 3, 103),
                ("Perfect for young readers.", 3, 104), ("One of my favorite horror books.", 5, 105), ("A little too long but worth it.", 6, 106),
                ("Great introduction to science fiction.", 7, 107), ("Interesting robot stories.", 8, 108), ("Beautiful love story.", 9, 109),
                ("Fantastic character development.", 10, 110), ("Masterpiece of magical realism.", 11, 111), ("Very emotional.", 12, 112),
                ("Could not guess the ending.", 13, 113), ("Classic detective novel.", 14, 114), ("A wonderful adventure.", 15, 115),
                ("Epic fantasy.", 16, 116), ("Changed my perspective on history.", 17, 117), ("Thought-provoking.", 18, 118),
                ("Amazing magic system.", 19, 119), ("Outstanding world building.", 20, 120), ("Highly recommended.", 20, 121),
                ("Would read again.", 15, 122), ("Loved every chapter.", 11, 123), ("Not my favorite but still good.", 8, 124),
                ("Five stars.", 3, 125)
            }.Select(comment => new Comment
            {
                Content = comment.Item1,
                BookId = books[comment.Item2 - 1].Id,
                UserId = users[comment.Item3]
            });
            context.Comments.AddRange(comments);

            await context.SaveChangesAsync();
        }

        private static async Task<Dictionary<int, string>> SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var users = new Dictionary<int, string>();

            for (var userNumber = 101; userNumber <= 125; userNumber++)
            {
                var email = $"seed-user-{userNumber}@bookshelf.local";
                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                    var result = await userManager.CreateAsync(user, "Bookshelf123!");
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(string.Join("; ", result.Errors.Select(error => error.Description)));
                    }
                }

                users[userNumber] = user.Id;
            }

            return users;
        }
    }
}
