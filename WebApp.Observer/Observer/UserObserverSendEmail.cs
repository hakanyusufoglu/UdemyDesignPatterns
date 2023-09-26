using BaseProject.Models;
using System.Net;
using System.Net.Mail;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser appUser)
        {
            //ToDo: Burada kesinlikle mail bilgilerinizi girmeniz bekleniyor. (https://ethereal.email/create)
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("smtp.ethereal.email");

            mailMessage.From = new MailAddress("laurianne.marvin@ethereal.email");
            mailMessage.To.Add(new MailAddress(appUser.Email));
            mailMessage.Subject = "Sistemimize hoş geldiniz";
            mailMessage.Body = "<p>Sistemizin genel kuralları : öğrenmek...</p>";
            mailMessage.IsBodyHtml = true;

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("laurianne.marvin@ethereal.email", "1rwQuMywrYEMHVwgRZ");
            smtpClient.Send(mailMessage);

            logger.LogInformation($"Email was send to user {appUser.UserName}");
        }
    }
}
