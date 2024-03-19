using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("{notificationId}")]
        public IActionResult Get(Guid notificationId)
        {
            return Ok(_notificationService.GetNotificationById(notificationId));
        }

        [HttpPut("{notificationId}")]
        public IActionResult Put(Guid notificationId, [FromBody] NotificationRequest value)
        {
            _notificationService.UpdateNotification(notificationId, value);
            return Ok();
        }

        [HttpDelete("{notificationId}")]
        public IActionResult Delete(Guid notificationId)
        {
            _notificationService.RemoveNotification(notificationId);
            return Ok();
        }
    }
}
