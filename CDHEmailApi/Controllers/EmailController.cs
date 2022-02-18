using System;
using System.Threading.Tasks;
using CDHEmailApi.Interface;
using CDHEmailApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CDHEmailApi.Controllers
{
    public class EmailController : Controller
    {
        private IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost, Route("email")]
        public async Task<IActionResult> SendEmailAsync(EmailModel emailModel)
        {
            try
            {
                string messageStatus = await _emailSender.SendEmailAsync(emailModel);
                return Ok(messageStatus);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                 throw e;
            }
        }
    }
}