using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;

namespace CyberMall.Models
{
    public class CartControllerCheckoutModel
    {
        public ApplicationUser User { get; set; }
        public Order Order { get; set; }

        public List<SelectListItem> ShippingAddressList { get; private set; }

        public List<SelectListItem> PaymentMethodList { get; private set; }

        public CartControllerCheckoutModel(ApplicationUser user, Order order)
        {
            User = user;
            Order = order;

            ShippingAddressList = new List<SelectListItem>();

            PaymentMethodList = new List<SelectListItem>();

            addAddressToList(user.PrimaryAddress);

            foreach (Address address in user.SecondaryAddresses)
            {
                addAddressToList(address);
            }

            foreach (CardPaymentMethod paymentMethod in user.PaymentMethods)
            {
                addPaymentMethodToList(paymentMethod);
            }
        }

        private void addAddressToList(Address address)
        {
            ShippingAddressList.Add(
                new SelectListItem
                {
                    Text = address.AddressLine1 + "\n"
                        + (address.AddressLine2 ?? "")
                        + address.City + " " + address.Region + " " + address.Country + "\n"
                        + address.ZipCode,
                    Value = address.Id.ToString()
                }
            );
        }

        private void addPaymentMethodToList(CardPaymentMethod paymentMethod)
        {
            PaymentMethodList.Add(
                new SelectListItem
                {
                    Text = paymentMethod.PaymentType.ToString() + " *" + paymentMethod.LastFourDigits,
                    Value = paymentMethod.Id.ToString()
                }
            );
        }

    }
}
