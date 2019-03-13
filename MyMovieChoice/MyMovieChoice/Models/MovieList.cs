using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieChoice.Models
{
    public enum Vote
    {
        Upvote, Downvote, Seen
    }
    public class MovieList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieListID { get; set; }

        public Guid? UserID { get; set; }

        public string Title { get; set; }

        [Display(Name = "Release Date")]
        public String ReleaseDate { get; set; }

        public string Picture { get; set; }

        public int Rating { get; set; }

        public Vote? Vote { get; set; }
    }
}
