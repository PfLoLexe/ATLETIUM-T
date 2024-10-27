using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.Models;

namespace ATLETIUM_T.components;

public partial class ClientsDataGrid : ContentView
{
    private ObservableCollection<ClientAttendanceMark> _clients { get; set; }
    
    public ClientsDataGrid()
    {
        InitializeComponent();
        
        // Название списка (используется для генерации вкладок tab bar`a)
        this.AutomationId = "Клиенты";
        
        _clients = new ObservableCollection<ClientAttendanceMark>
        {
            new ClientAttendanceMark(new Client("Александр")),
            new ClientAttendanceMark(new Client("Влад")),
            new ClientAttendanceMark(new Client("Марина")),
            new ClientAttendanceMark(new Client("Яна")),
        };
        ClientsDataGridView.ItemsSource = _clients;
        ClientsDataGridViewAttendanceComboBoxColumn.ItemsSource = AttendanceMark.attendance_variations_list;
    }


}