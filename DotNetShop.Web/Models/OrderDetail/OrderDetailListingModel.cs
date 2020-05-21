using DotNetShop.Web.Views.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Web.Models.OrderDetail
{
    public class OrderDetailListingModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MovieId { get; set; }
        public MovieSummaryModel Movie { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
