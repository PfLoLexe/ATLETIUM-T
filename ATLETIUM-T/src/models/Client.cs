using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ATLETIUM_T.Models;

public class Client()
{

    public string? firstname { get; set; }
    public string? lastname { get; set; }
    public Guid id { get; set; }
    public int visit_status { get; set; }

    public string? fullname => firstname + " " + lastname;
}

public class ClientAttendanceMark
{
    public ClientAttendanceMark(Client client)
    {
        this.client = client;
        this.attendance_list = AttendanceMark.attendance_list;
    }
    public Client client { get; private set; }
    public List<string> attendance_list { get; private set; }

    public string status =>
        attendance_list[
            client.visit_status
        ];
}