using System.Linq;
using CourseWork.BusinessLogicLayer.Services.Mappers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;
using CourseWork.DataLayer.Repositories;

namespace CourseWork.BusinessLogicLayer.Services.MessageManagers.Implementations
{
    public class MessageManager : IMessageManager
    {
	    private readonly Repository<Message> _messageRepository;
        private readonly Repository<ProjectSubscriber> _projectSubscribeRepository;
	    private readonly IMapper<MessageViewModel, Message> _serverMapper;
	    private readonly IMapper<ClientMessageViewModel, Message> _clientMapper;

        public MessageManager(Repository<Message> messageRepository, IMapper<MessageViewModel, Message> serverMapper,
            IMapper<ClientMessageViewModel, Message> clientMapper,
            Repository<ProjectSubscriber> projectSubscribeRepository)
        {
            _messageRepository = messageRepository;
            _serverMapper = serverMapper;
            _clientMapper = clientMapper;
            _projectSubscribeRepository = projectSubscribeRepository;
        }

        public void Send(MessageViewModel[] messages)
	    {
	        var messageModels = messages.Select(message => _serverMapper.ConvertTo(message));
			_messageRepository.AddRange(messageModels.ToArray());
	    }

        public void SendAsAdmin(MessageViewModel[] messages)
        {
            Send(messages);
        }

        public void NotifySubscribers(SubscriberNotificationViewModel model)
        {
            var subscribers = _projectSubscribeRepository.GetWhere(n => n.ProjectId.Equals(model.Id));
            var notifications = subscribers.Select(n => GetMessageForSubscribers(n, model.Text));
            Send(notifications.ToArray());
        }

        public ClientMessageViewModel[] GetUnreadMessages(string username)
        {
            var messages = _messageRepository.GetWhere(item => item.RecipientUserName.Equals(username) && !item.IsSeen);
		    return messages.Select(item => _clientMapper.ConvertFrom(item)).ToArray();
	    }

	    public void MarkAsRead(string[] ids)
	    {
	        var messages = _messageRepository.GetWhere(message => ids.Contains(message.Id));
		    var markedMessages = messages.Select(ReadMessage).ToArray();
		    _messageRepository.UpdateRange(markedMessages);
	    }

        private MessageViewModel GetMessageForSubscribers(ProjectSubscriber subscriber, string text)
        {
            return new MessageViewModel
            {
                RecipientUserName = subscriber.UserName,
                Text = text
            };
        }

        private Message ReadMessage(Message message)
        {
            message.IsSeen = true;
            return message;
        }
    }
}
