using System.IO;
using System.Linq;
using System.Net.Mail;
using LotusInn.Management.Model;

namespace LotusInn.Management.Services
{
    public class EmailService
    {
        public static string TemplateLocation { get; set; }

        public static string LoadTemplate(string templateName)
        {
            string filePath = Path.Combine(TemplateLocation, templateName);
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs);
                return reader.ReadToEnd();
            }
        }

        public static bool SendMail(MailInfo mailInfo)
        {
            using (var mail = new MailMessage())
            {
                //mail.From = new MailAddress(mailInfo.MailAccount.Username);
                mail.Subject = mailInfo.Subject;
                mail.IsBodyHtml = mailInfo.IsBodyHtml;
                mail.Priority = MailPriority.High;

                if (!string.IsNullOrEmpty(mailInfo.Body))
                    mail.Body = mailInfo.Body;

                if (mailInfo.AlternateView != null)
                    mail.AlternateViews.Add(mailInfo.AlternateView);

                if (mailInfo.To != null && mailInfo.To.Any())
                    foreach (var to in mailInfo.To)
                        mail.To.Add(to);

                if (mailInfo.Cc != null && mailInfo.Cc.Any())
                    foreach (var cc in mailInfo.Cc)
                        mail.CC.Add(cc);

                if (mailInfo.Bcc != null && mailInfo.Bcc.Any())
                    foreach (var bcc in mailInfo.Bcc)
                        mail.CC.Add(bcc);

                if (mailInfo.FileAttachmentInfos != null && mailInfo.FileAttachmentInfos.Any())
                    foreach (var fileAttachmentInfo in mailInfo.FileAttachmentInfos)
                        mail.Attachments.Add(new Attachment(fileAttachmentInfo.FileStream, fileAttachmentInfo.FileName));


                var smtp = new SmtpClient();
                smtp.Send(mail);
                return true;
            }
        }
    }
}
