using System.Linq;
using CourseWork.BusinessLogicLayer.Services.MessageManagers;
using CourseWork.BusinessLogicLayer.ViewModels.MessageViewModels;
using CourseWork.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CourseWork.Controllers
{
    [Produces("application/json")]
    public class MessageController : Controller
    {
	    private readonly IMessageManager _messageManager;
	    private readonly UserManager<ApplicationUser> _userManager;

		public MessageController(IMessageManager messageManager, UserManager<ApplicationUser> userManager)
	    {
		    _messageManager = messageManager;
		    _userManager = userManager;
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
                new MessageViewModel{ Text = GenerateNotification(usernames), RecipientUserName = username } }
            );
        }

        private string GenerateNotification(string[] usernames)
        {
            var text = "<a href=\"/AdminPage?confirmed=false&unconfirmed=false&requested=true\">";
            var ending = "<br> " + " have requested account confirmation" + ".</a>";
            text += usernames[0];
            if (usernames.Length == 1) return text + ending;
            for (int i = 1; i < usernames.Length; i++)
            {
                if (i >= 3) break;
                text += ",<br>" + usernames[i];
            }
            
            if (usernames.Length <= 3) return text + ending;
            return text + " and " + "<br>" + (usernames.Length - 3) + " other users" + ending;
        }

	    [HttpPost]
	    [Route("api/Message/Send")]
	    public void Send([FromBody] MessageViewModel[] messages)
	    {
		    _messageManager.Send(messages);
	    }

	    [HttpGet]
	    [Route("api/Message/MarkAsRead")]
	    public void MarkAsRead([FromQuery] string[] id)
	    {
		    _messageManager.MarkAsRead(id);
	    }

	    [HttpGet]
	    [Route("api/Message/GetUnreadMessages")]
		[Authorize]
	    public ClientMessageViewModel[] GetUnreadMessages()
	    {
		    return _messageManager.GetUnreadMessages(_userManager.GetUserName(HttpContext.User));
            
	    }
	}
}