using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.views;

public partial class TrainDetail : ContentPage
{
    public TrainDetail()
    {
        InitializeComponent();
        /*ObservableCollection<ClientAttendanceMark> clients = new ObservableCollection<ClientAttendanceMark>
        {
            new ClientAttendanceMark(new Client("Александр")),
            new ClientAttendanceMark(new Client("Влад")),
            new ClientAttendanceMark(new Client("Марина")),
            new ClientAttendanceMark(new Client("Яна")),
        };
        //clients[0].client
        ClientsListView.ItemsSource = clients;*/
    }
}