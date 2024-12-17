using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.viewModels;
using Microsoft.Maui.Controls;
using ChatUser = ATLETIUM_T.Models.ChatUser;

namespace ATLETIUM_T.views;

[QueryProperty(nameof(ChatUser), "chatUser")]
public partial class ChatPage : ContentPage
{
    private ChatViewModel ChatViewModel;
    public ChatUser ChatUser { get; set; }
    
    public ChatPage()
    {
        InitializeComponent();
        ChatViewModel = this.BindingContext as ChatViewModel;
        ChatViewModel.Messages.CollectionChanged += OnMessagesCollectionChanged;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ChatViewModel.RecipientUserId = ChatUser.Id;
        ChatViewModel.DialogueId = ChatUser.ChatId;
        ChatViewModel.СompanionName = ChatUser.Fullname;
        ChatViewModel.LoadMessages();
    }

    void OnMessagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
        chatSurface.ScrollTo(chatSurface.GetItemHandle(ChatViewModel.Messages.Count - 1));
    }
    
    private void MessagesCollectionSizeChanged(object sender, EventArgs e) { 
        chatSurface.ScrollTo(chatSurface.GetItemHandleByVisibleIndex(chatSurface.VisibleItemCount - 1));
    }
}