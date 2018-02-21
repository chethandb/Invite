using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.ComponentModel.DataAnnotations;

namespace Invite.Pages
{
    public class IndexKannadaModel : PageModel
    {
        public void OnGet()
        {

        }
        [BindProperty]
        public KannadaEmailRSVPFormModel KannadaEmailRSVP { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // create and send the mail here
            var mailbody = $@"Kannada RSVP by,            

            Name: {KannadaEmailRSVP.Name}           
            Email: {KannadaEmailRSVP.Email}
            Message: ""{KannadaEmailRSVP.Message}""

            Cheers,
            Chethan
           ";

            SendMail(mailbody);

            return RedirectToPage("Index");
        }

        private void SendMail(string mailbody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RSVPbyEmail", KannadaEmailRSVP.Email));
            message.To.Add(new MailboxAddress("RSVPtoEmail", "cheresdb@gmail.com"));
            message.Subject = "Kannada marriage RSVP";
            message.Body = new TextPart("plain")
            {
                Text = mailbody
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("cheresdb.2803@gmail.com", "uhgrpsmjjnjbnkkc");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }

    public class KannadaEmailRSVPFormModel
    {
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}

