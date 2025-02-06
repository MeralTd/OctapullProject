using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Mailing;

public class MailKitMailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailKitMailService(IConfiguration configuration)
    {
        const string configurationSection = "MailSettings";
        _mailSettings =
            configuration.GetSection(configurationSection).Get<MailSettings>()
            ?? throw new NullReferenceException($"\"{configurationSection}\" section cannot found in configuration.");
    }

    public void SendMail(Mail mail)
    {
        try
        {
            if (mail.ToList == null || mail.ToList.Count < 1)
                return;
            EmailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp, string.Empty);
            smtp.Send(email);
            smtp.Disconnect(true);
            email.Dispose();
            smtp.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new NotImplementedException(ex.Message);
        }
    }

    public async Task SendEmailAsync(Mail mail)
    {
        try
        {
            if (mail.ToList == null || mail.ToList.Count < 1)
                return;

            var html = string.IsNullOrEmpty(mail.HtmlBody) ? mail.TextBody : mail.HtmlBody;
            EmailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp, html);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            email.Dispose();
            smtp.Dispose();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new NotImplementedException(ex.Message);
        }
    }


    private void EmailPrepare(Mail mail, out MimeMessage email, out SmtpClient smtp, string htmlBody)
    {
        try
        {
            email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));
            email.To.AddRange(mail.ToList);
            if (mail.CcList != null && mail.CcList.Any())
                email.Cc.AddRange(mail.CcList);
            if (mail.BccList != null && mail.BccList.Any())
                email.Bcc.AddRange(mail.BccList);

            email.Subject = mail.Subject;
            if (mail.UnsubscribeLink != null)
                email.Headers.Add(field: "List-Unsubscribe", value: $"<{mail.UnsubscribeLink}>");
            BodyBuilder bodyBuilder = new() { TextBody = mail.TextBody, HtmlBody = htmlBody };

            if (mail.Attachments != null)
                foreach (MimeEntity? attachment in mail.Attachments)
                    if (attachment != null)
                        bodyBuilder.Attachments.Add(attachment);

            email.Body = bodyBuilder.ToMessageBody();
            email.Prepare(EncodingConstraint.SevenBit);


            smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Server, _mailSettings.Port, false);
            if (_mailSettings.AuthenticationRequired)
                smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new NotImplementedException(ex.Message);
        }
    }

}
