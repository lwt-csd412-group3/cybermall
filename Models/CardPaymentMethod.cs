
namespace CyberMall.Models
{
    public class CardPaymentMethod
    {
        public long Id { get; set; }

        // normally this would be stored encrypted with a key stored elsewhere
        public string CardNumber { get; set; }
        public CardPaymentType PaymentType { get; set; }
        public byte ExpirationMonth { get; set; }
        public short ExpirationYear { get; set; }
        public string CVC { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Address BillingAddress { get; set; }

        public string LastFourDigits
        {
            get
            {
                return CardNumber.Substring(CardNumber.Length - 4);
            }
        }

    }

    public enum CardPaymentType
    {
        Generic, Visa, MasterCard, Amex, Discover, UnionPay
    }
}
