using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
         Customer GetByName(string first, string last);
    }

    public class CustomerRepository : ICustomerRepository
    {
        ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customer.Add(customer);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Delete(Customer customer)
        {
            _context.Customer.Remove(customer);
        }

        public void DeleteRange(IEnumerable<Customer> customers)
        {
            _context.Customer.RemoveRange(customers);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customer.ToList();
        }

        public Customer GetById(Guid Id)
        {
            return _context.Customer.ToList().Find(x => x.Id == Id);
        }

         public Customer GetByName(string first, string last)
        {
            return _context.Customer.ToList().Find(x => x.FirstName == first && x.LastName == last);
        }
    }
}