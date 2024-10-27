using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.XtraPrinting.Native;

namespace ATLETIUM_T.Models;

public class TrainDetail
{
    public TrainDetail(string id, string label, Collection<ClientAttendanceMark> clients)
    {
        this.train_id = id;
        this.train_label = label;
        this.train_clients = clients;
        this.train_clients_count = clients.Count;
    }
    public string train_id { get; private set; }
    public string train_label { get; private set; }
    public Collection<ClientAttendanceMark> train_clients { get; private set; }
    public int train_clients_count { get; private set; }
}