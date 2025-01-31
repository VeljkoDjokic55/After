using AFTER.BLL.Services.Interfaces;
using AFTER.Shared.Common;
using AFTER.Shared.Constants;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AFTER.BLL.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailConfiguration> _emailConfiguration;

        public EmailService(IOptions<EmailConfiguration> emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task<ResponsePackageNoData> SendMailAsync(EmailData emailData)
        {
            MailMessage msg = new MailMessage();
            try
            {
                msg.To.Add(new MailAddress(emailData.To));
                foreach (var ccItem in emailData.CcList)
                {
                    msg.CC.Add(new MailAddress(ccItem));
                }

                // Subject and multipart/alternative Body
                msg.Subject = emailData.Subject;

                if (emailData.IsContentHtml)
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(emailData.Content, null, MediaTypeNames.Text.Html));
                else
                    msg.Body = emailData.Content;

                // Init SmtpClient and send

                SmtpClient smtpClient = null;

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    msg.From = new MailAddress(_emailConfiguration.Value.Email);
                    smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(_emailConfiguration.Value.Email, _emailConfiguration.Value.Password)
                    };
                }
                else
                {
                    msg.From = new MailAddress(_emailConfiguration.Value.From);
                    smtpClient = new SmtpClient(_emailConfiguration.Value.Host, _emailConfiguration.Value.Port);
                    smtpClient.Credentials = new NetworkCredential(_emailConfiguration.Value.Username, _emailConfiguration.Value.ApiKey);
                }

                await smtpClient.SendMailAsync(msg);
            }
            catch (Exception ex)
            {
                return new ResponsePackageNoData() { Status = ResponseStatus.InternalServerError, Message = "An error occured while sending email." };
            }

            return new ResponsePackageNoData();
        }
    }
}
