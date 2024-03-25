using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Woofpak Key")]
        public string WoofpakKey { get; set; }

        [Required]
        public bool AllowEmails { get; set; }

        [Display(Name = "Twitch Username")]
        public string TwitchUsername { get; set; }

        [Display(Name = "Twitch Description")]
        public string TwitchDescription { get; set; }

        [Display(Name = "Extra Life Streamer")]
        public bool IsExtraLifeStreamer { get; set; }

        public byte[] ProfilePhoto { get; set; }

    }
}
