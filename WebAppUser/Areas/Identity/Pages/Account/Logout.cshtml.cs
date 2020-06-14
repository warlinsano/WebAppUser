using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebAppUser.Data;
using WebAppUser.Helpers;
using WebAppUser.ViewModels;

namespace WebAppUser.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<User> _UserManager;
        private readonly ILogToDB _LogToDB;

        public LogoutModel(SignInManager<User> signInManager, 
            ILogger<LogoutModel> logger,
           UserManager<User> UserManager,
           ILogToDB logToDB

            )
        {
            _signInManager = signInManager;
            _UserManager = UserManager;
            _LogToDB=logToDB;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var Email = User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            //Log
            var Model = new ActivityLogViewModel
            {
                UserEMail = Email,
                Action = "Logout",
                Description = "User logged out.",
                Controller = "Account",
                Status = false,
                Type = "AppWeb"
            };
            var logIsSuccees = await _LogToDB.Create(Model);
            //      
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
