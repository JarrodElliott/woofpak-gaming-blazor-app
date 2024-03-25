using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WoofpakGamingSiteServerApp.ApplicationServices;
using WoofpakGamingSiteServerApp.Data;
using WoofpakGamingSiteServerApp.Data.Services;

namespace WoofpakGamingSiteServerApp.Areas.Identity.Pages.Account
{
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(ApplicationUserManager userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Email = user.Email;

            //var userName = await _userManager.GetUserNameAsync(user);
            //var woofpakKey = _userManager.GetWoofpakKey(user);
            //var email = await _userManager.GetEmailAsync(user);
            //Username = userName;
            //WoofpakKey = woofpakKey;
            //Email = email;
            //Input = new InputModel
            //{
            //    PhoneNumber = phoneNumber
            //};
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
        //public void OnGet()
        //{

        //}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else if (!string.IsNullOrEmpty(user.Email))
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = Url.Content("~/") },
                    protocol: Request.Scheme);

                string htmlString = string.Format("Please confirm your account by clicking the link below:\r\n{0}", callbackUrl);

                EmailService.SendConfirmationEmailAsync(user.Email, htmlString).Wait();
                ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");

            }
            else
            {
                return NotFound($"Unable to find email for user");
            }

            return Page();
        }
    }
}
