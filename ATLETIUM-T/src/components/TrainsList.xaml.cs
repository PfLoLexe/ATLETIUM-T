using System.Collections.Generic;
using System.Collections.ObjectModel;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;
using System.Timers;
using ATLETIUM_T.api.controllers;
using ATLETIUM_T.api.repositories;

using Timer = System.Timers.Timer;

namespace ATLETIUM_T.components;

public partial class TrainsList : ContentView
{

    private ObservableCollection<TrainMain> _trains { get; set; } = new ObservableCollection<TrainMain>();
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
    private TrainController _controller = new TrainController(new TrainRepository());
    public TrainsList()

    {
        InitializeComponent();
        DayWeekNumber = (int)DateTime.Now.DayOfWeek == 0 ? 7 : (int)DateTime.Now.DayOfWeek;;
        LoadTrains();
        _train_list_view_tapped_timer.Elapsed += OnTappedTimerEvent;
        TrainsListView.ItemsSource = _trains;
    }
    
    public void ListViewTrainsRefreshing()
    {
        LoadTrains();
    }
    
    private async void LoadTrains()
    {
        var trainsList = await _controller.GetTrainsListAsync(DayWeekNumber, DateTime.Now);
        if (_trains != null) _trains.Clear();
        if (trainsList == null) return;
        foreach (var train in trainsList)
        {
            _trains.Add(train);
        }

        count_of_trains_today = _trains.Count;
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