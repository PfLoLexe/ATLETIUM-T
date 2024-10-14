using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.components;

public partial class ClientsDataGrid : ContentView
{
    private ObservableCollection<ClientAttendanceMark> clients { get; set; }
    
    public ClientsDataGrid()
    {
        InitializeComponent();
        clients = new ObservableCollection<ClientAttendanceMark>
        {
            new ClientAttendanceMark(new Client("Александр")),
            new ClientAttendanceMark(new Client("Влад")),
            new ClientAttendanceMark(new Client("Марина")),
            new ClientAttendanceMark(new Client("Яна")),
        };
        ClientsDataGridView.ItemsSource = clients;
    }
}