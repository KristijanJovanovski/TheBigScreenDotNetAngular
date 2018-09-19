using System;
using System.ComponentModel.DataAnnotations;

namespace TheBigScreen.Entities.Entities
{
    public class RatedMovie: BaseEntity
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public virtual User User { get; set; }

        public virtual TraktMovie Movie { get; set; }

        public DateTime DateRated { get; set; }

        [Range(0, 10)]
        public int Rate { get; set; }
    }
}
