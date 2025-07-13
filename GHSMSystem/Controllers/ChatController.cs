using AutoMapper;
using BusinessObject.Model;
using GHSMSystem.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.Request.Conversation;
using Service.RequestAndResponse.Response.Conversation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatController(
        IChatService chatService,
        IMapper mapper,
        IAccountRepository accountRepository,
        IHubContext<ChatHub> hubContext)
    {
        _chatService = chatService;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _hubContext = hubContext;
    }

    [HttpGet("messages/{conversationId}")]
    public async Task<IActionResult> GetMessages(int conversationId)
    {
        var messages = await _chatService.GetMessagesByConversationAsync(conversationId);
        var messageResponses = _mapper.Map<List<SimplifiedMessageResponse>>(messages);
        return Ok(messageResponses);
    }

    [HttpGet("conversations/by-customer/{customerId}")]
    public async Task<IActionResult> GetAllChatByCustomerId(string customerId)
    {
        try
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return BadRequest(new { message = "CustomerId is required." });
            }

            var conversations = await _chatService.GetConversationsByCustomerIdAsync(customerId);

            if (conversations == null || !conversations.Any())
            {
                return Ok(new List<GetConversationResponse>());
            }

            var conversationResponses = new List<GetConversationResponse>();

            foreach (var conversation in conversations)
            {
                var response = new GetConversationResponse
                {
                    ConversationID = conversation.ConversationID
                };

                var receiverId = conversation.User1ID == customerId ? conversation.User2ID : conversation.User1ID;
                var otherUser = await _accountRepository.GetByAccountIdAsync(receiverId);

                if (otherUser != null)
                {
                    response.OtherUser = _mapper.Map<SimplifiedAccountResponse>(otherUser);
                }

                var messages = await _chatService.GetMessagesByConversationAsync(conversation.ConversationID);
                var lastMessage = messages.OrderByDescending(m => m.SentAt).FirstOrDefault();
                if (lastMessage != null)
                {
                    response.LastMessage = _mapper.Map<SimplifiedMessageResponse>(lastMessage);
                }

                conversationResponses.Add(response);
            }

            return Ok(conversationResponses);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("messages/by-customer-receiver")]
    public async Task<IActionResult> GetMessagesByCustomerAndReceiver([FromQuery] string customerId, [FromQuery] string receiverId)
    {
        try
        {
            var conversation = await _chatService.GetOrCreateConversationAsync(customerId, receiverId);
            if (conversation == null)
            {
                return BadRequest(new { message = "Conversation not found." });
            }

            var messages = await _chatService.GetMessagesByConversationAsync(conversation.ConversationID);
            Console.WriteLine($"Retrieved {messages.Count()} messages for ConversationID {conversation.ConversationID}");
            var sortedMessages = messages.OrderBy(m => m.SentAt).ToList();
            var messageResponses = _mapper.Map<List<SimplifiedMessageResponse>>(sortedMessages);
            return Ok(messageResponses);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("mark-as-read")]
    public async Task<IActionResult> MarkAllMessagesAsRead([FromBody] MarkAsReadRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _chatService.MarkAllMessagesAsReadAsync((int)request.ConversationId, request.SenderId);
            return Ok(new { message = "Messages marked as read." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromForm] SendMessageRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.ReceiverID) || string.IsNullOrEmpty(request.SenderID))
                return BadRequest(new { message = "ReceiverID và SenderID là bắt buộc." });

            // Lấy thông tin tài khoản gửi
            var senderAcc = await _accountRepository.GetByAccountIdAsync(request.SenderID);
            if (senderAcc == null)
                return BadRequest(new { message = "Không tìm thấy tài khoản Sender." });
            var senderName = senderAcc.UserName;

            // Lấy thông tin người nhận
            var receiverAcc = await _accountRepository.GetByAccountIdAsync(request.ReceiverID);
            if (receiverAcc == null) return BadRequest(new { message = "Không tìm thấy tài khoản Receiver." });
            var receiverName = receiverAcc.UserName;

            // Gửi tin nhắn với tên tự động
            var message = await _chatService.SendMessageAsync(
                request.SenderID,
                request.ReceiverID,
                request.Content,
                senderName,
                receiverName,
                request.Images
            );
            await _hubContext.Clients.Group(message.ReceiverID)
                .SendAsync("ReceiveMessage",
                    message.SenderID,
                    message.Content,
                    message.SentAt,
                    message.MessageID,
                    message.ConversationID,
                    senderName,
                    receiverName,
                    message.ReceiverID
                );

            return Ok(new { message = "Đã gửi tin nhắn.", data = _mapper.Map<SimplifiedMessageResponse>(message) });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("create-conversation")]
    public async Task<IActionResult> CreateConversation([FromBody] CreateConversationRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(request.SenderID) || string.IsNullOrEmpty(request.ReceiverID))
            {
                return BadRequest(new { message = "SenderID and ReceiverID are required." });
            }

            var conversation = await _chatService.GetOrCreateConversationAsync(
                request.SenderID,
                request.ReceiverID
            );

            var response = _mapper.Map<ConversationResponse>(conversation);

            return Ok(new { message = "Conversation created successfully.", data = response });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 