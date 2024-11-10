using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace ATLETIUM_T.components;

public partial class TrainInfoTabBar : ContentView
{
private List<ContentView> _list;
    private List<Button> _tabButtons;
    private List<BoxView> _tabs;
    private int i = 0;
    
    public TrainInfoTabBar(List<ContentView> list)
    {
        InitializeComponent();
        _list = list;
        _tabs = new List<BoxView>();
        _tabButtons = new List<Button>();
        TabBarGenerator(list);
        UncoloredTabBar();
        ColorTab(0);
        MainScrollView.Content = _list.FirstOrDefault();
    }

    private void ButtonOnClicked(object? sender, EventArgs e)
    {
        Button button = (Button)sender!;
        UncoloredTabBar();
        ColorTab(int.Parse(button.AutomationId));
        MainScrollView.Content = _list[int.Parse(button.AutomationId)];
    }

    private void TabBarGenerator(List<ContentView> views)
    {
        int tabCount = views.Count;
        int spaceCount = tabCount++;
        int tabIndex = 0;

        for (int i = 0; i < tabCount + spaceCount; i++)
        {
            if (i % 2 == 0)
            {
                TabsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                BtnsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            else
            {
                TabsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                BtnsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            }
        }

        for (int i = 0; i < tabCount + spaceCount; i++)
        {
            if (i % 2 == 0)
            {
                BoxView boxView = new BoxView
                {
                    Color = (Color)Application.Current.Resources["DefaultBackLineColor"],
                    HeightRequest = 3,
                };
                TabsGrid.Add(boxView, i, 0);
            }
            else
            {
                Button button = new Button
                {
                    BackgroundColor = Colors.Transparent,
                    BorderColor = Colors.Transparent,
                    TextColor = (Color)Application.Current.Resources["UnselectedTabColor"],
                    Text = views[tabIndex].AutomationId,
                    AutomationId = (tabIndex).ToString()
                };
                tabIndex++;
                button.Clicked += ButtonOnClicked;
                _tabButtons.Add(button);
                BtnsGrid.Add(button, i, 0);

                BoxView boxView = new BoxView
                {
                    Color = (Color)Application.Current.Resources["SelectedTabColor"],
                    HeightRequest = 3,
                    AutomationId = (tabIndex).ToString()
                };
                _tabs.Add(boxView);
                TabsGrid.Add(boxView, i, 0);
            }
        }
    }

    private void UncoloredTabBar()
    {
        if (_tabs is null || _tabButtons is null || _tabs.Count != _tabButtons.Count) return;
        
        foreach (BoxView boxView in _tabs)
            boxView.Color = (Color)Application.Current.Resources["UnselectedTabColor"];
        
        foreach (Button button in _tabButtons)
            button.TextColor = (Color)Application.Current.Resources["UnselectedTabColor"];
        
    }

    private void ColorTab(int index)
    {
        if (_tabs is null || _tabButtons is null || _tabs.Count != _tabButtons.Count) return;
        _tabs[index].Color = (Color)Application.Current.Resources["SelectedTabColor"];
        _tabButtons[index].TextColor = (Color)Application.Current.Resources["SelectedTabColor"];
    }
}