using System.Collections.ObjectModel;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using System.Timers;
using ATLETIUM_T.api;
using ATLETIUM_T.api.repository;
using Timer = System.Timers.Timer;

namespace ATLETIUM_T.components;

public partial class TrainsList : ContentView
{
    private TrainRepository _repository = new TrainRepository(new ApiHandler());
    private ObservableCollection<TrainListItem> _trains { get; set; } = new ObservableCollection<TrainListItem>();
    private int? _train_list_view_tapped;
    private Timer _train_list_view_tapped_timer = new Timer(2000);
    private int _dayWeekNumber;
    
    private int DayWeekNumber
    {
        get
        {
            if (_dayWeekNumber == 0) return (int)DateTime.Now.DayOfWeek;
            return _dayWeekNumber;
        }
        
        set { _dayWeekNumber = value; }
    }
    public int count_of_trains_today { get; private set; } = 0;
    public TrainsList(int dayWeekNumber = 0)
    {
        InitializeComponent();
        DayWeekNumber = dayWeekNumber;
        LoadTrains();
        _train_list_view_tapped_timer.Elapsed += OnTappedTimerEvent;
    }
    
    public void ListViewTrainsRefreshing()
    {
        LoadTrains();
        count_of_trains_today = _trains.Count;
    }
    
    private async void LoadTrains()
    {
        
        
        List<TrainListItem>? trainsList = await _repository.GetTrainsByWeekDayNumber(DayWeekNumber);
        
        if (_trains != null) _trains.Clear();
        if (trainsList == null) return;

        foreach (var train in trainsList) 
            _trains.Add(train);
        
        count_of_trains_today = _trains.Count;
        TrainsListView.ItemsSource = _trains;
    }

    private async void TrainsListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        _train_list_view_tapped_timer.Start();
        if (e.ItemIndex == _train_list_view_tapped)
        {
            _train_list_view_tapped = null;

            await Shell.Current.GoToAsync($"{nameof(TrainDetail)}",
                new Dictionary<string, object>
                {
                    ["train"] = _trains[e.ItemIndex]
                });

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