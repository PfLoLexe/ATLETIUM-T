using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ATLETIUM_T.components;
using ATLETIUM_T.Models;
using Microsoft.Maui.Controls;

namespace ATLETIUM_T
{
    public partial class MainPage : ContentPage
    {
        private readonly DateWeekDay _date_week_day = new DateWeekDay();
        private TrainsList _trainsList;
        
        public MainPage()
        {
            InitializeComponent();
            _trainsList = new TrainsList();
            MainLayout.Children.Add(_trainsList);
            _trainsList.ValueChanged += TrainsListOnValueChanged;
            LoadPageInfo();
        }

        private void LoadPageInfo()
        {
            DayOfTheWeekLabel.Text = _date_week_day.GetDayOfTheWeek();
            DayAndMonthLabel.Text = _date_week_day.GetDayAsInt() +
                                    ' ' + _date_week_day.GetMonthAsString();
        }

        private void TrainsListOnValueChanged(object? sender, int e)
        {
            CountOfTrainsTodayLabel.Text = "Занятия: " + _trainsList.AmountOfTrains.ToString();
        }


        private void MainPageMainRefreshView_OnRefreshing(object? sender, EventArgs e)
        {
            _trainsList.ListViewTrainsRefreshing();
            MainPageMainRefreshView.IsRefreshing = false;
        }
    }
}
