
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
        public string PIN { get; set; }

        public virtual Address BillingAddress { get; set; }

    }

    public enum CardPaymentType
    {
        Visa, MasterCard, Amex, Discover, UnionPay
    }
}
