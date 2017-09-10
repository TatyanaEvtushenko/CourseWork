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
	    private readonly IMapper<MessageViewModel, Message> _mapper;

	    public MessageManager(Repository<Message> messageRepository, IMapper<MessageViewModel, Message> mapper)
	    {
		    _messageRepository = messageRepository;
		    _mapper = mapper;
	    }

	    public void Send(MessageViewModel[] messages)
	    {
		    _messageRepository.AddRange(messages.Select(message => _mapper.ConvertTo(message)).ToArray());
	    }

	    public void MarkAsRead(string[] id)
	    {
		    _messageRepository.RemoveRange(id);
	    }
    }
}
