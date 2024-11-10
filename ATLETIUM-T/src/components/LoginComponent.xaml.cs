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

namespace ATLETIUM_T.components;

public partial class LoginComponent : ContentView
{
    private readonly LocalSettingsController _controller = new LocalSettingsController(new LocalSettingsRepository());
    
    public LoginComponent()
    {
        InitializeComponent();
    }
    
    private async void LoginBtn_OnClicked(object? sender, EventArgs e)
    {
        Token? token = await new AuthorizationController(new AuthorizationRepository())
            .AuthorizeAsync(LoginEntry.Text, PasswordEntry.Text);

        if (token != null)
        {
            await _controller.SaveToken(token.access_token, token.token_type);
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");           
        }
    }
}