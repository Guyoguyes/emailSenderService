using System;
using System.Net;
using System.Threading.Tasks;
using CDHEmailApi.Interface;
using CDHEmailApi.Model;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CDHEmailApi.Service
{
    public class EmailSenderService: IEmailSender
    {
        private readonly SmtpSetting _smtpSetting;

        public EmailSenderService(IOptions<SmtpSetting> smtpSetting)
        {
            _smtpSetting = smtpSetting.Value;
        }

        public async Task<string> SendEmailAsync(EmailModel emailModel)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpSetting.SenderEmail));
            message.To.Add(MailboxAddress.Parse(emailModel.emailAddress));
            message.Subject = emailModel.subject;
            // message.Body = emailModel.message;
            
            message.Body = new TextPart("plain")
            {
                Text = emailModel.message
            };
            var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSetting.Server, _smtpSetting.Port, true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSetting.SenderEmail, _smtpSetting.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return "Email Sent Successfully";
            }
            catch (Exception e)
            {
                throw e;
                Console.WriteLine(e);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}