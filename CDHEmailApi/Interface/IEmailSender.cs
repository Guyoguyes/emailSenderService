using System.Threading.Tasks;
using CDHEmailApi.Model;

namespace CDHEmailApi.Interface
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(EmailModel emailModel);
    }
}