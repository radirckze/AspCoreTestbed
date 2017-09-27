using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Quote
    {
        public int Id { get; set; }
        public string Quote1 { get; set; }
        public int CharacterId { get; set; }
        public int MovieId { get; set; }

        public AppearsIn AppearsIn { get; set; }
    }
}
