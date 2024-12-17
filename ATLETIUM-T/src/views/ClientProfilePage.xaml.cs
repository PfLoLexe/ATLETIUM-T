using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.views;

[QueryProperty(nameof(ClientData), "testClient")]
public partial class ClientProfilePage : ContentPage
{
    private ContactsComponent _contactsComponent = null;
    private ClientParentComponent _parentComponent = null;
    private Client _clientData;
    private ClientController _clientController = new ClientController(new ClientRepository());
    public Client ClientData
    {
        get => _clientData;
        set
        {
            if (_clientData != value)
            {
                _clientData = value;
                OnValueChanged(_clientData);
            }
        }
    }

    private void OnValueChanged(Client newValue)
    {
        TrainerNameLabel.Text = newValue.fullname;
    }
    
    public ClientProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (_contactsComponent == null)
            LoadContacts();
    }

    private async void LoadContacts()
    {
        ClientResponse clientInfo = await _clientController.GetClientInfo(ClientData.id);
        _contactsComponent = new ContactsComponent($"{clientInfo.phone_number}", false);
        if (clientInfo.parent_info != null)
        {
            _parentComponent = new ClientParentComponent($"{clientInfo.parent_info.GetFullName}",
                $"{clientInfo.parent_info.phone_number}");
            MainLayout.Children.Add(new TrainInfoTabBar([_contactsComponent, _parentComponent]));
            return;
        }
        
        MainLayout.Children.Add(new TrainInfoTabBar([_contactsComponent]));
    }

    private async void BackButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        // new ToastMessage().ShortToast(ClientData.fullname);
    }
}