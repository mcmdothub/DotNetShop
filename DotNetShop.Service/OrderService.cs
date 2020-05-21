using DotNetShop.Data;
using DotNetShop.Data.Enums;
using DotNetShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Service
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly ShoppingCart _shoppingCart;

        public OrderService(ApplicationDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _context.Orders.Add(order);
            _context.SaveChanges();

            var orderDetails = new List<OrderDetail>(_shoppingCart.ShoppingCartItems.Count());

            foreach (var item in _shoppingCart.ShoppingCartItems)
            {
                orderDetails.Add(
                    new OrderDetail
                    {
                        OrderId = order.Id,
                        MovieId = item.Movie.Id,
                        Amount = Math.Min(item.Amount, item.Movie.InStock),
                        Price = item.Movie.Price,
                        Movie = item.Movie
                    });
                _context.Update(item.Movie);
                item.Movie.InStock = Math.Max(item.Movie.InStock - item.Amount, 0);
            }

            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();
        }

        public Order GetById(int orderId)
        {
            return GetAll()
                .FirstOrDefault(order => order.Id == orderId);
        }

        public IEnumerable<Order> GetByUserId(string userId)
        {
            return GetAll()
                .Where(order => order.User.Id == userId);
        }

        public IEnumerable<Order> GetFilteredOrders(
            string userId,
            OrderBy orderBy = OrderBy.None,
            int offset = 0, int limit = 10,
            decimal? minimalPrice = null,
            decimal? maximalPrice = null,
            DateTime? minDate = null,
            DateTime? maxDate = null,
            string zipCode = null)
        {
            var orders = string.IsNullOrEmpty(userId) ? GetAll() : GetByUserId(userId);

            if (orderBy != OrderBy.None)
            {
                SetOrderBy(orders, orderBy);
            }

            if (minimalPrice.HasValue)
            {
                orders = orders.Where(order => order.OrderTotal > minimalPrice);
            }

            if (maximalPrice.HasValue)
            {
                orders = orders.Where(order => order.OrderTotal > maximalPrice);
            }

            if (minDate.HasValue)
            {
                orders = orders.Where(order => order.OrderPlaced > minDate.Value);
            }

            if (maxDate.HasValue)
            {
                orders = orders.Where(order => order.OrderPlaced < maxDate.Value);
            }

            if (!string.IsNullOrEmpty(zipCode))
            {
                orders = orders.Where(order => order.ZipCode == zipCode);
            }

            return orders.Skip(offset).Take(limit);
        }

        private void SetOrderBy(IEnumerable<Order> orders, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.DateDesc:
                    orders = orders.OrderByDescending(order => order.OrderPlaced);
                    break;
                case OrderBy.DateAsc:
                    orders = orders.OrderBy(order => order.OrderPlaced);
                    break;
                case OrderBy.PriceAsc:
                    orders = orders.OrderBy(order => order.OrderTotal);
                    break;
                case OrderBy.PriceDesc:
                    orders = orders.OrderByDescending(order => order.OrderTotal);
                    break;
            }
        }

        public IEnumerable<Order> GetUserLatestOrders(int count, string userId)
        {
            return GetByUserId(userId)
                .OrderByDescending(order => order.OrderPlaced)
                .Take(count);
        }

        public IEnumerable<Movie> GetUserMostPopularMovies(string userId)
        {
            Dictionary<Movie, int> movies = new Dictionary<Movie, int>();

            var a = GetByUserId(userId);
            foreach (var order in a)
            {
                foreach (var line in order.OrderLines)
                {
                    if (movies.ContainsKey(line.Movie))
                    {
                        movies[line.Movie] += line.Amount;
                    }
                    else
                    {
                        movies[line.Movie] = line.Amount;
                    }
                }
            }

            return movies.OrderByDescending(keyValues => keyValues.Value).Select((keyValues) => keyValues.Key).Take(10);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
                .AsNoTracking()
                .Include(order => order.User)
                .Include(order => order.OrderLines).ThenInclude(line => line.Movie);
        }
    }
}