using System;
using System.Collections.Generic;
using TheBigScreen.Entities.Entities;

namespace TheBigScreen.Services.ViewModels
{
    public class TraktMovieViewModel
    {
        public string Slug { get; set; }

        public int? TmdbId { get; set; }

        public string ImdbId { get; set; }
        
        public int TraktId { get; set; }
        
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

        public string PosterPath { get; set; }
        //        ______________________________________________ AuthedUser
        public DateTime? DateBookmarked { get; set; }

        public bool? Bookmarked { get; set; }

        public DateTime? DateRated { get; set; }

        public int? Rate { get; set; }

        public bool? Rated { get; set; }

        public DateTime? DateWatched { get; set; }

        public bool? Watched { get; set; }

        //        ________________________________________________ Specific

        public int? ListCountAnticipated { get; set; }

        public int? RevenueBoxOffice { get; set; }

        public int? WatchersTrending { get; set; }

        public int? WatcherCountPWC { get; set; }

        public int? PlayCountPWC { get; set; }

    }
}