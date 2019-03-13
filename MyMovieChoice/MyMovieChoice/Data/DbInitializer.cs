using MyMovieChoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieChoice.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MovieListContext context)
        {
            context.Database.EnsureCreated();

            //Look for any movies.
            if (context.MovieLists.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                AddMovieList(context);
            }
        }

        private static void AddMovieList(MovieListContext context)
        {
            var movielists = new MovieList[]
                {
                        new MovieList {
                            UserID = Guid.Parse("5b5d772b-1f2f-48cf-9303-59405736c438"),
                            Title ="Star Wars",
                            ReleaseDate = "1979",
                            Picture ="https://starwarsblog.starwars.com/wp-content/uploads/2015/10/tfa_poster_wide_header-1536x864-959818851016.jpg",
                            Rating =5
                        },
                        new MovieList {
                            UserID = Guid.Parse("5b5d772b-1f2f-48cf-9303-59405736c438"),
                            Title ="Star Wars: Rouge One",
                            ReleaseDate = "2018",
                            Picture ="https://upload.wikimedia.org/wikipedia/en/d/d4/Rogue_One%2C_A_Star_Wars_Story_poster.png",
                            Rating =5
                        },
                        new MovieList {
                            UserID = Guid.Parse("5b5d772b-1f2f-48cf-9303-59405736c438"),
                            Title ="Another Star Trek Movie",
                            ReleaseDate = "2017",
                            Picture ="https://upload.wikimedia.org/wikipedia/en/b/ba/Star_Trek_Beyond_poster.jpg",
                            Rating =4
                        },

                 };

            foreach (MovieList e in movielists)
            {

                {
                    context.MovieLists.Add(e);
                }
            }
            context.SaveChanges();
        }

    }
}

