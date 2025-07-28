using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Hubs;
using Service.IService;
using Service.RequestAndResponse.Request.Notifications;
using Service.RequestAndResponse.Response.Notifications;

namespace GHSMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public NotificationController(
            INotificationService notificationService,
            IMapper mapper,
            IHubContext<NotificationHub> notificationHub)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _notificationHub = notificationHub;
        }

       /* [Authorize(Roles = "Customer")]*/
        [HttpGet("by-account/{accountId}")]
        public async Task<IActionResult> GetNotificationsByAccountId(string accountId)
        {
            try
            {
                if (string.IsNullOrEmpty(accountId))
                {
                    return BadRequest(new { message = "AccountId is required." });
                }

                var notifications = await _notificationService.GetAllNotificationByAccountID(accountId);
                var response = _mapper.Map<IEnumerable<NotificationResponse>>(notifications.Data);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        /*[Authorize(Roles = "Customer")]*/
        [HttpPut("mark-as-read")]
        public async Task<IActionResult> MarkNotificationAsRead([FromBody] MarkNotificationAsReadRequest request)
        {
            try
            {
                var notification = await _notificationService.MarkNotificationAsReadAsync(request.NotificationID);
                var response = _mapper.Map<NotificationResponse>(notification);

                return Ok(new { message = "Notification marked as read.", data = response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /*[Authorize(Roles = "Customer")]*/
        [HttpPut("mark-all-as-read")]
        public async Task<IActionResult> MarkAllNotificationsAsRead([FromBody] MarkAllNotificationsAsReadRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.AccountID))
                {
                    return BadRequest(new { message = "AccountId is required." });
                }

                await _notificationService.MarkAllNotificationsAsReadAsync(request.AccountID);
                return Ok(new { message = "All notifications marked as read." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
