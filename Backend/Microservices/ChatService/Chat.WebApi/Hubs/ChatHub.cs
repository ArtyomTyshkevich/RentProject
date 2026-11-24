using Microsoft.AspNetCore.SignalR;
using Chat.Application.Services;

namespace Chat.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly MessageService _messageService;

        public ChatHub(MessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task GetMessages() // Убрали возвращаемый тип, теперь это Task (void)
        {
            var messagesFromDb = await _messageService.GetAllMessagesAsync();
            var result = messagesFromDb.Cast<object>().ToList();

            result.Add(new
            {
                Text = "Это тестовое сообщение!",
                Sender = "System",
                Date = DateTime.UtcNow
            });

            // Отправляем ответ обратно ТОЛЬКО вызывающему клиенту
            await Clients.Caller.SendAsync("ReceiveMessages", result);
        }
    }
}