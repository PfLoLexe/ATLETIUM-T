using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class ClientsDataGrid : ContentView
{
    private TrainController _controller = new(new TrainRepository());

    private readonly LocalAttendanceMarksController _localController = new(new LocalAttendanceMarksRepository());

    public ClientsDataGrid(TrainSpecific? trainSpecific, List<Client>? clients = null)
    {
        InitializeComponent();
        AutomationId = "Клиенты";
        _trainSpecific = trainSpecific;
        
        ClientsDataGridView.ItemsSource = _clients;
        ClientsDataGridViewAttendanceComboBoxColumn.ItemsSource = AttendanceMark.attendance_variations_list;
    }

    private ObservableCollection<ClientAttendanceMark> _clients { get; } = new();

    private TrainSpecific? _trainSpecific { get; }

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

        foreach (ClientAttendanceMark mark in _clients)
            mark.PropertyChanged += Mark_PropertyChanged;
        
    }

    private async Task<Dictionary<Guid, Client>?> IsLocalMarks()
    {
        if (_trainSpecific == null) return null;

        var localClientsList = await _localController.GetMarks(_trainSpecific.id);
        if (localClientsList.Count == 0) return null;

        return localClientsList;
    }

    private async void Mark_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var item = sender as ClientAttendanceMark;
        new ToastMessage().ShortToast("mark changed");
        await _localController.UpdateMark(_trainSpecific.id, item.client);
    }

    private void ClientsDataGridViewAttendanceComboBoxColumn_OnBindingContextChanged(object? sender, EventArgs e)
    {
        new ToastMessage().ShortToast("mark changed");
    }
}