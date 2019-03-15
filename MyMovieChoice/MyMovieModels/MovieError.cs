using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieModels
{
    public class Error
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class MovieError
    {
        public Error error { get; set; }
    }

}
