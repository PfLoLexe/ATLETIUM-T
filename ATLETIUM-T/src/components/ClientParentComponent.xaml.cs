using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ATLETIUM_T.components;

public partial class ClientParentComponent : ContentView
{
    public ClientParentComponent(string parentFullName, string parentPhoneNumber)
    {
        InitializeComponent();
        AutomationId = "Родители";
        ParentFullNameEntry.Text = parentFullName;
        PhoneNumberEntry.Text = parentPhoneNumber;
        CloseEntries();
    }
    
    private void CloseEntries()
    {
        ParentFullNameEntry.IsEnabled = false;
        ParentFullNameEntry.TextColor = Colors.Black;
        PhoneNumberEntry.IsEnabled = false;
        PhoneNumberEntry.TextColor = Colors.Black;
    }
}