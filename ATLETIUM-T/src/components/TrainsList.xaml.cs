using System.Collections.ObjectModel;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ATLETIUM_T.components;

public partial class TrainsList : ContentView
{
    private ObservableCollection<TrainListItem> _trains { get; set; } = new ObservableCollection<TrainListItem>();
    private int? _train_list_view_tapped;
    private Timer _train_list_view_tapped_timer = new Timer(2000);
    public int count_of_trains_today { get; private set; } = 0;
    public TrainsList()
    {
        InitializeComponent();
        LoadTrains();
        _train_list_view_tapped_timer.Elapsed += OnTappedTimerEvent;
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
        _train_list_view_tapped_timer.Start();
        if (e.ItemIndex == _train_list_view_tapped)
        {
            _train_list_view_tapped = null;
            await Shell.Current.GoToAsync($"{nameof(TrainDetail)}");
        }
        else
        {
            _train_list_view_tapped = e.ItemIndex;
        }
    }

    private void OnTappedTimerEvent(object sender, ElapsedEventArgs e)
    {
        _train_list_view_tapped = null;
    }
}