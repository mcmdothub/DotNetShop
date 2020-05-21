using DotNetShop.Web.Models.Category;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Web.Models.Movie
{
    public class MovieListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Total { get => (Price * Amount).ToString("c", CultureInfo.CreateSpecificCulture("en-US")); }
        public int InStock { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }
        public int Amount { get; set; } = 1;
        public CategoryListingModel Category { get; set; }
    }
}
