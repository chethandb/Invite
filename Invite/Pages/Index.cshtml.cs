using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using System.ComponentModel.DataAnnotations;

namespace Invite.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            
        }
        [BindProperty]
        public EmailRSVPFormModel EmailRSVP { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // create and send the mail here
            var mailbody = $@"RSVP by,

            Name: {EmailRSVP.Name}            
            Email: {EmailRSVP.Email}
            Message: ""{EmailRSVP.Message}""

            Cheers,
            Chethan
            ";

            SendMail(mailbody);

            return RedirectToPage("Index");
        }

        private void SendMail(string mailbody)
        {           
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RSVPbyEmail", EmailRSVP.Email));
            message.To.Add(new MailboxAddress("RSVPtoEmail", "cheresdb@gmail.com"));
            message.Subject = "Marriage RSVP";
            message.Body = new TextPart("plain")
            {
                Text = mailbody
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("ching.2803@gmail.com", "Ching123456");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }

    public class EmailRSVPFormModel
    {
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
