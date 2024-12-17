using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using UITimer = System.Timers.Timer;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;
using ATLETIUM_T.views;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class ClientsList : ContentView
{
    private ObservableCollection<ClientAttendanceMark> _clients { get; set; } = new();
    private LocalAttendanceMarksController _localController =
        new LocalAttendanceMarksController(new LocalAttendanceMarksRepository());
    
    private TrainController _controller = new TrainController(new TrainRepository());
    private TrainSpecific? _trainSpecific { get; }
    
    private UITimer _clientsTappedTimer = new UITimer(2000);
    private int? _clientsListItemTapped;
    
    public ClientsList(TrainSpecific? trainSpecific, List<Client>? clients = null)
    {
        InitializeComponent();
        AutomationId = "Клиенты";
        _trainSpecific = trainSpecific;
        _clientsTappedTimer.Elapsed += OnTappedTimerEvent;
        ClientsListView.ItemsSource = _clients;
    }
    
    public async void UpdateClientsMarks()
    {
        if (_trainSpecific.clients_list == null) return;
        if (_clients != null) _clients.Clear();
        var localClientsList = await IsLocalMarks();
        foreach (var client in _trainSpecific.clients_list)
        {
            if (localClientsList != null && localClientsList.ContainsKey(client.id))
                client.visit_status = localClientsList[client.id].visit_status;
            ClientAttendanceMark mark = new ClientAttendanceMark(client);
            
            _clients.Add(mark);
        }
    }
    
    private async Task<Dictionary<Guid, Client>?> IsLocalMarks()
    {
        if (_trainSpecific == null) return null;

        var localClientsList = await _localController.GetMarks(_trainSpecific.id);
        if (localClientsList.Count == 0) return null;

        return localClientsList;
    }

    private async void MarksPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        var picker = sender as Picker;
        var item = picker.BindingContext as ClientAttendanceMark;
        item.client.visit_status = picker.SelectedIndex;
        await _localController.UpdateMark(_trainSpecific.id, item.client);
    }

    public async void UploadListToServer()
    {
        if (_clients == null) return;
        _controller.UploadClientsStatus(_trainSpecific.id, _clients);
    }
    
    private void OnTappedTimerEvent(object sender, ElapsedEventArgs e)
    {
        _clientsListItemTapped = null;
    }

    private async void ClientsListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        _clientsTappedTimer.Start();
        if (e.ItemIndex == _clientsListItemTapped)
        {
            _clientsListItemTapped = null;
            var t = _clients[e.ItemIndex];
            await Shell.Current.GoToAsync($"{nameof(ClientProfilePage)}",
                new Dictionary<string, object>
                {
                    ["testClient"] = _clients[e.ItemIndex].client
                });
        }
        else
        {
            _clientsListItemTapped = e.ItemIndex;
        }
    }
}