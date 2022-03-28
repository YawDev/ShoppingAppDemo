using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IOrderRepository : IRepository<Order>
    {
        
    }

    public class OrderRepository : IOrderRepository
    {

        ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.orderItems.AddRange(order.Items);
            _context.Orders.Add(order);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(Order order)
        {
            _context.Orders.Remove(order);
        }

        public void DeleteRange(IEnumerable<Order> orders)
        {
     
            _context.Orders.RemoveRange(orders);
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(Guid Id)
        {
            return _context.Orders.Include(x => x.Items ).ThenInclude(x => x.ItemListing)
            .Include(x => x.ShippingAddress).Include(x => x.User)
            .ToList().FirstOrDefault(x => x.Id == Id);
        }
    }
}
