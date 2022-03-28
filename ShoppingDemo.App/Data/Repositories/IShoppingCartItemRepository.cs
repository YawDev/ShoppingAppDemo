using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
        List<ShoppingCartItem> GetAllByItemId(Item item);
        ShoppingCartItem GetByItemId(Guid Id);
        List<ShoppingCartItem> GetAllByItemId(Guid Id);

    }

    public class ShoppingCartItemRepository : IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Add(item);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Remove(item);
        }

        public IEnumerable<ShoppingCartItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetAllByItemId(Guid Id)
        {
            return _context.ShoppingCartItems.Include(x => x.ItemListing).ToList()
            .FindAll(x => x.ItemListing.Id==Id);
        }

        public void DeleteRange(IEnumerable<ShoppingCartItem> items)
        {
            _context.ShoppingCartItems.RemoveRange(items);
        }

        public List<ShoppingCartItem> GetAllByItemId(Item item)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartItem GetByItemId(Guid Id)
        {
            return _context.ShoppingCartItems?.FirstOrDefault(x => x.ItemListing.Id==Id);
        }
    }
}
