using System;

namespace ATLETIUM_T.Models;

public class DialogueResponse
{
    public string recipient_user_firstname { get; set; }
    public string recipient_user_lastname { get; set; }
    public string recipient_user_middle_name { get; set; }
    public Guid dialogue_id { get; set; }
    public Guid first_user_id { get; set; }
    public Guid second_user_id { get; set; }
}