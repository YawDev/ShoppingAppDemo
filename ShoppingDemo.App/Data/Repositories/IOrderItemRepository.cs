using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        
    }


    public class OrderItemRepository : IOrderItemRepository
    {
        ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(OrderItem item)
        {
            _context.orderItems.AddRange(item);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(OrderItem item)
        {
            _context.orderItems.Remove(item);
        }

        public void DeleteRange(IEnumerable<OrderItem> items)
        {
            _context.orderItems.RemoveRange(items);
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _context.orderItems.ToList();
        }

        public OrderItem GetById(Guid Id)
        {
            return _context.orderItems.Include(x => x.order).ThenInclude(x => x.ShippingAddress)
            .ToList().Find(x => x.Id == Id);
        }
    }
}
