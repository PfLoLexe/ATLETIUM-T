using System;
using System.Threading.Tasks;
using ATLETIUM_T.components;
 using Microsoft.Maui.Controls;

using Microsoft.Maui.Handlers;

namespace ATLETIUM_T.views;

public partial class TrainsPage : ContentPage
{
    private readonly DateWeekDay _date_week_day = new DateWeekDay();
    private TrainsList _trainsList = new TrainsList();
    private int SelectedDay { get; set; } = 0;
    private DateTime SelectedDayAsDayTime { get; set; }
    
    public TrainsPage()
    {
        InitializeComponent();
        MainLayout.Children.Add(_trainsList);
        _trainsList.ValueChanged += TrainsListOnValueChanged;
        LoadPageInfo();
    }

    private void TrainsListOnValueChanged(object? sender, int e)
    {
        CountOfTrainsTodayLabel.Text = "Занятия: " + _trainsList.AmountOfTrains.ToString();
    }

    private async void LoadPageInfo()
    {
        DayOfTheWeekLabel.Text = _date_week_day.GetDayOfTheWeek();
        DayNumberLabel.Text = _date_week_day.GetDayAsInt();
        MonthNameLabel.Text = _date_week_day.GetMonthAsString();
        
        if (SelectedDay == 0)
            SelectedDay = (int)DateTime.Now.DayOfWeek == 0 ? 7 : (int)DateTime.Now.DayOfWeek;
        _trainsList.DayWeekNumber = SelectedDay;
        _trainsList.CurrentDay = SelectedDayAsDayTime;
        _trainsList.LoadTrains();
    }


    private void TrainsPageMainRefreshView_OnRefreshing(object? sender, EventArgs e)
    {
        _trainsList.ListViewTrainsRefreshing();
        TrainsPageMainRefreshView.IsRefreshing = false;
    }

    private void DatePickerImageButton_OnClicked(object? sender, EventArgs e)
    {
        /*#if ANDROID
            var handler = _date_picker.Handler as IDatePickerHandler;
            handler.PlatformView.PerformClick();
        #else
            _date_picker.Focus();
        #endif*/
        
        TrainsDatePicker.IsVisible = true;
        #if ANDROID
            var handler = TrainsDatePicker.Handler as IDatePickerHandler;
            handler.PlatformView.PerformClick();
        #endif
    }

    private void TrainsDatePicker_OnDateSelected(object? sender, DateChangedEventArgs e)
    {
        _date_week_day.SetDate(e.NewDate);
        SelectedDay = (int)e.NewDate.DayOfWeek == 0 ? 7 : (int)e.NewDate.DayOfWeek;
            SelectedDayAsDayTime = e.NewDate;
        LoadPageInfo();
    }
}