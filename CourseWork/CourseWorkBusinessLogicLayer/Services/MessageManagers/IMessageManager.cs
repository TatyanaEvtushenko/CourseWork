using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;

namespace CourseWork.BusinessLogicLayer.Services.MessageManagers
{
    public interface IMessageManager
    {
	    void Send(MessageViewModel[] message);

	    void MarkAsRead(string[] id);
    }
}
