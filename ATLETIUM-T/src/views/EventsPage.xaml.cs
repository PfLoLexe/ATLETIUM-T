using System.Collections.ObjectModel;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T
{
    public partial class EventsPage : ContentPage
    {
        
        private ObservableCollection<TrainListItem> _trains { get; set; } = new ObservableCollection<TrainListItem>();
        
        public EventsPage()
        {
            InitializeComponent();
        }
        
    }

}
