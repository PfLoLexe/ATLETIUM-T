using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using ChatUser = ATLETIUM_T.Models.ChatUser;

namespace ATLETIUM_T.views;

public partial class ChatListPage : ContentPage
{
    private UsersChatCardsList _usersChatCardsList;
    private DialogueController _controller = new DialogueController(new DialogueRepository());
    public bool AddNewMode = false;
    private List<ChatUser> _ChatUsers { get; set; } = new();
    
    public ChatListPage()
    {
        InitializeComponent();
        LoadUsers();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadMainChats();
    }

    private void LoadUsers()
    {
        if (AddNewMode)
        {
            LoadNewChats();
            return;
        }
        
        LoadMainChats();
    }

    private async void LoadMainChats()
    {
        var usersList = await _controller.GetActiveDialogueListAsync();
        if (_ChatUsers != null) _ChatUsers.Clear();
        if (usersList == null) return;
        foreach (DialogueResponse dialogue in usersList)
        {
            _ChatUsers.Add(new ChatUser()
            {
                Id = dialogue.second_user_id,
                Firstname = dialogue.recipient_user_firstname,
                Lastname = dialogue.recipient_user_lastname,
                ChatId = dialogue.dialogue_id
            });
        }
        
        _usersChatCardsList = new UsersChatCardsList(_ChatUsers);
        MainLayout.Children.Add(_usersChatCardsList);
    }

    private async void LoadNewChats()
    {
        var usersList = await _controller.GetPossibleDialogueListAsync();
        if (_ChatUsers != null) _ChatUsers.Clear();
        if (usersList == null) return;
        foreach (PossibleDialogueResponse dialogue in usersList)
        {
            _ChatUsers.Add(new ChatUser()
            {
                Id = dialogue.recipient_user_id,
                Firstname = dialogue.recipient_user_firstname,
                Lastname = dialogue.recipient_user_lastname
            });
        }
        
        _usersChatCardsList = new UsersChatCardsList(_ChatUsers, true);
        MainLayout.Children.Add(_usersChatCardsList);
    }

    private void ChatListMainRefreshView_OnRefreshing(object? sender, EventArgs e)
    {
        MainLayout.Children.Clear();
        LoadUsers();
        ChatListMainRefreshView.IsRefreshing = false;
    }

    private void AddChatButton_OnClicked(object? sender, EventArgs e)
    {
        if (AddNewMode)
        {
            AddNewMode = false;
            AddChatButton.Source = "plus.png";
            PageName.Text = "Чаты";
            MainLayout.Children.Clear();
            LoadUsers();
            return;
        }
        
        AddNewMode = true;
        AddChatButton.Source = "x.png";
        PageName.Text = "Добавить чат";
        MainLayout.Children.Clear();
        LoadUsers();
    }

    public void SetDefaultPageState()
    {
        AddNewMode = false;
        AddChatButton.Source = "plus.png";
        PageName.Text = "Чаты";
        MainLayout.Children.Clear();
        LoadUsers();
    }
}