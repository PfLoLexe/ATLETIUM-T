namespace ATLETIUM_T.Models;

public class ParentInfo
{
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string middle_name { get; set; }
    public string phone_number { get; set; }
    
    public string GetFullName => $"{firstname} {lastname}";
}