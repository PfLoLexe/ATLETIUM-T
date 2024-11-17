using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.localDatabase.controllers;
using ATLETIUM_T.localDatabase.repositories;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using TrainDetail = ATLETIUM_T.views.TrainDetail;

namespace ATLETIUM_T.components;

public partial class ClientsList : ContentView
{
    private ObservableCollection<ClientAttendanceMark> _clients { get; set; }

    private LocalAttendanceMarksController _localController =
        new LocalAttendanceMarksController(new LocalAttendanceMarksRepository());
    
    public ClientsList()
    {
        InitializeComponent();
        ClientsListView.ItemsSource = _clients;
    }

    private void CheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        new ToastMessage().ShortToast(_clients[0].attendance_mark.ToString());
    }

    private void MarkCheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        
    }
}