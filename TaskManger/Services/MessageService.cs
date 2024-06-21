using TaskManger.Enums;

namespace TaskManger.Services;

public class MessageService
{
    public event Action<string, NotificationEnum>? OnShow;

    public void ShowNotification(string Message, NotificationEnum NotificationLevel)
    {
        OnShow?.Invoke(Message, NotificationLevel);
    }
}
