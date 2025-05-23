﻿using System;
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

public partial class PinCodeComponent : ContentView
{
    private readonly AuthorizationController _controller = new AuthorizationController(new AuthorizationRepository());

    private readonly LocalSettingsController _localController =
        new LocalSettingsController(new LocalSettingsRepository());
    
    private LocalSettingsController _localSettingsController =
        new LocalSettingsController(new LocalSettingsRepository());

    public event EventHandler<string> DataSent;
    
    public event EventHandler<bool> ChangeComponent;
    
    public PinCodeComponent()
    {
        InitializeComponent();
    }
    
    private async void LoginBtn_OnClicked(object? sender, EventArgs e)
    {
        if (!CheckPinCode())
        {
            new ToastMessage().ShortToast("Не верная длина пин-кода");
            return;
        }
            

        PinCodeResponse? pinCodeStatus = await _controller.AuthorizeByPinCodeAsync(PinView.PINValue);
        if (pinCodeStatus == null) return;
        if (pinCodeStatus.detail == "Unauthorized pincode")
        {
            new ToastMessage().ShortToast("Не верный пин-код");
            return;
        }

        if (pinCodeStatus.detail == "Unauthorized")
        {
            await _localController.SaveToken(null, null);
            string dataToSend = "Unauthorized";
            DataSent?.Invoke(this, dataToSend);
            return;
        }
        
        new ToastMessage().ShortToast("Успешная авторизация");
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        
    }

    private bool CheckPinCode()
    {
        if (PinView.PINValue == null) return false;
        if (PinView.PINLength != 6) return false;
        return true;
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        await _localSettingsController.DeleteToken();
        ChangeComponent?.Invoke(this, true);
    }
}