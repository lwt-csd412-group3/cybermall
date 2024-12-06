using CyberMall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberMall.Areas.Identity.Pages.Account.Manage
{
    public partial class CreatePaymentMethodModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CreatePaymentMethodModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [CreditCard]
            [Display(Name = "Card Number")]
            public string NewCardNumber { get; set; }

            [Display(Name = "Expiration Month")]
            [StringLength(maximumLength: 2)]
            public string NewExpirationMonth { get; set; }

            [Display(Name = "Expiration Year")]
            [StringLength(maximumLength: 4)]
            public string NewExpirationYear { get; set; }


            [Display(Name = "Security Code / CVC")]
            public string NewCVC { get; set; }

            [Required]
            [Display(Name = "Cardholder First Name")]
            public string NewFirstName { get; set; }

            [Required]
            [Display(Name = "Cardholder Last Name")]
            public string NewLastName { get; set; }


            [Required]
            [Display(Name = "Address Line 1")]
            public string NewAddressLine1 { get; set; }

            [Display(Name = "Address Line 2")]
            public string NewAddressLine2 { get; set; }


            [Required]
            [Display(Name = "City")]
            public string NewCity { get; set; }

            [Display(Name = "Region")]
            public string NewRegion { get; set; }

            [Required]
            [Display(Name = "Country")]
            public string NewCountry { get; set; }

            [Required]
            [Display(Name = "Postal Code")]
            public string NewZipCode { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            // to-do: either do something with this stub or get rid of it
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // to-do: complete this
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            CardPaymentMethod paymentMethod = new CardPaymentMethod
            {
                FirstName = Input.NewFirstName,
                LastName = Input.NewLastName,
                CardNumber = Input.NewCardNumber,
                ExpirationMonth = byte.Parse(Input.NewExpirationMonth),
                ExpirationYear = short.Parse(Input.NewExpirationYear),
                CVC = Input.NewCVC,
                // to-do: do something better than auto assign default
                PaymentType = CardPaymentType.Generic,
                BillingAddress = new Address
                {
                    AddressLine1 = Input.NewAddressLine1,
                    AddressLine2 = Input.NewAddressLine2,
                    City = Input.NewCity,
                    Region = Input.NewRegion,
                    Country = Input.NewCountry,
                    ZipCode = Input.NewZipCode
                }
            };

            user.PaymentMethods.Add(paymentMethod);
           
            await _userManager.UpdateAsync(user);
            return RedirectToPage("ManagePaymentMethods");
        }
    }
}
