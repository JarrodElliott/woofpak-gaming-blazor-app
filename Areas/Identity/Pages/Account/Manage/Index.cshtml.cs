using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using WoofpakGamingSiteServerApp.ApplicationServices;
using WoofpakGamingSiteServerApp.Data;
using WoofpakGamingSiteServerApp.Data.Services;

namespace WoofpakGamingSiteServerApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            ApplicationUserManager userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [Display(Name = "Woofpak Key")]
        public string WoofpakKey { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; }


        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        public byte[] ProfilePhoto { get; set; }

        public byte[] NewProfilePhoto { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Twitch Username")]
            public string TwitchUsername { get; set; }

            [Display(Name = "Twitch Description")]
            public string TwitchDescription { get; set; }


            [Display(Name = "Streaming for Extra Life")]
            public bool StreamingForExtraLife { get; set; }

            [Display(Name = "Email me about news and events")]
            public bool AllowWoofpakEmails { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            Username = user.UserName;
            WoofpakKey = user.WoofpakKey;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            ProfilePhoto = user.ProfilePhoto;

            if (user != null)
            {

                Input = new InputModel
                {
                    TwitchUsername = user.TwitchUsername ?? string.Empty,
                    TwitchDescription = user.TwitchDescription ?? string.Empty,
                    AllowWoofpakEmails = user.AllowEmails,
                    StreamingForExtraLife = user.IsExtraLifeStreamer

                    //            var userName = await _userManager.GetUserNameAsync(user);


                };  
            }
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
                        StatusMessage = string.Empty;

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
            StatusMessage = string.Empty;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            bool allowEmails = user.AllowEmails;
            bool streamingForExtraLife = user.IsExtraLifeStreamer;
            string twitchUser = user.TwitchUsername;// await _userManager.GetTwitchUsernameAsync(user);
            if (!string.IsNullOrEmpty(Input.TwitchUsername) && Input.TwitchUsername != twitchUser)
            {
                IdentityResult setTwitchUser = await _userManager.SetTwitchUsernameAsync(user, Input.TwitchUsername);
                if (!setTwitchUser.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set Twitch Username.";
                }
            }
            if (Input.StreamingForExtraLife != streamingForExtraLife)
            {
                IdentityResult setStreaming = await _userManager.SetExtraLifeStreamingAsync(user, Input.StreamingForExtraLife);
                if (!setStreaming.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update Extra Life Info.";
                }
            }

            if (Input.AllowWoofpakEmails != allowEmails)
            {
                IdentityResult setAllowEmails = await _userManager.SetAllowEmailsAsync(user, Input.AllowWoofpakEmails);
                if (!setAllowEmails.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update Allow Emails.";
                }
            }

            //if(NewProfilePhoto != null && NewProfilePhoto != ProfilePhoto)
            //{
            //    IdentityResult updateProfilePhoto = await _userManager.SetProfilePhotoAsync(user, NewProfilePhoto);
            //    if (!updateProfilePhoto.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to update Profile Photo.";
            //    }
            //}

            if (!string.Equals(Input.TwitchDescription, user.TwitchDescription))
            {
                IdentityResult setTwitchDescription = await _userManager.SetTwitchDescriptionAsync(user, Input.TwitchDescription);
                if (!setTwitchDescription.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update Twitch Description.";
                }
                
            }


            if (!string.IsNullOrEmpty(StatusMessage))
            {
                return RedirectToPage();
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> ResendConfirmationEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {
                if (!string.IsNullOrEmpty(user.Email))
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
                }
                return Page();
            }

        }
    }
}
