using Microsoft.AspNetCore.SignalR;

namespace GHSMSystem.Hubs
{
    public class NameIdentifierUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("AccountID")?.Value;
        }
    }
}
