using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

namespace ATLETIUM_T.views;

public partial class TrainsPage : ContentPage
{
    private readonly DateWeekDay _date_week_day = new DateWeekDay();
    
    public TrainsPage()
    {
        InitializeComponent();
        LoadPageInfo();
    }

    private void LoadPageInfo()
    {
        DayOfTheWeekLabel.Text = _date_week_day.GetDayOfTheWeek();
        DayNumberLabel.Text = _date_week_day.GetDayAsInt();
        MonthNameLabel.Text = _date_week_day.GetMonthAsString();
        LoadCountOfTrains();
    }
    
    private void LoadCountOfTrains() =>
        CountOfTrainsTodayLabel.Text = "Занятия: " + TrainListTemplate.count_of_trains_today.ToString();

    private void TrainsPageMainRefreshView_OnRefreshing(object? sender, EventArgs e)
    {
        TrainListTemplate.ListViewTrainsRefreshing();
        LoadCountOfTrains();
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
        LoadPageInfo();
    }
}