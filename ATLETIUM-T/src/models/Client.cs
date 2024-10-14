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
        this.mark = false;
    }
    public Client client { get; private set; }
    public bool mark { get; set; }
}