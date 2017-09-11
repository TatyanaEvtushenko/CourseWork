using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;

namespace CourseWork.BusinessLogicLayer.Services.MessageManagers
{
    public interface IMessageManager
    {
	    void Send(MessageViewModel[] messages);

        void SendAsAdmin(MessageViewModel[] messages);

	    ClientMessageViewModel[] GetUnreadMessages(string username);

	    void MarkAsRead(string[] id);
    }
}
