using System.Threading.Tasks;

namespace CourseWork.BusinessLogicLayer.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
