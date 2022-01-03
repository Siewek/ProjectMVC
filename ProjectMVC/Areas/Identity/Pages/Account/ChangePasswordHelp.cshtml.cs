using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using ProjectMVC.Data;

namespace ProjectMVC.Areas.Identity.Pages.Account
{
    public class ChangePasswordHelpModel : PageModel
    {
        private readonly UserManager<LibraryTwoUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ChangePasswordHelpModel(UserManager<LibraryTwoUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (ModelState.IsValid)
            {
                //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
               // var email = await _userManager.GetEmailAsync(User);


                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ChangePassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                /*await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/

                var messsage = new MimeMessage();
                messsage.From.Add(MailboxAddress.Parse("PrzepisyKulinarne.com"));
                messsage.To.Add(MailboxAddress.Parse(user.Email));
                messsage.Subject = "Weryfikacja Konta";
                messsage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = callbackUrl
                };
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Authenticate("przepisykulinarneps6mo@gmail.com", "YX5ibhzvFPrvp#n");
                    client.Send(messsage);
                    client.Disconnect(true);
                }
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}

