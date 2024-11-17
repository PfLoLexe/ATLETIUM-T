using System;
using System.Collections.Generic;
using ATLETIUM_T.Resources.locale;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.Models;

public enum TrainType
{
    Group,
    Solo
}

public class TrainListItem
{
    public TrainListItem(string train_id, string train_start_time, string train_end_time, string train_label, string train_place, TrainType train_type)
    {
        this.train_id = train_id;
        this.train_start_time = train_start_time;
        this.train_end_time = train_end_time;
        this.train_label = train_label;
        this.train_place = train_place;
        this.train_type = TrainTypeLocale.TrainTypesRussian[(int)train_type];
        switch ((int)train_type)
        {
            case 0:
                this.train_type_img = ImageSource.FromFile("group_icon.png");
                break;
            case 1:
                this.train_type_img = ImageSource.FromFile("one_to_one_icon.png");
                break;
        }
        
    }

    public string train_id { get; private set; }
    public string train_start_time { get; private set; }
    public string train_end_time { get; private set; }
    public string train_label { get; private set; }
    public string train_place { get; private set; }
    public string train_type { get; private set; }
    public ImageSource train_type_img { get; private set; }
    
    public string train_start_time_converted => DateTime.Parse(train_start_time).ToString("HH:mm");
    public string train_end_time_converted => DateTime.Parse(train_end_time).ToString("HH:mm");
}