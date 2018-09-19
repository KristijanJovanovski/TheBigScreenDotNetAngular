using System;
using System.Collections.Generic;
using System.Text;

namespace TheBigScreen.Entities.Entities
{
    public class WatchedMovie: BaseEntity
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public virtual User User { get; set; }

        public virtual TraktMovie Movie { get; set; }

        public DateTime DateWatched { get; set; }
    }
}
