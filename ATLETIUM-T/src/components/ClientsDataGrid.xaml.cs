using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class ClientsDataGrid : ContentView
{
    private ObservableCollection<ClientAttendanceMark> _clients { get; set; }
    
    public ClientsDataGrid(List<Client>? clients = null)
    {
        InitializeComponent();
        this.AutomationId = "Клиенты";
        
        if (clients == null) return;
        
        _clients = new ObservableCollection<ClientAttendanceMark>();
        foreach (Client client in clients)
        {
            _clients.Add(new ClientAttendanceMark(client));
        }
        
        ClientsDataGridView.ItemsSource = _clients;
        ClientsDataGridViewAttendanceComboBoxColumn.ItemsSource = AttendanceMark.attendance_variations_list;
    }


}