using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Services
{
    public interface IUserDetailsComposition
    {
        PaymentCard SavePaymentCard(PaymentCard card, CardModel model,ApplicationUser user);
        void SaveShippingAddress(ShippingAddress card, AddressModel model,ApplicationUser user);

        void SaveCardDetails(CardInformation card, PaymentCard paymentCard,ApplicationUser user);



    }
    public class UserDetailsComposition : IUserDetailsComposition
    {

        private readonly IUserRepository _userRepository;

        public UserDetailsComposition(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void SaveCardDetails(CardInformation cardInfo, PaymentCard paymentCard,ApplicationUser user)
        {
            if(cardInfo == null)
            {  
                cardInfo = new CardInformation
                {
                    NameOnCard = paymentCard.NameOnCard,
                    CVV = paymentCard.CVV,
                    CardNumber = paymentCard.CardNumber
                };
                cardInfo.BillingAddress = paymentCard.BillingAddress;
                cardInfo.User = user;
                _userRepository.SaveCardDetails(cardInfo);
            }
            else
            {
                cardInfo.NameOnCard = paymentCard.NameOnCard;
                cardInfo.CVV = paymentCard.CVV;
                cardInfo.CardNumber = paymentCard.CardNumber;
                cardInfo.BillingAddress.Addressline1 = paymentCard.BillingAddress.Addressline1;
                cardInfo.BillingAddress.Addressline2 = paymentCard.BillingAddress.Addressline2;
                cardInfo.BillingAddress.Addressline3 = paymentCard.BillingAddress.Addressline3;
                cardInfo.BillingAddress.State = paymentCard.BillingAddress.State;
                cardInfo.BillingAddress.Zipcode = paymentCard.BillingAddress.Zipcode;
                cardInfo.BillingAddress.Country = paymentCard.BillingAddress.Country;
            }
            _userRepository.Commit();
        }

        public PaymentCard SavePaymentCard(PaymentCard paymentCard, CardModel model,ApplicationUser user)
         {
             if(paymentCard == null)
                {
                    paymentCard = new PaymentCard()
                    {
                        CardNumber = model.CardNumber,
                        CVV = model.CVV,
                        NameOnCard = model.NameOnCard
                    };
                    paymentCard.BillingAddress = new BillingAddress()
                    {
                        Addressline1 = model.BillingAddress.Addressline1,
                        Addressline2 = model.BillingAddress.Addressline2,
                        Addressline3 = model.BillingAddress.Addressline3,
                        State = model.BillingAddress.State,
                        Zipcode = model.BillingAddress.Zipcode,
                        Country = model.BillingAddress.Country
                    };
                    paymentCard.User = user;
                    paymentCard.BillingAddress.User=user;
                    _userRepository.SavePaymentDetails(paymentCard);
                }
                else
                {
                    paymentCard.CardNumber = model.CardNumber;
                    paymentCard.CVV = model.CVV;
                    paymentCard.NameOnCard = model.NameOnCard;
                    paymentCard.BillingAddress.Addressline1 = model.BillingAddress.Addressline1;
                    paymentCard.BillingAddress.Addressline2 = model.BillingAddress.Addressline2;
                    paymentCard.BillingAddress.Addressline3 = model.BillingAddress.Addressline3;
                    paymentCard.BillingAddress.Zipcode = model.BillingAddress.Zipcode;
                    paymentCard.BillingAddress.State = model.BillingAddress.State;
                    paymentCard.BillingAddress.Country = model.BillingAddress.Country;
                    paymentCard.BillingAddress.User = user;
                }
                _userRepository.Commit();
                return paymentCard;
         }

        public void SaveShippingAddress(ShippingAddress shippingAddress, AddressModel model,ApplicationUser user)
        {
               if(shippingAddress == null)
                {
                    shippingAddress = new ShippingAddress()
                    {
                        Addressline1 = model.Addressline1,
                        Addressline2 = model.Addressline2,
                        Addressline3 = model.Addressline3,
                        State = model.State,
                        Zipcode = model.Zipcode,
                        Country = model.Country
                    };
                    shippingAddress.User = user;
                    _userRepository.SaveShippingAddress(shippingAddress);
                }   
                else
                {
                    shippingAddress.Addressline1 = model.Addressline1;
                    shippingAddress.Addressline2 = model.Addressline2;
                    shippingAddress.Addressline3 = model.Addressline3;
                    shippingAddress.State = model.State;
                    shippingAddress.Zipcode = model.Zipcode;
                    shippingAddress.Country = model.Country;
                }
                _userRepository.Commit();
        }
    }
}