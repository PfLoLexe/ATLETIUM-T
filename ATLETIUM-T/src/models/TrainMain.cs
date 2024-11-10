namespace ATLETIUM_T.Models;

public class TrainMain(
    Guid? id,
    string? name,
    string? start_time,
    string? end_time,
    string? place_name,
    string? train_type,
    string? date)
{
    public Guid? id { get; set; } = id;
    public string? name { get; set; } = name;
    public string? start_time { get; set; } = start_time;
    public string? end_time { get; set; } = end_time;
    public string? place_name { get; set; } = place_name;
    public string? train_type { get; set; } = train_type;
    public string? date { get; set; } = date;

    public string? start_time_parsed => DateTime.Parse(start_time).ToString("HH:mm");
    public string? end_time_parsed => DateTime.Parse(end_time).ToString("HH:mm");

    public DateTime date_parsed
    {
        get
        {
            if (date == null) return DateTime.Today;
            return DateTime.Parse(date);
        }
    }

    public ImageSource train_type_img
    {
        get
        {
            switch (train_type)
            {
                case "Personal":
                    return ImageSource.FromFile("group_icon.png");

                case "Group":
                    return ImageSource.FromFile("one_to_one_icon.png");
            }

            return null;
        }

        set { return; }
    }
    
    public string? train_type_ru
    {
        get
        {
            switch (train_type)
            {
                case "Personal":
                    return "Персональная";

                case "Group":
                    return "Групповая";
            }

            return null;
        }

        set { return; }
    }
}