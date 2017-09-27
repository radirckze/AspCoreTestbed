using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class AppearsIn
    {
        public AppearsIn()
        {
            Quote = new HashSet<Quote>();
        }

        public int MovieId { get; set; }
        public int CharacterId { get; set; }

        public Mcharacter Character { get; set; }
        public Movie Movie { get; set; }
        public ICollection<Quote> Quote { get; set; }
    }
}
