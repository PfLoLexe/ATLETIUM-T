using System.Collections.ObjectModel;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T.components;

public partial class TrainsList : ContentView
{
    private ObservableCollection<TrainListItem> _trains { get; set; } = new ObservableCollection<TrainListItem>();
    private object? _train_list_view_selected;
    public int count_of_trains_today { get; private set; } = 0;
    public TrainsList()
    {
        InitializeComponent();
        LoadTrains();
    }
    
    public void ListViewTrainsRefreshing()
    {
        _trains.Add(new TrainListItem("1", "11:00", "10:30", "Плавание 15+", "Бассейн", TrainType.Group));
        
        count_of_trains_today = _trains.Count;
    }
    
    private void LoadTrains()
    {
        _trains.Add(new TrainListItem("1", "10:00", "10:30", "Плавание 10+", "Бассейн", TrainType.Solo));
        count_of_trains_today = _trains.Count;
        TrainsListView.ItemsSource = _trains;
    }

    private async void TrainsListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if (TrainsListView.SelectedItem == _train_list_view_selected)
        {
            _train_list_view_selected = null;
            TrainsListView.SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(TrainDetail)}");
        }
        else
        {
            _train_list_view_selected = TrainsListView.SelectedItem;
        }
    }
}