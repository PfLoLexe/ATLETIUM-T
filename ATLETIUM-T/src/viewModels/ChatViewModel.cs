using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ATLETIUM_T.viewModels;

public partial class ChatViewModel : ObservableObject
{
    
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
    string editMessageText;

    public ChatUser Me { get; set; } = new ChatUser() { Name = "Me" };
    public ChatUser Сompanion { get; set; } = new ChatUser() { Name = "test test" };
    public ObservableCollection<Message> Messages { get; set; }
    public ObservableCollection<SuggestedAction> SuggestedActions { get; set; }

    Random r = new Random();

    #region WebSocket
    
    private readonly WebSocketService _webSocketService;
    
    public ObservableCollection<Message> WebSocketMessages { get; set; } = new ObservableCollection<Message>();
    
    private string _newMessage;
    public string NewMessage
    {
        get => _newMessage;
        set {
             _newMessage = value;
             OnPropertyChanged();
        }
    }
    
     // public ICommand SendMessageCommand { get; }
    
    
    
    #endregion
     
    private readonly UserController _userController = new UserController(new UserRepository());
    private readonly MessageController _messageController = new MessageController(new MessageRepository());
    public Guid RecipientUserId { get; set; }
    public Guid? DialogueId { get; set; }
    public string СompanionName { get; set; }
     
    public ChatViewModel()
    {
        
        _webSocketService = new WebSocketService();
        _webSocketService.MessageReceived += OnMessageReceived;

        Task.Run(async () => await _webSocketService.ConnectAsync($"ws://10.0.2.2:8000/v1/websocket/chat-connection/{await _userController.GetCurrentUserGuid()}"));
        
        Messages = new ObservableCollection<Message>() { };
    }

    public async void LoadMessages()
    {
        Сompanion = new ChatUser()
        {
            Name = СompanionName
        };
        
        var messages = await _messageController.GetMessagesListAsync(DialogueId);
        if (messages == null) return;

        foreach (MessageResponse message in messages)
        {
            Messages.Add(new Message()
            {
                Text = message.text,
                SentAt = message.send_date,
                Sender = message.my_message == true ? Me : Сompanion,
                IsLastMessage = true
            });
        }
    }
    
    private async void OnMessageReceived(string message)
    {
        // MessageResponse deserializedMessage = await Deserialize(message);
        //
        // // Добавляем сообщение от других пользователей
        // Messages.Add(new Message { Text = $"{deserializedMessage.text}", SentAt = deserializedMessage.send_date,
        //     Sender = Сompanion, IsLastMessage = true });
        
        Messages.Add(new Message { Text = $"{message}", SentAt = DateTime.Now,
            Sender = Сompanion, IsLastMessage = true });
    }
    
    private async Task SendMessageWebSocket(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
    
        // Отправляем сообщение через WebSocket
        // await _webSocketService.SendMessageAsync(message);

        if (DialogueId == null || RecipientUserId == null)
            return;

        MessageRequest messageRequest = new MessageRequest
        {
            dialogue_id = DialogueId,
            recipient_user_id = RecipientUserId,
            send_date = DateTime.Now,
            text = message
        };
        
        await _messageController.SendMessage(messageRequest);
    
        // Добавляем свое сообщение в список
        Messages.Add(new Message { Text = $"{message}", SentAt = DateTime.Now,
            Sender = Me, IsLastMessage = true });
    
        NewMessage = string.Empty;
    }

    public async void SubmitMessage(object message)
    {
        if (message is string messageText)
        {
            await SendMessageWebSocket(messageText);
            EditMessageText = string.Empty;
        }
        else if (message is Message messageObj)
        {
            messageObj.SentAt = DateTime.Now;
            Messages.Add(messageObj);
        }
    }

    [RelayCommand(CanExecute = nameof(CanSendMessage))]
    void SendMessage(object parameter)
    {
        if (parameter is SuggestedAction action)
        {
            SubmitMessage(action.Message);
            return;
        }

        SubmitMessage(EditMessageText);
    }

    bool CanSendMessage(object message)
    {
        if (message is string messageText)
            return !string.IsNullOrEmpty(messageText) && !string.IsNullOrWhiteSpace(messageText);
        return message != null;
    }
    
    // public async Task<MessageResponse> Deserialize(string message)
    // {
    //     var result = JsonSerializer.Deserialize<MessageResponse>(message);
    //     return result;
    // }
}

public class Message {
    public Guid Id { get; } = Guid.NewGuid();
    public ChatUser Sender { get; set; }
    public DateTime? SentAt { get; set; }
    public string Text { get; set; }
    public bool IsLastMessage { get; set; }
}

public class ChatUser {
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Initials => String.Concat(Name.AsSpan(0, 1), Name.Split(null)[1].AsSpan(0, 1));
}

public class SuggestedAction {
    public Message Message { get; set; }
    public string Text { get; set; }
}
