using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        HttpClient _client = new HttpClient();
        Uri uri = new Uri("https://10.0.2.2:8000/hello/Android");
        HttpResponseMessage response = await _client.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            new ToastMessage().ShortToast(content);
        }
        new ToastMessage().ShortToast("All bad");
    }
}