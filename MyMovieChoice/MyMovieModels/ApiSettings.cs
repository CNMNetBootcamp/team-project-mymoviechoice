﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovieChoice.Models
{
    public class ApiSettings
    {
        public string MovieBaseUrl { get; set; }

        public class OMDBAPISettings
        {
            public string OmdbBaseURL { get; set; }
            public string OmdbAPIKey { get; set; }
            public string SearchBaseURL { get; set; }
        }
    }
}
