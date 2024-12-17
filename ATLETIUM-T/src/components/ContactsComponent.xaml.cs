using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ATLETIUM_T.components;

public partial class ContactsComponent : ContentView
{
    public ContactsComponent(string phoneNumber, bool isEditMode = true)
    {
        // editmode должен быть доступен только текущему пользователю приложения
        
        InitializeComponent();
        AutomationId = "Контакты";
        PhoneNumberEntry.Text = phoneNumber;
        CloseEntries();
        if (!isEditMode)
            NoEditMode();
    }

    private void NoEditMode()
    {
        EditPhoneNumberButton.IsVisible = false;
        EditTgButton.IsVisible = false;
    }

    private void EditPhoneNumberButton_OnClicked(object? sender, EventArgs e)
    {
        PhoneNumberEntry.IsEnabled = true;
        PhoneNumberEntry.Focus();
    }
    
    private void EditTgButton_OnClicked(object? sender, EventArgs e)
    {
        TelegramEntry.IsEnabled = true;
        TelegramEntry.Focus();
    }

    private void PhoneNumberEntry_OnCompleted(object? sender, EventArgs e)
    {
        CloseEntries();
    }

    private void TelegramEntry_OnCompleted(object? sender, EventArgs e)
    {
        CloseEntries();
    }

    private void CloseEntries()
    {
        PhoneNumberEntry.IsEnabled = false;
        PhoneNumberEntry.TextColor = Colors.Black;
        TelegramEntry.IsEnabled = false;
        TelegramEntry.TextColor = Colors.Black;
    }
}