using System;
using System.Collections.Generic;
using System.Globalization;
using ATLETIUM_T.Resources.locale;

namespace ATLETIUM_T;

public class DateWeekDay
{
    private CultureInfo _culture = new CultureInfo("RU-ru");
    public DateTime _date { get; private set; }
    
    public DateWeekDay()
    {
        _date = DateTime.Now;
    }
    
    public string GetDayOfTheWeek()
    { 
        string day_name = (_culture.DateTimeFormat.DayNames[(int)_date.DayOfWeek]);
        return day_name[0].ToString().ToUpper() + day_name.Substring(1);
    }
    
    public string GetDayAsInt()
    { 
        return _date.Day.ToString();
    }

    public string GetMonthAsString()
    {
        int month = _date.Month;
        return MonthLocale.MonthRussian[month - 1];
    }

    public string GetYearAsString()
    {
        return _date.Year.ToString();
    }

    public void SetDate(DateTime date)
    {
        //new ToastMessage().LongToast(date.ToShortDateString());
        _date = date;
    }
}