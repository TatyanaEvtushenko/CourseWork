using System.Collections.Immutable;
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
	    private readonly IMapper<MessageViewModel, Message> _serverMapper;
	    private readonly IMapper<ClientMessageViewModel, Message> _clientMapper;

		public MessageManager(Repository<Message> messageRepository, IMapper<MessageViewModel, Message> serverMapper, IMapper<ClientMessageViewModel, Message> clientMapper)
	    {
		    _messageRepository = messageRepository;
		    _serverMapper = serverMapper;
		    _clientMapper = clientMapper;
	    }

	    public void Send(MessageViewModel[] messages)
	    {
			_messageRepository.AddRange(messages.Select(message => _serverMapper.ConvertTo(message)).ToArray());
	    }

        public void SendAsAdmin(MessageViewModel[] messages)
        {
            Send(messages);
        }

        public ClientMessageViewModel[] GetUnreadMessages(string username)
	    {
		    return _messageRepository.GetWhere(item => item.RecipientUserName.Equals(username) && !item.IsSeen).
				Select(item => _clientMapper.ConvertFrom(item)).ToArray();
	    }

	    public void MarkAsRead(string[] id)
	    {
		    var idSet = id.ToImmutableHashSet();
		    var markedMessages = _messageRepository.GetWhere(message => idSet.Contains(message.Id)).Select(message =>
			    {
				    message.IsSeen = true;
				    return message;
			    }).ToArray();
		    _messageRepository.UpdateRange(markedMessages);
	    }
    }
}
