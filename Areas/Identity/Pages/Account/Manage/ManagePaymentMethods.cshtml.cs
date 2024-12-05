using CyberMall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
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

        public List<CardPaymentMethod> PaymentMethods { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Action { get; set; }  // unused; see OnPostAsync for further details

			[Required]
			public long CardIdToDelete { get; set;  }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {

            };


            PaymentMethods = user.PaymentMethods;
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
            // we might have multiple action handlers in the future if we were to edit stuff
            // but we don't need to edit anything right now
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.PaymentMethods.RemoveAll(i => i.Id == Input.CardIdToDelete);


            await _userManager.UpdateAsync(user);
            return RedirectToPage();
        }
    }
}
