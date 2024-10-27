using System.Collections.ObjectModel;

namespace ATLETIUM_T.Models;

public class AttendanceMark
{
    public enum AttendanceMarkEnum
    {
        none,
        attended,
        not_attended,
        ill,
    }

    public class AttendanceMarkToCaption(string caption, AttendanceMarkEnum value)
    {
        public string caption { get; private set; } = caption;
        public AttendanceMarkEnum value { get; private set; } = value;
    }
    
    public static readonly ObservableCollection<AttendanceMarkToCaption> attendance_variations_list =
    [
        new AttendanceMarkToCaption("None", AttendanceMarkEnum.none),
        new AttendanceMarkToCaption("Attended", AttendanceMarkEnum.attended),
        new AttendanceMarkToCaption("Not Attended", AttendanceMarkEnum.not_attended),
        new AttendanceMarkToCaption("Ill", AttendanceMarkEnum.ill)
    ];
}

