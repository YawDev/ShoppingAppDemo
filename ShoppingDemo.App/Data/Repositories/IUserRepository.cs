using ShoppingDemo.App.Data.Entites;

namespace ShoppingDemo.EFCore
{
    public interface IUserRepository
    {
          int Commit();
        
        ApplicationUser GetByUserFullName(string fullname);
         ShippingAddress GetShippingAddress(string userId);
        

         BillingAddress GetBillingAddress(string userId);
        

         ApplicationUser GetByUserId(string userId);
        ApplicationUser GetByUserName(string username);

         void  CreateUser(ApplicationUser user);

         PaymentCard GetPaymentDetails(string userId);
         CardInformation GetCardInformation(string userId);

        void SavePaymentDetails(PaymentCard card);

        void SaveCardDetails(CardInformation cardInformation);

        void SaveShippingAddress(ShippingAddress address);
        
    }
}
