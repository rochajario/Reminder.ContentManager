using ContentManager.Api.Utils;
using ContentManager.Domain.Interfaces;
using ContentManager.Domain.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContentManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderManagementService)
        {
            _reminderService = reminderManagementService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userId = HttpContext.UserId();
            return Ok(_reminderService.GetReminders(userId));
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var userId = HttpContext.UserId();
            return Ok(_reminderService.GetReminderById(userId, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReminderRequest value)
        {
            value.UserId = HttpContext.UserId();
            var createdItemId = _reminderService.AddReminder(value);
            return Created($"/api/reminder/{createdItemId}", null);
        }

        [HttpPost("{reminderId}/notification")]
        public IActionResult Post(Guid reminderId, NotificationRequest notification)
        {
            var createdNotificationId = _reminderService.AddReminderNotification(reminderId, notification);
            return Created($"/api/notification/{createdNotificationId}", null);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ReminderRequest value)
        {
            value.UserId = HttpContext.UserId();
            _reminderService.UpdateReminder(id, value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _reminderService.RemoveReminder(id);
            return Ok();
        }
    }
}
