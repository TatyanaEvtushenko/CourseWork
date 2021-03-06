﻿using System;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;

namespace CourseWork.BusinessLogicLayer.Services.Mappers.Implementations.MessageMappers
{
    public class MessageViewModelToMessageMapper : IMapper<MessageViewModel, Message>
    {
	    public Message ConvertTo(MessageViewModel item)
	    {
		    return new Message
		    {
				Id = Guid.NewGuid().ToString(),
			    IsSeen = false,
			    RecipientUserName = item.RecipientUserName,
			    Text = item.Text,
                ParameterString = item.ParameterString
		    };
	    }

	    public MessageViewModel ConvertFrom(Message item)
	    {
			return new MessageViewModel
			{
				RecipientUserName = item.RecipientUserName,
				Text = item.Text,
                ParameterString = item.ParameterString
			};
		}
    }
}
