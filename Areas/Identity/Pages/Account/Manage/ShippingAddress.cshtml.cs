﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using CyberMall.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace CyberMall.Areas.Identity.Pages.Account.Manage
{
    public partial class ShippingAddressModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public ShippingAddressModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }


        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            // To-Do: Write Input Model
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
            AddressLine1 = user.AddressLine1;
            AddressLine2 = user.AddressLine2;
            City = user.City;
            Region = user.Region;
            Country = user.Country;
            ZipCode = user.ZipCode;

            Input = new InputModel
            {
                NewAddressLine1 = user.AddressLine1,
                NewAddressLine2 = user.AddressLine2,
                NewCity = user.City,
                NewRegion = user.Region,
                NewCountry = user.Country,
                NewZipCode = user.ZipCode
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
            user.AddressLine1 = Input.NewAddressLine1;
            user.AddressLine2 = Input.NewAddressLine2;
            user.City = Input.NewCity;
            user.Region = Input.NewRegion;
            user.Country = Input.NewCountry;
            user.ZipCode = Input.NewZipCode;
            await _userManager.UpdateAsync(user);
            return RedirectToPage();
        }
    }
}
