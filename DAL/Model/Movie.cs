using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Movie
    {
        public Movie()
        {
            Quote = new HashSet<Quote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Rating { get; set; }

        public ICollection<Quote> Quote { get; set; }
    }
}
