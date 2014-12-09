using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    #region DbContext
    public class MvcMovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public MvcMovieDb() : base("MovieCA2Db") { }
    }
    #endregion

    #region Movie
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(30, ErrorMessage = "At least {0} characters long for Title.", MinimumLength = 2)]
        public string MoviesTitle { get; set; }

        [Required]
        [Display(Name = "Genre"), StringLength(20)]
        public string MovieGenre { get; set; }

        [Required]
        [Display(Name = "Released"),
        DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MovieDate { get; set; }

        public ICollection<Actor> Actors { get; set; }
    }
    #endregion

    #region Actor
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        public int MovieId { get; set; }

        [Required]
        [Display(Name = "Actors Name")]
        public string ActorsName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "At least {0} characters long for Gender.", MinimumLength = 4)]
        [Display(Name = "Gender")]
        public string ActorGender { get; set; }
    }
    #endregion
}
