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
                new() { Name = "Brandon Sanderson", ImageUrl = "brandon_sanderson.jpg" },

                // New authors
                new() { Name = "Frank Herbert", ImageUrl = "frank_herbert.jpg" },
                new() { Name = "Aldous Huxley", ImageUrl = "aldous_huxley.jpg" },
                new() { Name = "Ray Bradbury", ImageUrl = "ray_bradbury.jpg" },
                new() { Name = "F. Scott Fitzgerald", ImageUrl = "f_scott_fitzgerald.jpg" },
                new() { Name = "Leo Tolstoy", ImageUrl = "leo_tolstoy.jpg" },
                new() { Name = "Victor Hugo", ImageUrl = "victor_hugo.jpg" },
                new() { Name = "Mary Shelley", ImageUrl = "mary_shelley.jpg" },
                new() { Name = "Douglas Adams", ImageUrl = "douglas_adams.jpg" },
                new() { Name = "Neil Gaiman", ImageUrl = "neil_gaiman.jpg" },
                new() { Name = "Terry Pratchett", ImageUrl = "terry_pratchett.jpg" },
                new() { Name = "Homer", ImageUrl = "homer.jpg" },
                new() { Name = "Dante Alighieri", ImageUrl = "dante_alighieri.jpg" }
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
                new()
                {
                    Title = "1984",
                    PublishedDate = new DateTime(1949, 6, 8),
                    ImageUrl = "1984.jpg",
                },
                new()
                {
                    Title = "Animal Farm",
                    PublishedDate = new DateTime(1945, 8, 17),
                    ImageUrl = "animal-farm.jpg",
                    Synopsis = "On Manor Farm, the animals overthrow their human owner with the hope of creating a society based on equality and freedom. The pigs, led by Napoleon and supported by Squealer, gradually take control and reshape the farm's rules to consolidate their authority. As the animals' original ideals are replaced by propaganda, privilege, and oppression, the revolution becomes a study of how political power can be corrupted."
                },
                new()
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                    PublishedDate = new DateTime(1997, 6, 26),
                    ImageUrl = "harry-potter-and-the-sorcerers-stone.jpg",
                    Synopsis = "Harry Potter, an orphan who has grown up with his neglectful relatives, discovers on his eleventh birthday that he is a wizard. He is invited to attend Hogwarts School of Witchcraft and Wizardry, where he makes friends with Ron Weasley and Hermione Granger and learns about the magical world. During his first year, Harry and his friends uncover a mystery involving the philosopher's stone and a threat connected to the dark wizard Voldemort."
                },
                new()
                {
                    Title = "Harry Potter and the Chamber of Secrets",
                    PublishedDate = new DateTime(1998, 7, 2),
                    ImageUrl = "harry-potter-and-the-chamber-of-secrets.jpg",
                    Synopsis = "During his second year at Hogwarts, Harry Potter returns to find the school threatened by a mysterious force that has opened the Chamber of Secrets. Students are attacked and the school community is filled with fear as rumors spread about the chamber and its history. With Ron and Hermione, Harry investigates the mystery, discovers the connection to Tom Riddle's past, and confronts the creature hidden within the chamber."
                },
                new()
                {
                    Title = "The Shining",
                    PublishedDate = new DateTime(1977, 1, 28),
                    ImageUrl = "the-shining.jpg",
                    Synopsis = "Jack Torrance accepts a winter caretaker position at the isolated Overlook Hotel in Colorado, hoping the quiet setting will help him work on his writing and rebuild his family life. Jack, his wife Wendy, and their son Danny become trapped at the hotel when heavy snow cuts them off from the outside world. Danny's unusual psychic ability allows him to sense the hotel's disturbing history, while supernatural forces increasingly threaten the family."
                },
                new()
                {
                    Title = "It",
                    PublishedDate = new DateTime(1986, 9, 15),
                    ImageUrl = "It.jpg",
                    Synopsis = "Seven-year-old Georgie Denbrough disappears in the town of Derry, Maine, and his disappearance becomes connected to a terrifying presence that returns periodically to prey on children. Years later, a group of childhood friends known as the Losers' Club confronts the entity and later reunites as adults when the threat returns. Their struggle forces them to face both the supernatural evil in Derry and the fears and memories that have followed them into adulthood."
                },
                new()
                {
                    Title = "Foundation",
                    PublishedDate = new DateTime(1951, 6, 1),
                    ImageUrl = "Foundation.jpg",
                    Synopsis = "In a distant future, mathematician Hari Seldon develops psychohistory, a science that uses mathematics to predict the broad movements of human societies. His calculations indicate that the Galactic Empire is approaching a long period of decline and chaos. Seldon establishes the Foundation on the remote planet Terminus to preserve human knowledge and shorten the coming dark age, while political pressures and rival powers challenge the new society."
                },
                new()
                {
                    Title = "I, Robot",
                    PublishedDate = new DateTime(1950, 12, 2),
                    ImageUrl = "i-robot.jpg",
                    Synopsis = "I, Robot is a collection of interconnected science-fiction stories about robots and the humans who design, use, and regulate them. Through characters such as robopsychologist Susan Calvin, the stories explore the practical and ethical problems created by increasingly sophisticated machines. The recurring Three Laws of Robotics become the basis for investigations into situations where apparently simple rules produce unexpected and complex behavior."
                },
                new()
                {
                    Title = "Pride and Prejudice",
                    PublishedDate = new DateTime(1813, 1, 28),
                    ImageUrl = "pride-and-prejudice.jpg",
                    Synopsis = "Elizabeth Bennet, one of five daughters in a family whose financial future depends on advantageous marriages, meets the wealthy and reserved Mr. Darcy. Their first impressions of each other are marked by misunderstanding and prejudice, while Elizabeth becomes involved with several relationships and family complications. As Elizabeth and Darcy learn more about one another, their assumptions are challenged and their relationship gradually changes."
                },
                new()
                {
                    Title = "Emma",
                    PublishedDate = new DateTime(1815, 12, 23),
                    ImageUrl = "Emma.jpg",
                    Synopsis = "Emma Woodhouse is a wealthy young woman who enjoys arranging marriages among people around her, despite having no intention of marrying herself. She becomes convinced that her judgment is superior and attempts to guide the romantic lives of her friends, particularly Harriet Smith. Her plans repeatedly lead to misunderstandings, and Emma must gradually recognize her own mistakes and reconsider her feelings and relationships."
                },
                new()
                {
                    Title = "One Hundred Years of Solitude",
                    PublishedDate = new DateTime(1967, 5, 30),
                    ImageUrl = "one-hundred-years-of-solitude.jpg",
                    Synopsis = "The novel follows several generations of the Buendía family in the fictional town of Macondo, tracing the community's rise, transformation, and eventual decline. Family relationships, political conflicts, war, love, solitude, and recurring patterns of behavior shape the history of the town. Magical and extraordinary events are presented alongside everyday life, creating a multigenerational portrait in which history and memory repeatedly echo across generations."
                },
                new()
                {
                    Title = "Love in the Time of Cholera",
                    PublishedDate = new DateTime(1985, 9, 5),
                    ImageUrl = "love-in-the-time-of-cholera.jpg",
                    Synopsis = "In a Caribbean city during the late nineteenth and early twentieth centuries, Florentino Ariza falls deeply in love with Fermina Daza. She eventually marries Dr. Juvenal Urbino, a respected physician, while Florentino maintains his love for her for decades. After Urbino's death in old age, Florentino renews his declaration of love, and the two confront the passage of time, memory, and the meaning of enduring love."
                },
                new()
                {
                    Title = "Murder on the Orient Express",
                    PublishedDate = new DateTime(1934, 1, 1),
                    ImageUrl = "murder-on-the-orient-express.jpg",
                    Synopsis = "Aboard the luxurious Orient Express, detective Hercule Poirot is traveling across Europe when a passenger is found murdered during the journey. Heavy snow has stopped the train, leaving the murderer among the confined passengers. Poirot interviews the travelers and examines the evidence, uncovering a complicated web of relationships and motives before revealing the solution to the crime."
                },
                new()
                {
                    Title = "Death on the Nile",
                    PublishedDate = new DateTime(1937, 11, 1),
                    ImageUrl = "death-on-the-nile.jpg",
                    Synopsis = "Hercule Poirot travels along the Nile on a pleasure cruise in Egypt, where wealthy heiress Linnet Ridgeway is enjoying her honeymoon. The trip becomes a murder investigation after Linnet is killed and the circumstances make it clear that several passengers have possible motives. Poirot examines the relationships, secrets, and conflicting accounts among the travelers to identify the murderer."
                },
                new()
                {
                    Title = "The Hobbit",
                    PublishedDate = new DateTime(1937, 9, 21),
                    ImageUrl = "the-hobbit.jpg",
                    Synopsis = "Bilbo Baggins is a comfort-loving hobbit whose quiet life changes when the wizard Gandalf and a company of dwarves arrive at his home. They recruit Bilbo to accompany them on a journey to the Lonely Mountain, where the dwarves hope to reclaim their ancestral treasure from the dragon Smaug. Along the way, Bilbo encounters trolls, elves, goblins, giant spiders, and a mysterious creature named Gollum, while discovering unexpected courage and resourcefulness."
                },
                new()
                {
                    Title = "The Lord of the Rings",
                    PublishedDate = new DateTime(1954, 7, 29),
                    ImageUrl = "the-lord-of-the-rings.jpg",
                    Synopsis = "The Lord of the Rings follows Frodo Baggins after he learns that the ring inherited from his uncle Bilbo is the One Ring, a powerful artifact created by the dark lord Sauron. Frodo sets out from the Shire and eventually joins the Fellowship of the Ring, whose members seek to destroy the ring in the fires of Mount Doom. The journey leads them through Middle-earth as the Fellowship is divided and each character faces the growing threat of Sauron's return."
                },
                new()
                {
                    Title = "Sapiens",
                    PublishedDate = new DateTime(2011, 1, 1),
                    ImageUrl = "Sapiens.jpg",
                    Synopsis = "Sapiens traces the history of Homo sapiens from the emergence of early humans through the Agricultural and Scientific Revolutions and into the modern era. Harari examines how shared myths, institutions, economic systems, and technologies allowed large numbers of humans to cooperate. The book connects major transformations in human society with changes in culture, politics, economics, and the environment."
                },
                new()
                {
                    Title = "Homo Deus",
                    PublishedDate = new DateTime(2015, 1, 1),
                    ImageUrl = "homo-deus.jpg",
                    Synopsis = "Homo Deus examines possible directions for humanity after modern societies have made significant progress in reducing famine, epidemic disease, and large-scale war. Harari considers how technologies such as artificial intelligence, biotechnology, and data-driven systems could change human priorities and institutions. The book explores ideas about immortality, happiness, and human enhancement while questioning what might happen if technological systems become increasingly capable of making decisions and processing information."
                },
                new()
                {
                    Title = "Mistborn: The Final Empire",
                    PublishedDate = new DateTime(2006, 7, 17),
                    ImageUrl = "mistborn-the-final-empire.jpg",
                    Synopsis = "In a world where ash falls from the sky and a powerful immortal ruler called the Lord Ruler has controlled the Final Empire for centuries, the skaa live under severe oppression. Vin, a young thief with hidden magical abilities, is recruited by Kelsier, a charismatic Mistborn who plans an ambitious rebellion against the empire. As Vin learns to use Allomancy and becomes involved with Kelsier's crew, she discovers that defeating the Lord Ruler requires confronting the history and mysteries behind his power."
                },
                new()
                {
                    Title = "The Way of Kings",
                    PublishedDate = new DateTime(2010, 8, 31),
                    ImageUrl = "the-way-of-kings.jpg",
                    Synopsis = "Kaladin, a young man from Alethkar, is forced into a life of slavery after a series of tragic events and eventually becomes a bridgeman in the army. At the same time, highprince Dalinar Kholin experiences visions that lead him to question the traditions and purpose of the Alethi war effort. The story also follows Shallan Davar, a young woman seeking knowledge under the guidance of the scholar Jasnah Kholin. Their paths unfold within the vast world of Roshar, where ancient forces and the return of supernatural powers threaten the established order."
                },

                // 21-40: New books
                new()
                {
                    Title = "Dune",
                    PublishedDate = new DateTime(1965, 8, 1),
                    ImageUrl = "dune.jpg",
                    Synopsis = "Paul Atreides and his family are entrusted with the desert planet Arrakis, the only known source of the valuable spice melange. Political betrayal forces Paul and his mother Jessica into the harsh desert, where they encounter the Fremen and become involved in a conflict that could reshape the future of the galaxy. The novel explores politics, ecology, religion, power, and human adaptation."
                },
                new()
                {
                    Title = "Brave New World",
                    PublishedDate = new DateTime(1932, 1, 1),
                    ImageUrl = "brave_new_world.jpg",
                    Synopsis = "In a technologically advanced future society, humans are genetically conditioned into predetermined social classes and kept content through consumerism, entertainment, and a drug called soma. Bernard Marx and Lenina Crowne encounter John, a young man raised outside the World State, whose values challenge the society's assumptions. Their experiences raise questions about freedom, individuality, happiness, and social control."
                },
                new()
                {
                    Title = "Fahrenheit 451",
                    PublishedDate = new DateTime(1953, 10, 19),
                    ImageUrl = "fahrenheit_451.jpg",
                    Synopsis = "Guy Montag works as a fireman in a society where books are forbidden and firemen burn them rather than extinguishing fires. After meeting his curious young neighbor Clarisse, Montag begins questioning the society he serves. His growing interest in books brings him into conflict with his employer and forces him to consider the value of knowledge, independent thought, and human connection."
                },
                new()
                {
                    Title = "The Great Gatsby",
                    PublishedDate = new DateTime(1925, 4, 10),
                    ImageUrl = "the_great_gatsby.jpg",
                    Synopsis = "Nick Carraway moves to Long Island and becomes acquainted with his mysterious and wealthy neighbor Jay Gatsby. Gatsby hosts extravagant parties while secretly hoping to rekindle his relationship with Daisy Buchanan, who is now married to Tom Buchanan. Set during the Jazz Age, the novel examines wealth, ambition, social class, love, memory, and the limits of the American Dream."
                },
                new()
                {
                    Title = "War and Peace",
                    PublishedDate = new DateTime(1869, 1, 1),
                    ImageUrl = "war_and_peace.jpg",
                    Synopsis = "Set during the Napoleonic Wars, War and Peace follows several Russian aristocratic families as their lives are transformed by war, political upheaval, marriage, and personal loss. Characters including Pierre Bezukhov, Prince Andrei Bolkonsky, and Natasha Rostova experience changing relationships and beliefs while confronting the consequences of historical events. The novel combines intimate personal stories with a broad examination of history and society."
                },
                new()
                {
                    Title = "Les Misérables",
                    PublishedDate = new DateTime(1862, 4, 3),
                    ImageUrl = "les-miserables.jpg",
                    Synopsis = "Jean Valjean, a former prisoner seeking to rebuild his life, becomes the center of a story involving poverty, justice, redemption, and social inequality in nineteenth-century France. Pursued by the determined Inspector Javert, Valjean attempts to protect Cosette while living under an assumed identity. Their lives intersect with political unrest and the 1832 Paris uprising, bringing together personal struggles and broader social conflicts."
                },
                new()
                {
                    Title = "Frankenstein",
                    PublishedDate = new DateTime(1818, 1, 1),
                    ImageUrl = "frankenstein.jpg",
                    Synopsis = "Victor Frankenstein, a young scientist, becomes obsessed with discovering the secret of creating life. His experiment succeeds, but he is horrified by the creature he has brought into existence and abandons it. The creature, rejected by society and searching for companionship, confronts Victor and demands recognition, setting the two on a tragic path involving responsibility, isolation, and revenge."
                },
                new()
                {
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    PublishedDate = new DateTime(1979, 10, 12),
                    ImageUrl = "the-hitchhikers-guide-to-the-galaxy.jpg",
                    Synopsis = "Arthur Dent's ordinary life is interrupted when he discovers that his friend Ford Prefect is actually an extraterrestrial researcher. Moments later, Earth is demolished to make way for a hyperspace bypass, and Arthur escapes into space with Ford. Their journey introduces them to bizarre worlds, alien civilizations, improbable technology, and the mysterious answer to the ultimate question of life, the universe, and everything."
                },
                new()
                {
                    Title = "Good Omens",
                    PublishedDate = new DateTime(1990, 5, 1),
                    ImageUrl = "good-omens.jpg",
                    Synopsis = "An angel named Aziraphale and a demon named Crowley have spent centuries living among humans and have developed an unlikely friendship. When the Antichrist is born and the forces of Heaven and Hell prepare for the end of the world, the two supernatural beings discover that the wrong child has been raised as the Antichrist. They attempt to prevent the apocalypse while dealing with prophecies, witches, demons, angels, and ordinary human misunderstandings."
                },
                new()
                {
                    Title = "American Gods",
                    PublishedDate = new DateTime(2001, 6, 18),
                    ImageUrl = "american_gods.jpg",
                    Synopsis = "Shadow Moon is released from prison shortly before his wife dies and accepts a mysterious job from a man named Mr. Wednesday. As Shadow travels across the United States with his employer, he discovers that ancient gods brought to America by immigrants are preparing for a conflict with newer deities representing modern technology and culture. The journey combines mythology, mystery, fantasy, and an exploration of belief."
                },
                new()
                {
                    Title = "The Odyssey",
                    PublishedDate = new DateTime(1614, 1, 1),
                    ImageUrl = "the-odyssey.jpg",
                    Synopsis = "The Odyssey follows Odysseus as he attempts to return home to Ithaca after the Trojan War. His journey is delayed by encounters with gods, monsters, storms, and strange lands, while his wife Penelope and son Telemachus face pressures from suitors at home. The epic explores perseverance, loyalty, hospitality, identity, and the difficulties of returning home after a long absence."
                },
                new()
                {
                    Title = "The Divine Comedy",
                    PublishedDate = new DateTime(1320, 1, 1),
                    ImageUrl = "the_divine_comedy.jpg",
                    Synopsis = "Dante undertakes an allegorical journey through Hell, Purgatory, and Paradise. Guided first by the Roman poet Virgil and later by Beatrice, he encounters historical, mythological, and theological figures while reflecting on sin, justice, redemption, faith, and divine order. The poem presents a vast medieval vision of the afterlife while exploring the individual's spiritual and moral transformation."
                },
                new()
                {
                    Title = "The Picture of Dorian Gray",
                    PublishedDate = new DateTime(1890, 6, 20),
                    ImageUrl = "the-picture-of-dorian-gray.jpg",
                    Synopsis = "Dorian Gray is a young man whose portrait is painted by the artist Basil Hallward. Influenced by Lord Henry Wotton, Dorian becomes fascinated with youth, beauty, and the pursuit of pleasure. When the portrait begins to reflect the consequences of his actions while his physical appearance remains unchanged, Dorian attempts to escape the moral consequences of his choices."
                },
                new()
                {
                    Title = "Dracula",
                    PublishedDate = new DateTime(1897, 5, 26),
                    ImageUrl = "dracula.jpg",
                    Synopsis = "Jonathan Harker travels to Transylvania to assist Count Dracula with a property transaction and soon discovers that his host is a dangerous supernatural being. Dracula eventually travels to England, where a group led by Professor Van Helsing attempts to stop him. Told through journals, letters, and other documents, the novel follows the group's investigation and their efforts to protect those threatened by the vampire."
                },
                new()
                {
                    Title = "The Count of Monte Cristo",
                    PublishedDate = new DateTime(1844, 8, 28),
                    ImageUrl = "the-count-of-monte_cristo.jpg",
                    Synopsis = "Edmond Dantès is falsely imprisoned shortly before his wedding because of a political conspiracy involving people who envy him. After years in prison, he escapes with the help of a fellow prisoner and discovers a hidden fortune. Taking a new identity and extraordinary wealth, Edmond sets out to reward those who helped him and confront those responsible for his imprisonment."
                },
                new()
                {
                    Title = "The Alchemist",
                    PublishedDate = new DateTime(1988, 1, 1),
                    ImageUrl = "the-alchemist.jpg",
                    Synopsis = "Santiago, a young shepherd from Andalusia, repeatedly dreams about a treasure near the Egyptian pyramids. Encouraged to pursue the dream, he travels across North Africa and meets people who teach him about perseverance, opportunity, love, and personal purpose. His journey becomes a philosophical adventure about following one's goals and recognizing meaning in the experiences along the way."
                },
                new()
                {
                    Title = "The Name of the Wind",
                    PublishedDate = new DateTime(2007, 3, 27),
                    ImageUrl = "the-name-of-the-wind.jpg",
                    Synopsis = "Kvothe, a legendary figure living under an assumed identity, begins telling the story of his life to a chronicler. He describes his childhood with a traveling troupe, his years of hardship, his education at a prestigious university, and his search for knowledge about the mysterious Chandrian. The narrative combines fantasy, music, magic, adventure, and the construction of a personal legend."
                },
                new()
                {
                    Title = "The Little Prince",
                    PublishedDate = new DateTime(1943, 4, 6),
                    ImageUrl = "the-little-prince.jpg",
                    Synopsis = "A pilot stranded in the Sahara meets a mysterious young visitor who claims to come from a tiny asteroid. The Little Prince describes his travels among several small worlds and the unusual adults he encountered there before arriving on Earth. Through his conversations and experiences, the story explores friendship, love, responsibility, imagination, and the ways adults perceive the world."
                },
                new()
                {
                    Title = "The Road",
                    PublishedDate = new DateTime(2006, 9, 26),
                    ImageUrl = "the-road.jpg",
                    Synopsis = "In a devastated future landscape, a father and his young son travel south in search of safety. Carrying limited supplies and facing an uncertain world, they attempt to preserve their sense of morality and compassion. Their journey examines survival, love, memory, hope, and the responsibility of protecting another person in the aftermath of catastrophe."
                },
                new()
                {
                    Title = "The Martian",
                    PublishedDate = new DateTime(2011, 2, 11),
                    ImageUrl = "the-martian.jpg",
                    Synopsis = "Astronaut Mark Watney is accidentally left behind on Mars after his crew is forced to evacuate during a dangerous mission. With limited supplies and no immediate way to communicate with Earth, he uses engineering, scientific knowledge, and careful planning to survive. Meanwhile, NASA and his crewmates work to develop a way to bring him home."
                }
            };

            // Reset generated IDs so EF Core can assign them.
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

            // Explicit author relationships.
            // This is preferable to relying on list positions because
            // some books have authors added later and some have multiple authors.
            var authorBooks = new[]
            {
                // Original books
                (1, "George Orwell", 1),
                (2, "George Orwell", 1),
                (3, "J.K. Rowling", 1),
                (4, "J.K. Rowling", 1),
                (5, "Stephen King", 1),
                (6, "Stephen King", 1),
                (7, "Isaac Asimov", 1),
                (8, "Isaac Asimov", 1),
                (9, "Jane Austen", 1),
                (10, "Jane Austen", 1),
                (11, "Gabriel García Márquez", 1),
                (12, "Gabriel García Márquez", 1),
                (13, "Agatha Christie", 1),
                (14, "Agatha Christie", 1),
                (15, "J.R.R. Tolkien", 1),
                (16, "J.R.R. Tolkien", 1),
                (17, "Yuval Noah Harari", 1),
                (18, "Yuval Noah Harari", 1),
                (19, "Brandon Sanderson", 1),
                (20, "Brandon Sanderson", 1),

                // New books
                (21, "Frank Herbert", 1),
                (22, "Aldous Huxley", 1),
                (23, "Ray Bradbury", 1),
                (24, "F. Scott Fitzgerald", 1),
                (25, "Leo Tolstoy", 1),
                (26, "Victor Hugo", 1),
                (27, "Mary Shelley", 1),
                (28, "Douglas Adams", 1),
                (29, "Neil Gaiman", 1),
                (29, "Terry Pratchett", 2),
                (30, "Neil Gaiman", 1),
                (31, "Homer", 1),
                (32, "Dante Alighieri", 1),
                (33, "Oscar Wilde", 1),
                (34, "Bram Stoker", 1),
                (35, "Alexandre Dumas", 1),
                (36, "Paulo Coelho", 1),
                (37, "Patrick Rothfuss", 1),
                (38, "Antoine de Saint-Exupéry", 1),
                (39, "Cormac McCarthy", 1),
                (40, "Andy Weir", 1)
            };

            // Add missing authors referenced by the new books.
            var additionalAuthors = new[]
            {
                new Author { Name = "Oscar Wilde", ImageUrl = "oscar_wilde.jpg" },
                new Author { Name = "Bram Stoker", ImageUrl = "bram_stoker.jpg" },
                new Author { Name = "Alexandre Dumas", ImageUrl = "alexandre_dumas.jpg" },
                new Author { Name = "Paulo Coelho", ImageUrl = "paulo_coelho.jpg" },
                new Author { Name = "Patrick Rothfuss", ImageUrl = "patrick_rothfuss.jpg" },
                new Author { Name = "Antoine de Saint-Exupéry", ImageUrl = "antoine_de_saint_exupery.jpg" },
                new Author { Name = "Cormac McCarthy", ImageUrl = "cormac_mccarthy.jpg" },
                new Author { Name = "Andy Weir", ImageUrl = "andy_weir.jpg" }
            };

            context.Authors.AddRange(additionalAuthors);
            await context.SaveChangesAsync();

            var authorLookup = authors
                .Concat(additionalAuthors)
                .ToDictionary(author => author.Name, author => author.Id);

            context.AuthorBooks.AddRange(
                authorBooks.Select(item => new AuthorBook
                {
                    BookId = books[item.Item1 - 1].Id,
                    AuthorId = authorLookup[item.Item2],
                    Order = item.Item3
                })
            );

            // Genre IDs:
            // 1 = Fantasy
            // 2 = Science Fiction
            // 3 = Mystery
            // 4 = Horror
            // 5 = Romance
            // 6 = History
            // 7 = Drama
            // 8 = Adventure

            var bookGenres = new[]
            {
                // Original books
                (1, 2),
                (2, 7),
                (3, 1),
                (4, 1),
                (5, 4),
                (6, 4),
                (7, 2),
                (8, 2),
                (9, 5),
                (10, 5),
                (11, 7),
                (12, 5),
                (13, 3),
                (14, 3),
                (15, 8),
                (16, 1),
                (17, 6),
                (18, 6),
                (19, 1),
                (20, 1),

                // New books
                (21, 2), (21, 8),
                (22, 2), (22, 7),
                (23, 2), (23, 7),
                (24, 7), (24, 5),
                (25, 6), (25, 7), (25, 5),
                (26, 6), (26, 7), (26, 8),
                (27, 4), (27, 7), (27, 1),
                (28, 2), (28, 8),
                (29, 1), (29, 2), (29, 7),
                (30, 1), (30, 8),
                (31, 8), (31, 1),
                (32, 6), (32, 7),
                (33, 7), (33, 5),
                (34, 4), (34, 7),
                (35, 8), (35, 5), (35, 7),
                (36, 7), (36, 5),
                (37, 1), (37, 8),
                (38, 1), (38, 7),
                (39, 7), (39, 8),
                (40, 2), (40, 8)
            };

            context.BookGenres.AddRange(
                bookGenres.Select(pair => new BookGenre
                {
                    BookId = books[pair.Item1 - 1].Id,
                    GenreId = genres[pair.Item2 - 1].Id
                })
            );

            var comments = new[]
            {
                ("Excellent dystopian novel.", 1, 101),
                ("A timeless classic.", 1, 102),
                ("Very entertaining.", 3, 103),
                ("Perfect for young readers.", 3, 104),
                ("One of my favorite horror books.", 5, 105),
                ("A little too long but worth it.", 6, 106),
                ("Great introduction to science fiction.", 7, 107),
                ("Interesting robot stories.", 8, 108),
                ("Beautiful love story.", 9, 109),
                ("Fantastic character development.", 10, 110),
                ("Masterpiece of magical realism.", 11, 111),
                ("Very emotional.", 12, 112),
                ("Could not guess the ending.", 13, 113),
                ("Classic detective novel.", 14, 114),
                ("A wonderful adventure.", 15, 115),
                ("Epic fantasy.", 16, 116),
                ("Changed my perspective on history.", 17, 117),
                ("Thought-provoking.", 18, 118),
                ("Amazing magic system.", 19, 119),
                ("Outstanding world building.", 20, 120),
                ("Highly recommended.", 20, 121),
                ("Would read again.", 15, 122),
                ("Loved every chapter.", 11, 123),
                ("Not my favorite but still good.", 8, 124),
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

        private static async Task<Dictionary<int, string>> SeedUsersAsync(
            UserManager<IdentityUser> userManager)
        {
            var users = new Dictionary<int, string>();

            for (var userNumber = 101; userNumber <= 125; userNumber++)
            {
                var email = $"seed-user-{userNumber}@bookshelf.local";
                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new IdentityUser
                    {
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(
                        user,
                        "Bookshelf123!"
                    );

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(
                            string.Join(
                                "; ",
                                result.Errors.Select(error => error.Description)
                            )
                        );
                    }
                }

                users[userNumber] = user.Id;
            }

            return users;
        }
    }
}