using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IItemRepository : IRepository<Item>
    {
        
    }

    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Item item)
        {
            _context.Items.Add(item);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(Item item)
        {
            _context.Items.Remove(item);
        }

        public void DeleteRange(IEnumerable<Item> items)
        {
            _context.Items.RemoveRange(items);
        }

        public IEnumerable<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        public Item GetById(Guid Id)
        {
            return _context.Items.FirstOrDefault(x => x.Id == Id);
        }
    }
}
