using AutoMapper;
using BusinessObject.Model;
using CloudinaryDotNet;
using Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using Repository.IRepositories;
using Repository.Repositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Notifications;
using Service.RequestAndResponse.Response.Categories;
using Service.RequestAndResponse.Response.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Service.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly ICyclePredictionRepository _cyclePredictionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper,
            ICyclePredictionRepository cyclePredictionRepository, IAccountRepository accountRepository,
            IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _cyclePredictionRepository = cyclePredictionRepository;
            _accountRepository = accountRepository;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task<Notification> CreateNotificationAsync(string customerId, string title, string content, NotificationType type)
        {
            var notification = new Notification
            {
                CustomerID = customerId,
                Title = title,
                Content = content,
                Type = type,
                IsRead = false,
                CreatedAt = DateTime.Now
            };

            return await _notificationRepository.CreateNotificationAsync(notification);
        }

        public async Task<BaseResponse<IEnumerable<NotificationResponse>>> GetAllNotificationByAccountID(string accountID)
        {
            IEnumerable<Notification> notification = await _notificationRepository.GetAllNotificationByAccountID(accountID);
            if (notification == null)
            {
                return new BaseResponse<IEnumerable<NotificationResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var notifications = _mapper.Map<IEnumerable<NotificationResponse>>(notification);
            if (notifications == null)
            {
                return new BaseResponse<IEnumerable<NotificationResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<NotificationResponse>>("Get all notifications as base success",
                StatusCodeEnum.OK_200, notifications);
        }

        public async Task MarkAllNotificationsAsReadAsync(string accountId)
        {
            if (string.IsNullOrEmpty(accountId))
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            await _notificationRepository.MarkAllNotificationsAsReadAsync(accountId);
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.MarkNotificationAsReadAsync(notificationId);
            if (notification == null)
            {
                throw new Exception("Notification not found.");
            }
            return notification;
        }

        public async Task ProcessRemindersAsync()
        {
            var today = DateTime.Today;
            var predictions = await _cyclePredictionRepository.GetAllCycelPredictionAsync();

            if(predictions == null)
            {
                throw new ArgumentNullException("Cannot Find Any CyclePredictions");
            }

            bool hasAnyReminder = false; 

            foreach (var prediction in predictions)
            {
                bool hasReminder = false;
                var customerId = prediction.MenstrualCycle.Customer.Id;
                if (string.IsNullOrEmpty(customerId))
                {
                    continue;
                }


                if (today >= prediction.FertileStartDate && today <= prediction.FertileEndDate)
                {
                    await CreateNotificationAsync(customerId,
                        "Giai đoạn dễ thụ thai",
                        "Hôm nay bạn đang trong giai đoạn dễ thụ thai. Nếu không muốn thụ thai, vui lòng uống thuốc tránh thai trong giai đoạn này!",
                        NotificationType.FertileWindow);

                    _logger.LogInformation("Bắt đầu gửi thông báo cho user: {UserId}", customerId);
                    await _hubContext.Clients.User(customerId).SendAsync("Receive Notification", new NotificationRequest
                    {
                        Title = "Giai đoạn dễ thụ thai",
                        Message = "Hôm nay bạn đang trong giai đoạn dễ thụ thai. Nếu không muốn thụ thai, vui lòng uống thuốc tránh thai trong giai đoạn này!",
                        Type = (int)NotificationType.FertileWindow
                    });
                    _logger.LogInformation("Gửi thông báo thành công cho user: {UserId}", customerId);
                    hasReminder = true;
                }

                
                if (today == prediction.OvulationDate.AddDays(-1))
                {
                    await CreateNotificationAsync(customerId,
                        "Nhắc rụng trứng",
                        "Ngày mai là ngày rụng trứng dự đoán.",
                        NotificationType.Ovulation);

                    _logger.LogInformation("Bắt đầu gửi thông báo cho user: {UserId}", customerId);
                    await _hubContext.Clients.User(customerId).SendAsync("Receive Notification", new NotificationRequest
                    {
                        Title = "Nhắc rụng trứng",
                        Message = "Ngày mai là ngày rụng trứng dự đoán.",
                        Type = (int)NotificationType.Ovulation
                    });
                    _logger.LogInformation("Gửi thông báo thành công cho user: {UserId}", customerId);
                    hasReminder = true;
                }
                else if (today == prediction.OvulationDate)
                {
                    await CreateNotificationAsync(customerId,
                        "Hôm nay là ngày rụng trứng",
                        "Hôm nay là ngày rụng trứng dự đoán.",
                        NotificationType.Ovulation);

                    _logger.LogInformation("Bắt đầu gửi thông báo cho user: {UserId}", customerId);
                    await _hubContext.Clients.User(customerId).SendAsync("Receive Notification", new NotificationRequest
                    {
                        Title = "Hôm nay là ngày rụng trứng",
                        Message = "Hôm nay là ngày rụng trứng dự đoán.",
                        Type = (int)NotificationType.Ovulation
                    });
                    _logger.LogInformation("Gửi thông báo thành công cho user: {UserId}", customerId);
                    hasReminder = true;
                }

                var periodStart = prediction.NextPeriodStartDate;
                var periodEnd = prediction.NextPeriodStartDate.AddDays(prediction.MenstrualCycle.PeriodLength - 1);

                if (today >= periodStart && today <= periodEnd)
                {
                    await CreateNotificationAsync(customerId,
                        "Đang trong kỳ kinh nguyệt",
                        "Hôm nay là ngày trong kỳ kinh nguyệt dự đoán.",
                        NotificationType.MenstrualCycle);

                    _logger.LogInformation("Bắt đầu gửi thông báo cho user: {UserId}", customerId);
                    await _hubContext.Clients.User(customerId).SendAsync("Receive Notification", new NotificationRequest
                    {
                        Title = "Đang trong kỳ kinh nguyệt",
                        Message = "Hôm nay là ngày trong kỳ kinh nguyệt dự đoán.",
                        Type = (int)NotificationType.MenstrualCycle
                    });
                    _logger.LogInformation("Gửi thông báo thành công cho user: {UserId}", customerId);
                    hasReminder = true;
                }

                if (hasReminder)
                {
                    hasAnyReminder = true; 
                }
            }

            if (!hasAnyReminder)
            {
                throw new Exception("Không có nhắc nhở nào cần gửi hôm nay. Job sẽ dừng với trạng thái Fail.");
            }

        }
    }
}
