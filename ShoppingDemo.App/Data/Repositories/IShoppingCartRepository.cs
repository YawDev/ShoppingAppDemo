using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        ShoppingCart GetByUserId(string userId);
    }

    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(ShoppingCart cart)
        {
            _context.ShoppingCarts.Remove(cart);
        }

        public void DeleteRange(IEnumerable<ShoppingCart> carts)
        {
            _context.ShoppingCarts.RemoveRange(carts);
        }

        public IEnumerable<ShoppingCart> GetAll()
        {
            return _context.ShoppingCarts.ToList();
        }

        public ShoppingCart GetById(Guid Id)
        {
            return _context.ShoppingCarts.FirstOrDefault(x => x.Id == Id);
        }

        public ShoppingCart GetByUserId(string userId)
        {
            return _context.ShoppingCarts.Include(x => x.Items)
                .ThenInclude(x => x.ItemListing).FirstOrDefault(x => x.User.Id == userId);
        }
    }
}
