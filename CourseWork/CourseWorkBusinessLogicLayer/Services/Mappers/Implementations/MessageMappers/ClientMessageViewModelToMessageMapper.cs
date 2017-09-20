using System;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.MessageMappers
{
	public class ClientMessageViewModelToMessageMapper : IMapper<ClientMessageViewModel, Message>
	{
		public ClientMessageViewModel ConvertFrom(Message item)
		{
			return new ClientMessageViewModel
			{
				Id = item.Id,
				Text = item.Text
			};
		}

		public Message ConvertTo(ClientMessageViewModel item)
		{
			throw new NotImplementedException();
		}
	}
}
