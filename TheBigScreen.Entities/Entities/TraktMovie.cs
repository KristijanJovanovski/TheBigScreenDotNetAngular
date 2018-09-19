using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBigScreen.Entities.Entities
{
    public class TraktMovie
    {
        [Required]
        public string Slug { get; set; }
        
        public int? TmdbId { get; set; }
        
        public string ImdbId { get; set; }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TraktId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public int? Year { get; set; }

        public string Tagline { get; set; }

        public string Overview { get; set; }

        public DateTime? Released { get; set; }

        public int? Runtime { get; set; }

        public string Trailer { get; set; }

        public string Homepage { get; set; }

        public float? Rating { get; set; }

        public int? Votes { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string LanguageCode { get; set; }

        public string Certification { get; set; }

        public virtual IEnumerable<BookmarkedMovie> BookmarkedUsers { get; set; }

        public virtual IEnumerable<RatedMovie> RatedUsers { get; set; }

        public virtual IEnumerable<WatchedMovie> WatchedUsers { get; set; }

    }
}