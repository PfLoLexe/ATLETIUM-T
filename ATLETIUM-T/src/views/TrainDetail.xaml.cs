using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.views;

[QueryProperty(nameof(Train), "train")]
public partial class TrainDetail : ContentPage
{
    private readonly TrainController _controller = new TrainController(new TrainRepository());
    private ClientsList _clientDataGrid = null;
    
    public TrainMain Train { get; set; }

    public TrainDetail()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        TrainNameLabel.Text = Train.name;
        TrainTimeLabel.Text = $"{Train.start_time_parsed} - {Train.end_time_parsed}";
        AddClients();
    }

    private async void AddClients()
    {
        TrainSpecific train = await _controller.GetTrainInfo(Train.id, Train.date_parsed);
        if (train == null) return;
        if (train.clients_list == null) return;
        
        List<Client> clients = new List<Client>();
        foreach (Client client in train.clients_list)
        {
            clients.Add(client);
        }

        //ClientsDataGrid clientsDataGrid = new ClientsDataGrid(train, clients);
        _clientDataGrid = new ClientsList(train, clients);
        MainLayout.Children.Clear();
        MainLayout.Children.Add(new TrainInfoTabBar([_clientDataGrid]));
        _clientDataGrid.UpdateClientsMarks();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        if (_clientDataGrid == null) return;
        _clientDataGrid.UploadListToServer();
        new ToastMessage().ShortToast("Сохранено");
    }

    private async void AddChatButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}