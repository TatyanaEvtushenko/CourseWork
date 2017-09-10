using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;

namespace CourseWork.BusinessLogicLayer.Services.MessageManagers
{
    public interface IMessageManager
    {
	    void Send(MessageViewModel[] message);

	    ClientMessageViewModel[] GetUnreadMessages(string username);

	    void MarkAsRead(string[] id);
    }
}
