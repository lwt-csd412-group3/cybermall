using CyberMall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CyberMall.Areas.Identity.Pages.Account.Manage
{
    public partial class ManagePaymentMethodsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManagePaymentMethodsModel(
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

        }

        private async Task LoadAsync(ApplicationUser user)
        {
           /* CardPaymentMethod paymentMethod = user.PaymentMethods.First();
            if (paymentMethod == null)
            {
                user.PaymentMethods.Add(new CardPaymentMethod
                {
                    PaymentType = CardPaymentType.Generic
                });
            }*/

            Address cardAddress = new Address();

            Input = new InputModel
            {
                
            };
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
    }
}
