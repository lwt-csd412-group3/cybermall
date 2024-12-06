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


        public int SelectedAddressIndex { get; set; }

        public int SelectedPaymentIndex { get; set; }


        public CartControllerCheckoutModel()
        {
        }


        public CartControllerCheckoutModel(ApplicationUser user, Order order)
        {
            User = user;
            Order = order;

            ShippingAddressList = new List<SelectListItem>();

            PaymentMethodList = new List<SelectListItem>();

            addAddressToList(user.PrimaryAddress, -1);

            int i = 0;

            foreach (Address address in user.SecondaryAddresses)
            {
                addAddressToList(address, i);
            }

            i = 0;

            foreach (CardPaymentMethod paymentMethod in user.PaymentMethods)
            {
                addPaymentMethodToList(paymentMethod, i++);
            }

        }

        private void addAddressToList(Address address, int index)
        {
            ShippingAddressList.Add(
                new SelectListItem
                {
                    Text = address.ToString(),
                    Value = index.ToString()
                }
            );
        }

        private void addPaymentMethodToList(CardPaymentMethod paymentMethod, int index)
        {
            PaymentMethodList.Add(
                new SelectListItem
                {
                    Text = paymentMethod.ToString(),
                    Value = index.ToString()
                }
            );
        }

    }
}
