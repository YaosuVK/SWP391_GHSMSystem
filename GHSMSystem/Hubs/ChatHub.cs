
using Microsoft.AspNetCore.SignalR;

namespace GHSMSystem.Hubs
{
    public class ChatHub : Hub
    {
        // This method is called by the client to send a message.
        // The parameters should align with what the client sends.
        public async Task SendMessageToGroup(string receiverId, string senderId, string content, DateTime sentAt, int messageId, int conversationId, string senderName, string receiverName)
        {
            // Send the message to the group associated with the receiverId.
            // The client listening to this group will receive the message.
            await Clients.Group(receiverId).SendAsync("ReceiveMessage", senderId, content, sentAt, messageId, conversationId, senderName, receiverName);
        }

        // This method allows a user to join a specific group (e.g., their own user ID group).
        public async Task JoinGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        // This method allows a user to leave a specific group.
        public async Task LeaveGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        // This method can be used to send a notification to a specific user (e.g., when a message is marked as read)
        public async Task SendNotificationToUser(string userId, string notificationMessage)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", notificationMessage);
        }
    }
} 