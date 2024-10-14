using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using TrainDetail = ATLETIUM_T.views.TrainDetail;

namespace ATLETIUM_T.components;

public partial class ClientsList : ContentView
{
    private ObservableCollection<ClientAttendanceMark> clients { get; set; }
    public ClientsList()
    {
        InitializeComponent();
        clients = new ObservableCollection<ClientAttendanceMark>
        {
            new ClientAttendanceMark(new Client("Александр")),
            new ClientAttendanceMark(new Client("Влад")),
            new ClientAttendanceMark(new Client("Марина")),
            new ClientAttendanceMark(new Client("Яна")),
        };
        ClientsListView.ItemsSource = clients;
    }

    private void CheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        new ToastMessage().ShortToast(clients[0].mark.ToString());
    }
}