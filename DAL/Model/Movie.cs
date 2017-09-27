using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Movie
    {
        public Movie()
        {
            AppearsIn = new HashSet<AppearsIn>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte? Rating { get; set; }

        public ICollection<AppearsIn> AppearsIn { get; set; }
    }
}
