using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;

namespace CyberMall.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Address PrimaryAddress { get; set; }

        public virtual List<Address> SecondaryAddresses { get; set; }

        public virtual List<Order> OrderHistory { get; set; }

        public virtual List<ItemSale> ItemsInCart { get; set; }
    }
}
