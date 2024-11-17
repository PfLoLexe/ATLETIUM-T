using System;
using System.ComponentModel;

namespace ATLETIUM_T.Models;

public class Client()
{

    public string? firstname { get; set; }
    public string? lastname { get; set; }
    public Guid id { get; set; }
    public int visit_status { get; set; }

    public string? fullname => firstname + " " + lastname;
}

public class ClientAttendanceMark : INotifyPropertyChanged
{
    public ClientAttendanceMark(Client client)
    {
        this.client = client;
    }
    public Client client { get; private set; }
    public AttendanceMark.AttendanceMarkEnum attendance_mark { get; set; }
    
    // Реализация INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

/*public ObservableCollection<AttendanceMark.AttendanceMarkToCaption> attendance_mark { get; set; }
= AttendanceMark.attendance_variations_list;*/