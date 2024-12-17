namespace ATLETIUM_T.Models;

public class ClientResponse
{
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string middle_name { get; set; }
    public string phone_number { get; set; }
    public int age { get; set; }
    public Guid id { get; set; }
    public ParentInfo parent_info { get; set; }
}