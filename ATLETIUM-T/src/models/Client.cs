namespace ATLETIUM_T.Models;

public class Client
{

    public Client(string fullname)
    {
        this.fullname = fullname;
    }
    
    public string fullname { get; private set; }
}

public class ClientAttendanceMark
{
    public ClientAttendanceMark(Client client)
    {
        this.client = client;
    }
    public Client client { get; private set; }
    public AttendanceMark.AttendanceMarkEnum attendance_mark { get; set; }
}

/*public ObservableCollection<AttendanceMark.AttendanceMarkToCaption> attendance_mark { get; set; }
= AttendanceMark.attendance_variations_list;*/