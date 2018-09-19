using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TheBigScreen.Entities.Entities
{
    public class User: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MinLength(8)]
        [MaxLength(25)]
        public string Username { get; set; }

        [MaxLength(150)]
        public string Avatar { get; set; }
        

        public string Gender { get; set; }
        public bool Active { get; set; }

        public virtual IEnumerable<BookmarkedMovie> BookmarkedMovies { get; set; }
        public virtual IEnumerable<RatedMovie> RatedMovies { get; set; }
        public virtual IEnumerable<WatchedMovie> WatchedMovies { get; set; }

    }
}
