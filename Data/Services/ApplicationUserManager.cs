using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WoofpakGamingSiteServerApp.Data.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private static RNGCryptoServiceProvider rngCsp;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            if(rngCsp == null)
            {
                 rngCsp = new RNGCryptoServiceProvider();
            }
        }


        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            //TODO: Something sensible
            string userKey = GetWoofpakKey();
            using (var context = new ApplicationDbContext())
            {
                while (context.ApplicationUser.Where(u => string.Equals(u.WoofpakKey, userKey)).Count() > 0)
                {
                    userKey = GetWoofpakKey();
                }

            }

            user.WoofpakKey = userKey;
            var t =  await base.CreateAsync(user);

            return t;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            //TODO: Something sensible
            string userKey = GetWoofpakKey();
            using (var context = new ApplicationDbContext())
            {
                while (context.ApplicationUser.Where(u => string.Equals(u.WoofpakKey, userKey)).Count() > 0)
                {
                    userKey = GetWoofpakKey();
                }

            }

            user.WoofpakKey = userKey;
            return await base.CreateAsync(user, password);
        }

        public string GetWoofpakKey(ApplicationUser user)
        {
            return user.WoofpakKey;
            //using (var context = new ApplicationDbContext())
            //{
            //    user.WoofpakKey;
            //    var t = context.ApplicationUser.FindAsync(t => t.Game).ToList();
            //    return Task.FromResult(t);
            //}
        }


        private string GetWoofpakKey()
        {
            int maxSize = 20;

            int size = maxSize;
            byte[] data = new byte[size];
            rngCsp.GetNonZeroBytes(data);
            StringBuilder keyStringBuilder = new StringBuilder();
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            keyStringBuilder.Append("#WP");
            foreach (var i in data)
            {
                keyStringBuilder.Append(validChars[(int)(i % validChars.Length)]);
            }
            keyStringBuilder.Append('#');
            var generatedKey = keyStringBuilder.ToString();

            keyStringBuilder = null;
            return generatedKey;
        }

        internal async Task<IdentityResult> SetTwitchUsernameAsync(ApplicationUser user, string twitchUsername)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var curUser = await FindByIdAsync(user.Id);

                    if (!string.Equals(curUser.TwitchUsername, twitchUsername))
                    {
                        curUser.TwitchUsername = twitchUsername;
                        context.Users.Attach(curUser).Property("TwitchUsername").IsModified = true;
                        await context.SaveChangesAsync();
                    }

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    IdentityError error = new IdentityError();
                    error.Description = ex.Message;
                    return IdentityResult.Failed(error);
                }
            }
        }

        internal async Task<string> GetTwitchUsernameAsync(ApplicationUser user)
        {
            using (var context = new ApplicationDbContext())
            {
                var curUser = await FindByIdAsync(user.Id);

                return curUser.TwitchUsername;
                //context.ApplicationUser.Where(u => u.Id == user.Id).Select(user.TwitchUsername);
            }
        }

        internal async Task<IdentityResult> SetExtraLifeStreamingAsync(ApplicationUser user, bool isStreaming)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var curUser = await FindByIdAsync(user.Id);

                    if (!string.Equals(curUser.IsExtraLifeStreamer, isStreaming))
                    {
                        curUser.IsExtraLifeStreamer = isStreaming;
                        context.Users.Attach(curUser).Property("IsExtraLifeStreamer").IsModified = true;
                        await context.SaveChangesAsync();
                    }

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    IdentityError error = new IdentityError();
                    error.Description = ex.Message;
                    return IdentityResult.Failed(error);
                }
            }
        }

        internal async Task<IdentityResult> SetAllowEmailsAsync(ApplicationUser user, bool allowEmails)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var curUser = await FindByIdAsync(user.Id);

                    if (!string.Equals(curUser.AllowEmails, allowEmails))
                    {
                        curUser.AllowEmails = allowEmails;
                        context.Users.Attach(curUser).Property("AllowEmails").IsModified = true;
                        await context.SaveChangesAsync();
                    }

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    IdentityError error = new IdentityError();
                    error.Description = ex.Message;
                    return IdentityResult.Failed(error);
                }
            }
        }

        internal async Task<IdentityResult> SetTwitchDescriptionAsync(ApplicationUser user, string twitchDescription)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var curUser = await FindByIdAsync(user.Id);

                    if (!string.Equals(curUser.TwitchDescription, twitchDescription))
                    {
                        curUser.TwitchDescription = twitchDescription;
                        context.Users.Attach(curUser).Property("TwitchDescription").IsModified = true;
                        await context.SaveChangesAsync();
                    }

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    IdentityError error = new IdentityError();
                    error.Description = ex.Message;
                    return IdentityResult.Failed(error);
                }
            }
        }

        public async Task<List<ApplicationUser>> LoadExtraLifeStreamersAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var t = context.ApplicationUser.Where(u=> u.TwitchUsername != null && u.IsExtraLifeStreamer).ToList();
                return await Task.FromResult(t);
            }
        }
        internal async Task<IdentityResult> SetProfilePhotoAsync(ApplicationUser user, byte [] photo )
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var curUser = await FindByIdAsync(user.Id);

                    if (!string.Equals(curUser.ProfilePhoto, photo))
                    {
                        curUser.ProfilePhoto = photo;
                        context.Users.Attach(curUser).Property("ProfilePhoto").IsModified = true;
                        await context.SaveChangesAsync();
                    }

                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    IdentityError error = new IdentityError();
                    error.Description = ex.Message;
                    return IdentityResult.Failed(error);
                }
            }
        }
        
    }
}
