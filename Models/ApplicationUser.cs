using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CyberMall.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public List<Order> OrderHistory { get; set; }
    }
}
