using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.views;

[QueryProperty(nameof(train), "train")]
public partial class TrainDetail : ContentPage
{
    public string train { get; set; }
    public TrainDetail()
    {
        InitializeComponent();
        MainLayout.Children.Add(new TrainInfoTabBar([new ClientsDataGrid()]));
        new ToastMessage().ShortToast(train);
    }
    
}