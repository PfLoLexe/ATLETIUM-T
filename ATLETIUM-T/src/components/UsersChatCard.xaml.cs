using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class UsersChatCard : ContentView
{
    public UsersChatCard(ChatUser chatUser)
    {
        InitializeComponent();
        FullNameLabel.Text = GetFullName(chatUser);
    }

    private string GetFullName(ChatUser chatUser)
    {
        return $"{chatUser.Firstname} {chatUser.Lastname}";
    }
}