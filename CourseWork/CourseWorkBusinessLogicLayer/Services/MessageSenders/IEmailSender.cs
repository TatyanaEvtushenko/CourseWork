using System.Threading.Tasks;

namespace CourseWork.BusinessLogicLayer.Services.MessageSenders
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
