using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetOrdersByCustomer(Guid Id);
        IEnumerable<Order> GetOrdersByUserId(string Id);

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


        public IEnumerable<Order> GetOrdersByUser(Guid Id)
        {
            return _context.Orders.Include(x => x.Items).Include(x => x.ShippingAddress)
            .Include(x => x.Card).ThenInclude(x => x.BillingAddress)
            .Include(x => x.Customer)
            .ToList().FindAll(x => x.Customer.Id==Id);
        }

        public Order GetById(Guid Id)
        {
            return _context.Orders.Include(x => x.Items)
            .Include(x => x.ShippingAddress).Include(x => x.Customer)
            .ToList().FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Order> GetOrdersByCustomer(Guid Id)
        {
            return _context.Orders.Include(x => x.Items).Include(x => x.ShippingAddress)
            .Include(x => x.Card).ThenInclude(x => x.BillingAddress)
            .Include(x => x.Customer)
            .ToList().FindAll(x => x.Customer.Id==Id);        
        }

        public IEnumerable<Order> GetOrdersByUserId(string Id)
        {
            return _context.Orders.Include(x => x.Items).Include(x => x.ShippingAddress)
            .Include(x => x.Card).ThenInclude(x => x.BillingAddress)
            .Include(x => x.Customer)
            .ToList().FindAll(x => x.UserId ==Id);         }
    }
}
