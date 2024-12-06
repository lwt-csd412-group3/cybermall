using System.Net;

namespace CyberMall.Models
{
    public class Address
    {

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public new string ToString()
        {
            return AddressLine1 + "\n"
                        + (AddressLine2 ?? "") + "\n"
            + City + " " + Region + " " + Country + "\n"
            + ZipCode;
        }

    }
}
