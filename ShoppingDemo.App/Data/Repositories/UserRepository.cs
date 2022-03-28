using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
           return _context.SaveChanges();
        }

        public ShippingAddress GetShippingAddress(string userId)
        {
            return _context.ShippingAddresses.Include(x => x.User).FirstOrDefault(x => x.User.Id == userId);
        }

        public BillingAddress GetBillingAddress(string userId)
        {
            return _context.BillingAddresses.Include(x => x.User).FirstOrDefault(x => x.User.Id == userId);
        }

        public ApplicationUser GetByUserId(string userId)
        {
            return _context.User.FirstOrDefault(x => x.Id == userId);
        }

        public PaymentCard GetPaymentDetails(string userId)
        {
            return _context.PaymentCards.Include(x => x.User).Include(x => x.BillingAddress)
            .FirstOrDefault(x => x.User.Id == userId);
        }

        public CardInformation GetCardInformation(string userId)
        {
            return _context.CardInformation.Include(x => x.User)
            .Include(x => x.BillingAddress).FirstOrDefault(x => x.User.Id == userId);
        }

        public void SavePaymentDetails(PaymentCard card)
        {
            _context.PaymentCards.Add(card);
        }

        public void SaveCardDetails(CardInformation cardInformation)
        {
            _context.CardInformation.Add(cardInformation);
        }

        public void SaveShippingAddress(ShippingAddress address)
        {
            _context.ShippingAddresses.Add(address);
        }

        public ApplicationUser GetByUserName(string username)
        {
            return  _context.User.FirstOrDefault(x => x.UserName == username);

        }

        public void CreateUser(ApplicationUser user)
        {
            _context.User.Add(user);
        }
    }
}
