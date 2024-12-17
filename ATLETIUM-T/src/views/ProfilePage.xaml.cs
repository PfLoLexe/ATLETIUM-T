using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T;

public partial class ProfilePage : ContentPage
{
    private TrainerProfileComponent? trainerProfile = null;
    
    public  ProfilePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (trainerProfile == null)
        {
            LoadComponent();
            return;
        }
        
        trainerProfile.LoadPageData();
    }

    private async void LoadComponent()
    {
        trainerProfile = new TrainerProfileComponent(new Guid());
        MainStackLayout.Children.Add(trainerProfile);
        trainerProfile.LoadPageData();
    }
}