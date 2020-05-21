using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Web.Models.Movie
{
    public class NewMovieModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name of the movie")]
        [Display(Name = "Movie name*")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter url of the movie image")]
        [Display(Name = "Image url*")]
        public string ImageUrl { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }


        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Please enter price of the movie")]
        [Range(0.1, double.MaxValue)]
        [Display(Name = "Price*")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Select if the movie is prefered or not")]
        [Display(Name = "Is prefered?*")]
        public bool? IsPreferedMovie { get; set; } = false;

        [Range(0, double.MaxValue)]
        [Required(ErrorMessage = "Please enter how many is left in stock")]
        [Display(Name = "In stock*")]
        public int? InStock { get; set; }

        [Required(ErrorMessage = "Please select category")]
        [Range(1, double.MaxValue)]
        public int? CategoryId { get; set; }
    }
}
