using ATLETIUM_T.views;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            tabBar.CurrentItem = mainPage;
            Routing.RegisterRoute(nameof(TrainDetail), typeof(TrainDetail));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
