
﻿using System;
using Microsoft.Maui.Controls;

using Microsoft.Maui.Handlers;

namespace ATLETIUM_T.views;

public partial class TrainsPage : ContentPage
{
    private readonly DateWeekDay _date_week_day = new DateWeekDay();
    private TrainsList _trainsList;
    
    public TrainsPage()
    {
        InitializeComponent();
        _trainsList = new TrainsList();
        MainLayout.Children.Add(_trainsList);
        LoadPageInfo();
    }

    private void LoadPageInfo()
    {
        DayOfTheWeekLabel.Text = _date_week_day.GetDayOfTheWeek();
        DayNumberLabel.Text = _date_week_day.GetDayAsInt();
        MonthNameLabel.Text = _date_week_day.GetMonthAsString();
        MainLayout.Children.Clear();
        TrainsList _trainsList = new TrainsList(_date_week_day.DayWeekNumber);
        MainLayout.Children.Add(_trainsList);
        LoadCountOfTrains();
    }
    
    // private void LoadCountOfTrains() =>
    //     CountOfTrainsTodayLabel.Text = "Занятия: " + TrainListTemplate.count_of_trains_today.ToString();
    private void LoadCountOfTrains() =>
    CountOfTrainsTodayLabel.Text = "Занятия: " + _trainsList.count_of_trains_today.ToString();

    private void TrainsPageMainRefreshView_OnRefreshing(object? sender, EventArgs e)
    {
        _trainsList.ListViewTrainsRefreshing();
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