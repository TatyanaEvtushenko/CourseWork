using System.Linq;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class MessageController : Controller
    {
	    private readonly IMessageManager _messageManager;
	    private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<LocalizationController> _localizer;

		public MessageController(IMessageManager messageManager, UserManager<ApplicationUser> userManager, IStringLocalizer<LocalizationController> localizer)
	    {
		    _messageManager = messageManager;
		    _userManager = userManager;
	        _localizer = localizer;
	    }

        [HttpPost]
        [Route("api/Message/NotifySubscribers")]
        [Authorize(Roles = "Admin, ConfirmedUser")]
        public void NotifySubscribers([FromBody] SubscriberNotificationViewModel model)
        {
            _messageManager.NotifySubscribers(model);
        }

        [HttpPost]
        [Route("api/Message/SendAsAdmin")]
        [Authorize(Roles = "Admin")]
        public void SendAsAdmin([FromBody] string[] usernames)
        {
            var username = _userManager.GetUserName(HttpContext.User);
            _messageManager.Send(new[] { 
                new MessageViewModel
                {
                    Text = usernames.Length <= 3 ? "ADMINPAGELINK": "ADMINPAGELINKMANY",
                    RecipientUserName = username,
                    ParameterString = GenerateNotification(usernames)
                } 
            });
        }

        private string GenerateNotification(string[] usernames)
        {
            var text = usernames[0];
            if (usernames.Length == 1) return text;
            for (int i = 1; i < usernames.Length; i++)
            {
                if (i >= 3) break;
                text += ",<br>" + usernames[i];
            }
            if (usernames.Length <= 3) return text;
            return text + "*" + (usernames.Length - 3);
        }

	    [HttpPost]
	    [Route("api/Message/Send")]
        [Authorize]
	    public void Send([FromBody] MessageViewModel[] messages)
	    {
		    _messageManager.Send(messages);
	    }

	    [HttpGet]
	    [Route("api/Message/MarkAsRead")]
	    [Authorize]
        public void MarkAsRead([FromQuery] string[] id)
	    {
		    _messageManager.MarkAsRead(id);
	    }

	    [HttpGet]
	    [Route("api/Message/GetUnreadMessages")]
		[Authorize]
	    public ClientMessageViewModel[] GetUnreadMessages()
	    {
		    var message = _messageManager.GetUnreadMessages(_userManager.GetUserName(HttpContext.User));
	        return message.Select(item => new ClientMessageViewModel
	        {
	            Id = item.Id,
	            Text = item.ParameterString == null ? _localizer[item.Text] : string.Format(_localizer[item.Text],
                    item.ParameterString.Split(new char[] {'*'}).Select(s => _localizer[s] ?? s).ToArray())
	        }).ToArray();
	    }
	}
}