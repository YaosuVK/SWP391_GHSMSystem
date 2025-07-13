using AutoMapper;
using BusinessObject.Model;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Conversation;
using Service.RequestAndResponse.Response.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserManager<BusinessObject.Model.Account> _userManager;
        private readonly Cloudinary _cloudinary;

        public ChatService(
            IChatRepository chatRepository,
            UserManager<BusinessObject.Model.Account> userManager,
            Cloudinary cloudinary)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
            _cloudinary = cloudinary;
        }

        public async Task<BaseResponse<IEnumerable<Conversation>>> GetConversationsByUserAsync(string userId)
        {
            var conversations = await _chatRepository.GetConversationsByUserAsync(userId);
            if (conversations == null || !conversations.Any())
            {
                return new BaseResponse<IEnumerable<Conversation>>("No conversations found", StatusCodeEnum.NotFound_404, null);
            }
            return new BaseResponse<IEnumerable<Conversation>>("Conversations fetched successfully", StatusCodeEnum.OK_200, conversations);
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationAsync(int conversationId)
        {
            var messages = await _chatRepository.GetMessagesByConversationAsync(conversationId);
            return messages;
        }

        public async Task MarkAllMessagesAsReadAsync(int conversationId, string readerId)
        {
            await _chatRepository.MarkAllMessagesAsReadAsync(conversationId, readerId);
        }

        public async Task<Message> SendMessageAsync(string senderId, string receiverId, string content, string senderName, string receiverName, List<IFormFile> images)
        {
            var conversation = await _chatRepository.GetOrCreateConversationAsync(senderId, receiverId);
            if (conversation == null)
            {
                throw new Exception("Could not create or retrieve conversation.");
            }

            var message = new Message
            {
                ConversationID = conversation.ConversationID,
                SenderID = senderId,
                ReceiverID = receiverId,
                Content = content,
                senderName = senderName,
                receiverName = receiverName,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };


            var addedMessage = await _chatRepository.AddMessageAsync(message);

            // Update LastMessageAt in conversation
            conversation.LastMessageAt = DateTime.UtcNow;
            await _chatRepository.UpdateAsync(conversation);

            return addedMessage;
        }

        public async Task<Conversation> GetOrCreateConversationAsync(string user1Id, string user2Id)
        {
            return await _chatRepository.GetOrCreateConversationAsync(user1Id, user2Id);
        }

        public async Task<IEnumerable<Conversation>> GetConversationsByCustomerIdAsync(string customerId)
        {
            return await _chatRepository.GetConversationsByCustomerIdAsync(customerId);
        }

        public async Task<int> GetUnreadMessageCountAsync(int conversationId, string userId)
        {
            var messages = await _chatRepository.GetMessagesByConversationAsync(conversationId);
            return messages.Count(m => m.ReceiverID == userId && !m.IsRead);
        }
    }
} 