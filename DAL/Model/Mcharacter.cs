using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Mcharacter
    {
        public Mcharacter()
        {
            AppearsIn = new HashSet<AppearsIn>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AppearsIn> AppearsIn { get; set; }
    }
}
