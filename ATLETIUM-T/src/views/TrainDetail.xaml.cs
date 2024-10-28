using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api;
using ATLETIUM_T.api.repository;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.views;

[QueryProperty(nameof(Train), "train")]
public partial class TrainDetail : ContentPage
{
 

    public TrainListItem Train { get; set; }
    public TrainDetail()
    {
        InitializeComponent();
        MainLayout.Children.Add(new TrainInfoTabBar([new ClientsDataGrid()]));
        // ClientsDataGridBorder.AddLogicalChild(new TrainInfoTabBar([new ClientsDataGrid()]));
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        TrainNameLabel.Text = Train.train_label;
        TrainTimeLabel.Text = $"{Train.train_start_time} - {Train.train_end_time}";
    }

}