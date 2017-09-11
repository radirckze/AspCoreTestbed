using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Character
    {
        public Character()
        {
            Quote = new HashSet<Quote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Quote> Quote { get; set; }
    }
}
