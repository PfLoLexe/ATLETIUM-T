using System;

namespace ATLETIUM_T.Models;

public class PossibleDialogueResponse
{
    public string recipient_user_firstname { get; set; }
    public string recipient_user_lastname { get; set; }
    public string recipient_user_middle_name { get; set; }
    public Guid recipient_user_id { get; set; }
}