namespace ATLETIUM_T.Models;

public class TrainSpecific()
{
        
    public Guid? id { get; set; }
    public int? clients_amount { get; set; }
    public bool is_over { get; set; }
    public List<Client>? clients_list { get; set; }
}