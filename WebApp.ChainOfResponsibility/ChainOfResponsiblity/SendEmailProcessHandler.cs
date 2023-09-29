using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfResponsiblity
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }
        //object o = zip dosyası geliyor.
        public override object Handle(object o)
        {

            var zipMemoryStream = o as MemoryStream;
            //stream'i sıfırdan itibaren al.

            zipMemoryStream.Position = 0;
            //ToDo: Burada kesinlikle mail bilgilerinizi girmeniz bekleniyor. (https://ethereal.email/create)

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("smtp.ethereal.email");

            mailMessage.From = new MailAddress("laurianne.marvin@ethereal.email");
            mailMessage.To.Add(new MailAddress(_toEmail));
            mailMessage.Subject = "Zip dosyası";
            mailMessage.Body = "<p>Zip dosyası ektedir.</p>";
            mailMessage.IsBodyHtml = true;

            Attachment attachment = new Attachment(o as MemoryStream, _fileName, MediaTypeNames.Application.Zip);
            mailMessage.Attachments.Add(attachment);

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("laurianne.marvin@ethereal.email", "1rwQuMywrYEMHVwgRZ");
            smtpClient.Send(mailMessage);

            //zinciri bir sonraki halkası olmadığından nulll ekleyebilirim
            return base.Handle(null);
        }
    }
}
