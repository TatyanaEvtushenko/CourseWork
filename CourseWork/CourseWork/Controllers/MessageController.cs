using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class MessageController : Controller
    {
	    private readonly IMessageManager _messageManager;

	    public MessageController(IMessageManager messageManager)
	    {
		    _messageManager = messageManager;
	    }

	    [HttpGet]
	    [Route("api/Message/Send")]
	    public void Send([FromQuery] MessageViewModel[] messages)
	    {
		    _messageManager.Send(messages);
	    }

	    [HttpGet]
	    [Route("api/Message/MarkAsRead")]
	    public void MarkAsRead([FromQuery] string[] id)
	    {
		    _messageManager.MarkAsRead(id);
	    }
	}
}