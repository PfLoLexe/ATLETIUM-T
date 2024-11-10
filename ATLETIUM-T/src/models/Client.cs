namespace ATLETIUM_T.Models;

public class Client()
{

    public string? firstname { get; set; }
    public string? lastname { get; set; }
    public Guid id { get; set; }
    public int? visit_status { get; set; }

    public string? fullname => firstname + " " + lastname;
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