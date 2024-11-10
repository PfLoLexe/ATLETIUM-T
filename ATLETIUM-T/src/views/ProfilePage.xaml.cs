using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ATLETIUM_T.api;
using ATLETIUM_T.api.repository;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T;

public partial class ProfilePage : ContentPage
{
    private TrainRepository repa = new TrainRepository(new ApiHandler());
    private List<TrainListItem> _resp = new List<TrainListItem>();
    public  ProfilePage()
    {
        InitializeComponent();
        
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        // HttpClient _client = new HttpClient();
        // Uri uri = new Uri("http://10.0.2.2:8000/hello/Android");
        // HttpResponseMessage response = await _client.GetAsync(uri);
        //
        // string content = await response.Content.ReadAsStringAsync();
        // new ToastMessage().ShortToast(content);
        
        _resp = await repa.GetTrainsByWeekDayNumber(1);
        
        new ToastMessage().ShortToast($"test");
        new ToastMessage().ShortToast($"{_resp.Count}, {_resp[0].train_label}");
    }
}