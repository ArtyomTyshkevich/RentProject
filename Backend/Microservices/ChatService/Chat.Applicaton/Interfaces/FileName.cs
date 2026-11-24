
namespace Chat.Applicaton.Interfaces
{
    public interface IChatNotificationService
    {
        Task NotifyMessageReceivedAsync(string chatRoomId, object message);
    }
}
