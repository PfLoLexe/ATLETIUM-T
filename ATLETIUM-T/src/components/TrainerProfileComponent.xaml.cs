using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;
using ATLETIUM_T.views;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class TrainerProfileComponent : ContentView
{
    private Guid _trainerId;
    private bool _isMe = true;
    private ContactsComponent _contactsComponent;
    private UserController _userController = new UserController(new UserRepository());

    private LocalSettingsController _localSettingsController =
        new LocalSettingsController(new LocalSettingsRepository());
    
    public TrainerProfileComponent(Guid trainerId, bool isMe = true)
    {
        InitializeComponent();
        _trainerId = trainerId;
        _isMe = isMe;
    }

    public void LoadPageData()
    {
        if (_isMe)
        {
            LoadMyInfo();
            return;
        }
        
        LoadInfo();
    }

    private async void LoadMyInfo()
    {
        CurrentUserInfo user = await _userController.GetCurrentUserInfo();
        TrainerNameLabel.Text = $"{user.firstname} {user.lastname} {user.middle_name}";
        _contactsComponent = new ContactsComponent(user.phone_number);
        MainLayout.Children.Clear();
        MainLayout.Children.Add(new TrainInfoTabBar([_contactsComponent]));
    }

    private async void LoadInfo()
    {
        MainLabel.Text = "Профиль тренера";
        ExitButton.IsEnabled = false;
        BackButton.IsEnabled = true;
    }

    private async void ExitButton_OnClicked(object? sender, EventArgs e)
    {
        var result = await Shell.Current.DisplayAlert(title: $"Выход", message: $"Вы уверены что хотите выйти?",
            cancel: "Отмена", accept: "Да");
        if (!result)
            return;

        await _localSettingsController.DeleteToken();
        await Shell.Current.GoToAsync($"//{nameof(AuthorizationPage)}");
    }
}