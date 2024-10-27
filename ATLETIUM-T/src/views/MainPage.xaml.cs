using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T
{
    public partial class MainPage : ContentPage
    {
        private readonly DateWeekDay _date_week_day = new DateWeekDay();
        
        public MainPage()
        {
            InitializeComponent();
            LoadPageInfo();
        }

        private void LoadPageInfo()
        {
            DayOfTheWeekLabel.Text = _date_week_day.GetDayOfTheWeek();
            DayAndMonthLabel.Text = _date_week_day.GetDayAsInt() +
                                    ' ' + _date_week_day.GetMonthAsString();
            LoadCountOfTrains();
        }

        private void LoadCountOfTrains() =>
            CountOfTrainsTodayLabel.Text = "Занятия: " + TrainListTemplate.count_of_trains_today.ToString();


        private void MainPageMainRefreshView_OnRefreshing(object? sender, EventArgs e)
        {
            TrainListTemplate.ListViewTrainsRefreshing();
            LoadCountOfTrains();
            MainPageMainRefreshView.IsRefreshing = false;
        }
    }
}
