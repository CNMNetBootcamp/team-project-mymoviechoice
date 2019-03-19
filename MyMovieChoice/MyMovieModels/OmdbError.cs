using System;
using System.Collections.Generic;
using System.Text;

namespace myChoiceModels
{
    class Error
    {
        public string code { get; set; }
        public string description { get; set; }
    }
    public class OmdbError
    {
        public OmdbError error { get; set; }
    }
}