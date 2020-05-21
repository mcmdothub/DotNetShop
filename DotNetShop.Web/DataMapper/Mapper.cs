using DotNetShop.Data;
using DotNetShop.Data.Models;
using DotNetShop.Web.Models.Account;
using DotNetShop.Web.Models.Category;
using DotNetShop.Web.Models.Home;
using DotNetShop.Web.Models.Movie;
using DotNetShop.Web.Models.Order;
using DotNetShop.Web.Models.OrderDetail;
using DotNetShop.Web.Views.Movie;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Web.DataMapper
{
    public class Mapper
    {

        #region Category

        public Category CategoryListingToModel(CategoryListingModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
            };
        }

        public CategoryListingModel MovieToCategoryListing(Movie movie)
        {
            var category = movie.Category;
            return CategoryToCategoryListing(category);
        }

        public CategoryListingModel CategoryToCategoryListing(Category category)
        {
            return new CategoryListingModel
            {
                Name = category.Name,
                Description = category.Description,
                Id = category.Id,
                ImageUrl = category.ImageUrl
            };
        }

        #endregion


        #region Movie

        public NewMovieModel MovieToNewMovieModel(Movie movie)
        {
            return new NewMovieModel
            {
                Id = movie.Id,
                Name = movie.Name,
                CategoryId = movie.CategoryId,
                ImageUrl = movie.ImageUrl,
                InStock = movie.InStock,
                IsPreferedMovie = movie.IsPreferedMovie,
                LongDescription = movie.LongDescription,
                Price = movie.Price,
                ShortDescription = movie.ShortDescription,
            };
        }


        public Movie NewMovieModelToMovie(NewMovieModel model, bool newInstance, ICategory categoryService)
        {
            var movie = new Movie
            {
                Name = model.Name,
                Category = categoryService.GetById(model.CategoryId.Value),
                CategoryId = model.CategoryId.Value,
                ImageUrl = model.ImageUrl,
                InStock = model.InStock.Value,
                IsPreferedMovie = model.IsPreferedMovie.Value,
                LongDescription = model.LongDescription,
                Price = model.Price.Value,
                ShortDescription = model.ShortDescription,
            };

            if (!newInstance)
            {
                movie.Id = model.Id;
            }

            return movie;
        }

        private IEnumerable<MovieSummaryModel> MovieToMovieSummaryModel(IEnumerable<Movie> movies)
        {
            return movies.Select(movie => new MovieSummaryModel
            {
                Name = movie.Name,
                Id = movie.Id
            });
        }

        #endregion

        #region Home

        public HomeIndexModel MoviesToHomeIndexModel(IEnumerable<Movie> movies)
        {

            var moviesListing = movies.Select(movie => new MovieListingModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Category = CategoryToCategoryListing(movie.Category),
                ImageUrl = movie.ImageUrl,
                InStock = movie.InStock,
                Price = movie.Price,
                ShortDescription = movie.ShortDescription
            });

            return new HomeIndexModel
            {
                MoviesList = moviesListing
            };
        }

        #endregion

        #region Order

        public Order OrderIndexModelToOrder(OrderIndexModel model, ApplicationUser user)
        {
            return new Order
            {
                Id = model.Id,
                OrderPlaced = model.OrderPlaced,
                OrderTotal = model.OrderTotal,
                User = user,
                UserId = user.Id,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode
            };
        }

        private IEnumerable<OrderDetail> OrderDetailsListingModelToOrderDetails(IEnumerable<OrderDetailListingModel> orderLines)
        {
            return orderLines.Select(line => new OrderDetail
            {
                Amount = line.Amount,
                MovieId = line.Movie.Id,
                Id = line.Id,
                OrderId = line.OrderId,
                Price = line.Price
            });
        }

        public IEnumerable<OrderIndexModel> OrdersToOrderIndexModels(IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderIndexModel
            {
                Id = order.Id,
                Address = order.Address,
                City = order.City,
                Country = order.Country,
                OrderPlaced = order.OrderPlaced,
                OrderTotal = order.OrderTotal,
                UserId = order.UserId,
                ZipCode = order.ZipCode,
                UserFullName = $"{order.User.FirstName} {order.User.LastName}",
                OrderLines = OrderDetailsToOrderDetailsListingModel(order.OrderLines)
            });
        }



        private IEnumerable<OrderDetailListingModel> OrderDetailsToOrderDetailsListingModel(IEnumerable<OrderDetail> orderLines)
        {
            if (orderLines == null)
            {
                orderLines = Enumerable.Empty<OrderDetail>();
            }
            return orderLines.Select(orderLine => new OrderDetailListingModel
            {
                Amount = orderLine.Amount,
                Id = orderLine.Id,
                OrderId = orderLine.OrderId,
                Price = orderLine.Price,
                Movie = new Views.Movie.MovieSummaryModel
                {
                    Id = orderLine.MovieId,
                    Name = orderLine.Movie.Name
                },
                MovieId = orderLine.MovieId
            });
        }

        #endregion

        #region Account
        public AccountSettingsModel ApplicationUserToAccountSettingsModel(ApplicationUser user, string roleId)
        {
            return new AccountSettingsModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                ImageUrl = user.ImageUrl,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleId = roleId
            };
        }

        public ApplicationUser AccountRegisterModelToApplicationUser(AccountRegisterModel login)
        {
            return new ApplicationUser
            {
                FirstName = login.FirstName,
                AddressLine1 = login.AddressLine1,
                AddressLine2 = login.AddressLine2,
                City = login.City,
                Country = login.Country,
                Email = login.Email,
                ImageUrl = login.ImageUrl,
                MemberSince = DateTime.Now,
                Balance = 0,
                LastName = login.LastName,
                UserName = login.Email,
                Orders = Enumerable.Empty<Order>(),
                PhoneNumber = login.PhoneNumber,
            };
        }

        public AccountProfileModel ApplicationUserToAccountProfileModel(ApplicationUser user, IOrder orderService, string role)
        {
            return new AccountProfileModel
            {
                Id = user.Id,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                Balance = user.Balance,
                City = user.City,
                Country = user.Country,
                Email = user.Email,
                FirstName = user.FirstName,
                ImageUrl = user.ImageUrl,
                LastName = user.LastName,
                MemberSince = user.MemberSince,
                PhoneNumber = user.PhoneNumber,
                MostPopularMovies = MovieToMovieSummaryModel(orderService.GetUserMostPopularMovies(user.Id)),
                OrderCount = orderService.GetByUserId(user.Id).Count(),
                LatestOrders = OrdersToOrderIndexModels(orderService.GetUserLatestOrders(5, user.Id)),
                Role = role,
                TotalSpent = orderService.GetByUserId(user.Id).Sum(order => order.OrderTotal)
            };
        }

        public void AccountSettingsModelToApplicationUser(AccountSettingsModel model, ApplicationUser user)
        {
            user.City = model.City;
            user.AddressLine1 = model.AddressLine1;
            user.AddressLine2 = model.AddressLine2;
            user.Country = model.Country;
            user.FirstName = model.FirstName;
            user.ImageUrl = model.ImageUrl;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
        }

        public async Task<IEnumerable<AccountProfileModel>> ApplicationUsersToAccountProfileModelsAsync(IEnumerable<ApplicationUser> users, IOrder orderService, UserManager<ApplicationUser> userManager)
        {
            List<AccountProfileModel> models = new List<AccountProfileModel>(users.Count());

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                models.Add(ApplicationUserToAccountProfileModel(user, orderService, roles.FirstOrDefault()));
            }

            return models;
        }

        #endregion
    }
}
