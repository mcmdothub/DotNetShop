﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetShop.Data.Models
{
    public class ShoppingCart
	{
		private readonly ApplicationDbContext _context;

		public ShoppingCart(ApplicationDbContext context)
		{
			_context = context;
		}

		public string Id { get; set; }
		public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<ApplicationDbContext>();
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			session.SetString("CartId", cartId);
			return new ShoppingCart(context) { Id = cartId };
		}

		public bool AddToCart(Movie movie, int amount)
		{
			if (movie.InStock == 0 || amount == 0)
			{
				return false;
			}

			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Movie.Id == movie.Id && s.ShoppingCartId == Id);
			var isValidAmount = true;
			if (shoppingCartItem == null)
			{
				if (amount > movie.InStock)
				{
					isValidAmount = false;
				}
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = Id,
					Movie = movie,
					Amount = Math.Min(movie.InStock, amount)
				};
				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				if (movie.InStock - shoppingCartItem.Amount - amount >= 0)
				{
					shoppingCartItem.Amount += amount;
				}
				else
				{
					shoppingCartItem.Amount += (movie.InStock - shoppingCartItem.Amount);
					isValidAmount = false;
				}
			}


			_context.SaveChanges();
			return isValidAmount;
		}

		public int RemoveFromCart(Movie movie)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Movie.Id == movie.Id && s.ShoppingCartId == Id);
			int localAmount = 0;
			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();
			return localAmount;
		}

		public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??
				   (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
					   .Include(s => s.Movie));
		}

		public void ClearCart()
		{
			var cartItems = _context
				.ShoppingCartItems
				.Where(cart => cart.ShoppingCartId == Id);

			_context.ShoppingCartItems.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public decimal GetShoppingCartTotal()
		{
			return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
				.Select(c => c.Movie.Price * c.Amount).Sum();
		}

	}
}
