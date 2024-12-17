using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.Models;
using ATLETIUM_T.views;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class UsersChatCardsList : ContentView
{
    private ObservableCollection<ChatUser> _ChatUsers { get; set; } = new();
    private bool _isForAdding = false;
    private DialogueController _dialogueController = new DialogueController(new DialogueRepository());
    

    
    public UsersChatCardsList(List<ChatUser> chatUsersList, bool isForAdding = false)
    {
        InitializeComponent();
        _isForAdding = isForAdding;
        if (_ChatUsers != null) _ChatUsers.Clear();
        if (chatUsersList == null) return;
        
        foreach (ChatUser user in chatUsersList)
            _ChatUsers.Add(user);
        
        ChatsListView.ItemsSource = _ChatUsers;
    }

    private void ChatsListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        var chatUser = e.Item as ChatUser;
        if (_isForAdding)
        {
            TappedNewChat(chatUser);
            return;
        }
        
        TappedChat(chatUser);
    }

    private async void TappedChat(ChatUser chatUser)
    {
        await Shell.Current.GoToAsync($"{nameof(ChatListPage)}/{nameof(ChatPage)}",
            new Dictionary<string, object>
            {
                ["chatUser"] = chatUser
            });
    }
    
    private async void TappedNewChat(ChatUser chatUser)
    {
        var parent = GetParentPage();
        
        
        var newChatUser = await CreateChat(chatUser);
        if (newChatUser == null)
        {
            new ToastMessage().ShortToast("Не удалось создать диалог");
            return;
        }
        
        parent.SetDefaultPageState();
        await Shell.Current.GoToAsync($"{nameof(ChatListPage)}/{nameof(ChatPage)}",
            new Dictionary<string, object>
            {
                ["chatUser"] = chatUser
            });
    }

    private async Task<ChatUser?> CreateChat(ChatUser chatUser)
    {
        var dialogueId = await _dialogueController.AddDialogueAsync(chatUser.Id);
        chatUser.ChatId = dialogueId.dialogue_id;
        return chatUser;
    }
    
    private ChatListPage GetParentPage()
    {
        Element parent = this.Parent;
        while (parent != null && parent is not ContentPage)
        {
            parent = parent.Parent;
        }
        return parent as ChatListPage;
    }
}