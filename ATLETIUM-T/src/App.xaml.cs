using System.Globalization;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

}