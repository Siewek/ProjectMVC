using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ProjectMVC.Data;
using MimeKit;

namespace ProjectMVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<LibraryTwoUser> _userManager;
        private readonly IEmailSender _sender;

        public RegisterConfirmationModel(UserManager<LibraryTwoUser> userManager, IEmailSender sender)
        {
            _userManager = userManager;
            _sender = sender;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
                var messsage = new MimeMessage();
                messsage.From.Add(MailboxAddress.Parse("PrzepisyKulinarne.com"));
                messsage.To.Add(MailboxAddress.Parse(Email));
                messsage.Subject = "Weryfikacja Konta";
                messsage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = EmailConfirmationUrl
                };
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                   // client.CheckCertificateRevocation = false;
                    client.Connect("smtp.gmail.com", 587, false);
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Authenticate("przepisykulinarneps6mo@gmail.com", "YX5ibhzvFPrvp#n");
                    client.Send(messsage);
                    client.Disconnect(true);
                }
            }

            return Page();
        }
    }
}
