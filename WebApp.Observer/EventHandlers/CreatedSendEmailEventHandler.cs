using MediatR;
using System.Net.Mail;
using System.Net;
using WebApp.Observer.Events;
using WebApp.Observer.Observer;

namespace WebApp.Observer.EventHandlers
{
    public class CreatedSendEmailEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<CreatedSendEmailEventHandler> _logger;

        public CreatedSendEmailEventHandler(ILogger<CreatedSendEmailEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //ToDo: Burada kesinlikle mail bilgilerinizi girmeniz bekleniyor. (https://ethereal.email/create)
          
            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("smtp.ethereal.email");

            mailMessage.From = new MailAddress("laurianne.marvin@ethereal.email");
            mailMessage.To.Add(new MailAddress(notification.AppUser.Email));
            mailMessage.Subject = "Sistemimize hoş geldiniz";
            mailMessage.Body = "<p>Sistemizin genel kuralları : öğrenmek...</p>";
            mailMessage.IsBodyHtml = true;

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("laurianne.marvin@ethereal.email", "1rwQuMywrYEMHVwgRZ");
            smtpClient.Send(mailMessage);

            _logger.LogInformation($"Email was send to user {notification.AppUser.UserName}");

            return Task.CompletedTask;
        }
    }
}
