using DotNetShop.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Data.Seeds
{
    public class DatabaseInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories.Select(c => c.Value));
                }

                //context.Drinks.RemoveRange(context.Drinks);
                if (!context.Movies.Any())
                {
                    var movies = new Movie[]
                    {
                         new Movie
                         {
                             Name = "Inception",
                             Category = categories["Action"],
                             ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/280892/small_654b1c569a9a2b850e8d9c4de3bd8f02-Inception-movie-poster-7.jpg",
                             InStock = 20,
                             IsPreferedMovie = false,
                             ShortDescription = "Cobb, a skilled thief...",
                             LongDescription =
                                    "Cobb, a skilled thief who commits corporate espionage by infiltrating the subconscious of his targets is offered a chance to regain his old life as payment for a task considered to be impossible: \"inception\", the implantation of another person's idea into a target's subconscious.",
                             Price = 4.5M
                         },
                        new Movie
                        {
                            Name = "The Lord of the Rings: The Return of the King",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/2470/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Aragorn is revealed as...",
                            LongDescription =
                                    "Aragorn is revealed as the heir to the ancient kings as he, Gandalf and the other members of the broken fellowship struggle to save Gondor from Sauron's forces. Meanwhile, Frodo and Sam bring the ring closer to the heart of Mordor, the dark lord's realm.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Revenant",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/235283/small_f734b9dbce3ae793c26db26bb700426e-revenant.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In the 1820s, a frontiersman...",
                            LongDescription =
                                    "In the 1820s, a frontiersman, Hugh Glass, sets out on a path of vengeance against those who left him for dead after a bear mauling.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Empire Strikes Back",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/278190/small_e6d276db404afa1e361e2b39dded4099-4217887-origpic-5e6f9b.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "The epic saga continues...",
                            LongDescription =
                                    "The epic saga continues as Luke Skywalker, in hopes of defeating the evil Galactic Empire, learns the ways of the Jedi from aging master Yoda. But Darth Vader is more determined than ever to capture Luke. Meanwhile, rebel leader Princess Leia, cocky Han Solo, Chewbacca, and droids C-3PO and R2-D2 are thrown into various stages of capture, betrayal and despair.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Tropic Thunder",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/369/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Vietnam veteran...",
                            LongDescription =
                                    "Vietnam veteran 'Four Leaf' Tayback's memoir, Tropic Thunder, is being made into a film, but Director Damien Cockburn can’t control the cast of prima donnas. Behind schedule and over budget, Cockburn is ordered by a studio executive to get filming back on track, or risk its cancellation. On Tayback's advice, Cockburn drops the actors into the middle of the jungle to film the remaining scenes but, unbeknownst to the actors and production, the group have been dropped in the middle of the Golden Triangle, the home of heroin-producing gangs.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Guardians of the Galaxy",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/98496/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Light years from Earth...",
                            LongDescription =
                                    "Light years from Earth, 26 years after being abducted, Peter Quill finds himself the prime target of a manhunt after discovering an orb wanted by Ronan the Accuser.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Logan",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/305029/small_51aba6497799f5e28a6f17ca9a19eea6-588f44ec67854_thumbnail.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In the near future...",
                            LongDescription =
                                    "In the near future, a weary Logan cares for an ailing Professor X in a hideout on the Mexican border. But Logan's attempts to hide from the world and his legacy are upended when a young mutant arrives, pursued by dark forces.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Dark Knight Rises",
                            Category = categories["Action"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/78055/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Following the death...",
                            LongDescription =
                                     "Following the death of District Attorney Harvey Dent, Batman assumes responsibility for Dent's crimes to protect the late attorney's reputation and is subsequently hunted by the Gotham City Police Department. Eight years later, Batman encounters the mysterious Selina Kyle and the villainous Bane, a new terrorist leader who overwhelms Gotham's finest. The Dark Knight resurfaces to protect a city that has branded him an enemy.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Django Unchained",
                            Category = categories["Adventure"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/83583/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "With the help of a...",
                            LongDescription =
                                    "With the help of a German bounty hunter, a freed slave sets out to rescue his wife from a brutal Mississippi plantation owner.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Jurassic Park",
                            Category = categories["Adventure"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/268763/small_91d4e2a9212ac341b5180cc88d67fe6b-jurassic.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A wealthy entrepreneur...",
                            LongDescription =
                                     "A wealthy entrepreneur secretly creates a theme park featuring living dinosaurs drawn from prehistoric DNA. Before opening day, he invites a team of experts and his two eager grandchildren to experience the park and help calm anxious investors. However, the park is anything but amusing as the security systems go off-line and the dinosaurs escape.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "WALL·E",
                            Category = categories["Animation"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/379/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "WALL·E is the last...",
                            LongDescription =
                                    "WALL·E is the last robot left on an Earth that has been overrun with garbage and all humans have fled to outer space. For 700 years he has continued to try and clean up the mess, but has developed some rather interesting human-like qualities. When a ship arrives with a sleek new type of robot, WALL·E thinks he's finally found a friend and stows away on the ship when it leaves.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Lion King",
                            Category = categories["Animation"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/69766/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A young lion cub named...",
                            LongDescription =
                                    "A young lion cub named Simba can't wait to be king. But his uncle craves the title for himself and will stop at nothing to get it",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Bad Education",
                            Category = categories["Biography"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/370213/small_843392edfa81ec3000da934de73179ff-baded.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "The beloved superintendent...",
                            LongDescription =
                                    "The beloved superintendent of New York's Roslyn school district and his staff, friends and relatives become the prime suspects in the unfolding of the single largest public school embezzlement scandal in American history.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Toy Story 3",
                            Category = categories["Comedy"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/173745/small_16b725f67686ddd4cc7c674f47a4fb28-Toy_20Story_203.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Woody, Buzz, and...",
                            LongDescription =
                                     "Woody, Buzz, and the rest of Andy's toys haven't been played with in years. With Andy about to go to college, the gang find themselves accidentally left at a nefarious day care center. The toys must band together to escape and return home to Andy.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Grand Budapest Hotel",
                            Category = categories["Comedy"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/92082/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "The Grand Budapest Hotel...",
                            LongDescription =
                                    "The Grand Budapest Hotel tells of a legendary concierge at a famous European hotel between the wars and his friendship with a young employee who becomes his trusted protégé. The story involves the theft and recovery of a priceless Renaissance painting, the battle for an enormous family fortune and the slow and then sudden upheavals that transformed Europe during the first half of the 20th century.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Pulp Fiction",
                            Category = categories["Crime"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/2212/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A burger-loving hit...",
                            LongDescription =
                                    "A burger-loving hit man, his philosophical partner, a drug-addled gangster's moll and a washed-up boxer converge in this sprawling, comedic crime caper. Their adventures unfurl in three stories that ingeniously trip back and forth in time.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Godfather: Part II",
                            Category = categories["Crime"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/237496/small_2aac97615c14269b2c5cc4e66ebd9401-60240-godfather2.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In the continuing saga...",
                            LongDescription =
                                     "In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Hacksaw Ridge",
                            Category = categories["Documentary"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/285157/small_563c20f09b1808ee28c94a46e97663d8-unnamed.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "WWII American Army Medic...",
                            LongDescription =
                                    "WWII American Army Medic Desmond T. Doss, who served during the Battle of Okinawa, refuses to kill people and becomes the first Conscientious Objector in American history to receive the Congressional Medal of Honor.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Capone",
                            Category = categories["Documentary"],
                            ImageUrl ="https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/371304/small_9b9dcd8be2dc29c4eb22368cf7878c58-capone.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "The 47-year old...",
                            LongDescription =
                                    "The 47-year old Al Capone, after 10 years in prison, starts suffering from dementia and comes to be haunted by his violent past.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Shape of Water",
                            Category = categories["Drama"],
                            ImageUrl ="https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/328162/small_94e93f2359d8d66d7d1d40e5ed8d3e1b-shape.jpeg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "An other-worldly story...",
                            LongDescription =
                                     "An other-worldly story, set against the backdrop of Cold War era America circa 1962, where a mute janitor working at a lab falls in love with an amphibious man being held captive there and devises a plan to help him escape.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Straight Outta Compton",
                            Category = categories["Drama"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/196662/small_21ea47a82abc354b50845c1a947a10fd-l_1398426_c0eed2e4.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In 1987, five young men...",
                            LongDescription =
                                     "In 1987, five young men, using brutally honest rhymes and hardcore beats, put their frustration and anger about life in the most dangerous place in America into the most powerful weapon they had: their music.  Taking us back to where it all began, Straight Outta Compton tells the true story of how these cultural rebels—armed only with their lyrics, swagger, bravado and raw talent—stood up to the authorities that meant to keep them down and formed the world’s most dangerous group, N.W.A.  And as they spoke the truth that no one had before and exposed life in the hood, their voice ignited a social revolution that is still reverberating today.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Harry Potter and the Deathly Hallows: Part 2",
                            Category = categories["Family"],
                            ImageUrl ="https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/68344/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Harry, Ron and Hermione...",
                            LongDescription =
                                     "Harry, Ron and Hermione continue their quest to vanquish the evil Voldemort once and for all. Just as things begin to look hopeless for the young wizards, Harry discovers a trio of magical objects that endow him with powers to rival Voldemort's formidable skills.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Back to the Future",
                            Category = categories["Family"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/208173/small_23c05ce08bd50b09b8cf9f09b758aefa-5470.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Eighties teenager Marty...",
                            LongDescription =
                                    "Eighties teenager Marty McFly is accidentally sent back in time to 1955, inadvertently disrupting his parents' first meeting and attracting his mother's romantic interest. Marty must repair the damage to history by rekindling his parents' romance and - with the help of his eccentric inventor friend Doc Brown - return to 1985.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Get Out",
                            Category = categories["Fantasy"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/303372/small_e52189ac788507429c602afd40e846c8-get_out_ver3.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Chris and his girlfriend...",
                            LongDescription =
                                    "Chris and his girlfriend Rose go upstate to visit her parents for the weekend. At first, Chris reads the family's overly accommodating behavior as nervous attempts to deal with their daughter's interracial relationship, but as the weekend progresses, a series of increasingly disturbing discoveries lead him to a truth that he never could have imagined.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Pan's Labyrinth",
                            Category = categories["Fantasy"],
                            ImageUrl ="https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/134869/small_original.jpeg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Living with her tyrannical...",
                            LongDescription =
                                    "Living with her tyrannical stepfather in a new home with her pregnant mother, 10-year-old Ofelia feels alone until she explores a decaying labyrinth guarded by a mysterious faun who claims to know her destiny. If she wishes to return to her real father, Ofelia must complete three terrifying tasks.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Sunset Blvd",
                            Category = categories["Film-Noir"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/229824/small_c2049f3a1558b9c6aef6ad76a3adc51b-l_43014_304112e5.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A screenwriter develops...",
                            LongDescription =
                                    "A screenwriter develops a dangerous relationship with a faded film star determined to make a triumphant return.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "12 Years a Slave",
                            Category = categories["History"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/89445/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In the antebellum United States...",
                            LongDescription =
                                    "In the antebellum United States, Solomon Northup, a free black man from upstate New York, is abducted and sold into slavery.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Saving Private Ryan",
                            Category = categories["History"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/3661/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "As U.S. troops storm...",
                            LongDescription =
                                    "As U.S. troops storm the beaches of Normandy, three brothers lie dead on the battlefield, with a fourth trapped behind enemy lines. Ranger captain John Miller and seven men are tasked with penetrating German-held territory and bringing the boy home.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Ritual",
                            Category = categories["Horror"],
                            ImageUrl ="https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/322092/small_f4d600894ea15238582a2cfffb2ee1c1-ritual.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A group of college friends...",
                            LongDescription =
                                    "A group of college friends reunite for a trip to the forest, but encounter a menacing presence in the woods that's stalking them.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Black Mirror",
                            Category = categories["Mini-Series"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/326814/small_0a73069ea63a2db7679b51c104b4f826-black.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "An anthology series exploring...",
                            LongDescription =
                                    "An anthology series exploring a twisted, high-tech multiverse where humanity's greatest innovations and darkest instincts collide.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "High School Musical",
                            Category = categories["Music"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/481/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A popular high school...",
                            LongDescription =
                                    "A popular high school athlete and an academically gifted girl get roles in the school musical and develop a friendship that threatens East High's social order.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Eddy",
                            Category = categories["Musical"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/369703/small_df4a2a86d12eb8405d32a940de63061a-eddy-poster.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A French club owner...",
                            LongDescription =
                                    "A French club owner deals with the everyday chaos of running a live music venue in the heart of Paris.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Money Heist",
                            Category = categories["Mystery"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/367860/small_910091d70678ddc0dc3c124cfa33dc8a-FB_IMG_1584365528601.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "An unusual group of robbers...",
                            LongDescription =
                                    "An unusual group of robbers attempt to carry out the most perfect robbery in Spanish history - stealing 2.4 billion euros from the Royal Mint of Spain.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Little Women",
                            Category = categories["Romance"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/3995/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "Jo March reflects back...",
                            LongDescription =
                                    "Jo March reflects back and forth on her life, telling the beloved story of the March sisters - four young women, each determined to live life on her own terms.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Alien",
                            Category = categories["Science-Fiction"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/268765/small_9d83b2f7cdad5ba64c954ae6058649ea-alien.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "After a space merchant vessel...",
                            LongDescription =
                                    "After a space merchant vessel receives an unknown transmission as a distress call, one of the crew is attacked by a mysterious life form and they soon realize that its life cycle has merely begun.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Kung Fury",
                            Category = categories["Short"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/194464/small_7cc7ac3f7eb19dc0a24986e9eb9cd4da-l_3472226_f998578d.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "In 1985, Kung Fury...",
                            LongDescription =
                                    "In 1985, Kung Fury, the toughest martial artist cop in Miami, goes back in time to kill the worst criminal of all time - Kung Führer, a.k.a. Adolf Hitler.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Million Dollar Baby",
                            Category = categories["Sport"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/221587/small_85881822b0304ed534a993f300f8f2dd-l_121377_0405159_d2af6cd4.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "A determined woman...",
                            LongDescription =
                                    "A determined woman works with a hardened boxing trainer to become a professional.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "Fight Club",
                            Category = categories["Suspense"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/3535/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "An insomniac office worker...",
                            LongDescription =
                                    "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "1917",
                            Category = categories["War"],
                            ImageUrl = "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/363280/small_155b64a39a770f94ec004b467c5e17d8-1917.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "April 6th, 1917...",
                            LongDescription =
                                    "April 6th, 1917. As a regiment assembles to wage war deep in enemy territory, two soldiers are assigned to race against time and deliver a message that will stop 1,600 men from walking straight into a deadly trap.",
                            Price = 4.4M
                        },
                        new Movie
                        {
                            Name = "The Good, the Bad and the Ugly",
                            Category = categories["Western"],
                            ImageUrl =  "https://d2iltjk184xms5.cloudfront.net/uploads/photo/file/60706/small_original.jpg",
                            InStock = 20,
                            IsPreferedMovie = true,
                            ShortDescription = "While the Civil War rages...",
                            LongDescription =
                                    "While the Civil War rages between the Union and the Confederacy, three men – a quiet loner, a ruthless hit man and a Mexican bandit – comb the American Southwest in search of a strongbox containing $200,000 in stolen gold.",
                            Price = 4.4M
                        }

                    };


                    

                    context.AddRange(movies);
                }

                context.SaveChanges();
            }
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category
                        {
                            Name = "Action",
                            Description = "Action movies",
                            ImageUrl = "https://i.ibb.co/3dmnnkG/Action.jpg",
                        },
                    new Category
                    {
                        Name = "Adventure",
                        Description = "All Adventure",
                        ImageUrl = "https://i.ibb.co/xm0tQ8q/Adventure.jpg"
                    },
                    new Category
                    {
                        Name = "Animation",
                        Description = "All Animation",
                        ImageUrl = "https://i.ibb.co/dKKF0TP/Animation.jpg"
                    },
                    new Category
                    {
                        Name = "Biography",
                        Description = "All Biography",
                        ImageUrl = "https://i.ibb.co/RzRPxRw/Biography.jpg"
                    },
                    new Category
                    {
                        Name = "Comedy",
                        Description = "All Comedy",
                        ImageUrl = "https://i.ibb.co/K7LcRNm/Comedy.jpg"
                    },
                    new Category
                    {
                        Name = "Crime",
                        Description = "All Crime",
                        ImageUrl = "https://i.ibb.co/6N3Z928/Crime.jpg"
                    },
                    new Category
                    {
                        Name = "Documentary",
                        Description = "All Documentary",
                        ImageUrl = "https://i.ibb.co/WzMygTH/Documentary.jpg"
                    },
                    new Category
                    {
                        Name = "Drama",
                        Description = "All Drama",
                        ImageUrl = "https://i.ibb.co/Gd2MTfC/Drama.jpg"
                    },
                    new Category
                    {
                        Name = "Family",
                        Description = "All Family",
                        ImageUrl = "https://i.ibb.co/F7mwjGz/Family.jpg"
                    },
                    new Category
                    {
                        Name = "Fantasy",
                        Description = "All Fantasy",
                        ImageUrl = "https://i.ibb.co/mXmtRgB/Fantasy.jpg"
                    },
                    new Category
                    {
                        Name = "Film-Noir",
                        Description = "All Film-Noir",
                        ImageUrl = "https://i.ibb.co/RPzWh4Y/Film-Noir.jpg"
                    },
                    new Category
                    {
                        Name = "History",
                        Description = "All History",
                        ImageUrl = "https://i.ibb.co/nCKJvxL/History.jpg"
                    },
                    new Category
                    {
                        Name = "Horror",
                        Description = "All Horror",
                        ImageUrl = "https://i.ibb.co/nc1rcbg/Horror.jpg"
                    },
                    new Category
                    {
                        Name = "Mini-Series",
                        Description = "All Mini-Series",
                        ImageUrl = "https://i.ibb.co/tKXCxzV/Mini-Series.jpg"
                    },
                    new Category
                    {
                        Name = "Music",
                        Description = "All Music",
                        ImageUrl = "https://i.ibb.co/3rrrs00/Music.jpg"
                    },
                    new Category
                    {
                        Name = "Musical",
                        Description = "All Musical",
                        ImageUrl = "https://i.ibb.co/tB7spZ4/Musical.jpg"
                    },
                    new Category
                    {
                        Name = "Mystery",
                        Description = "All Mystery",
                        ImageUrl = "https://i.ibb.co/4PnJqqQ/Mystery.jpg"
                    },
                    new Category
                    {
                        Name = "Romance",
                        Description = "All Romance",
                        ImageUrl = "https://i.ibb.co/dLhwCWh/Romance.jpg"
                    },
                    new Category
                    {
                        Name = "Science-Fiction",
                        Description = "All Science-Fiction",
                        ImageUrl = "https://i.ibb.co/rst16Js/Science-Fiction.jpg"
                    },
                    new Category
                    {
                        Name = "Short",
                        Description = "All Short",
                        ImageUrl = "https://i.ibb.co/PmMKRxH/Short.jpg"
                    },
                    new Category
                    {
                        Name = "Sport",
                        Description = "All Sport",
                        ImageUrl = "https://i.ibb.co/KVwyJ4j/Sport.jpg"
                    },
                    new Category
                    {
                        Name = "Suspense",
                        Description = "All Suspense",
                        ImageUrl = "https://i.ibb.co/x5rJ1nL/Suspense.jpg"
                    },
                    new Category
                    {
                        Name = "War",
                        Description = "All War",
                        ImageUrl = "https://i.ibb.co/bKVsPzV/War.jpg"
                    },
                    new Category
                    {
                        Name = "Western",
                        Description = "All Western",
                        ImageUrl = "https://i.ibb.co/mq5xjXy/Western.jpg"
                    }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        //genre.ImageUrl = $"/images/Categories/{genre.Name}.png";
                        categories.Add(genre.Name, genre);
                    }
                }

                return categories;
            }
        }
    }
}
