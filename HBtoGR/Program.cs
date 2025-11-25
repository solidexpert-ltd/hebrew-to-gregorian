// See https://aka.ms/new-console-template for more information

using System.Globalization;
using SolidExpert.HebrewToGregorian;

HBHolidays hollydays = new HBHolidays();
var hc = new HebrewCalendar();
var gc = new GregorianCalendar();
foreach (var dateTime in hollydays.GetHolidaysForGregorianYear(new DateTime(2023, 1, 1)).OrderBy(x=>x.Date))
{
    
    Console.WriteLine("GR: " + dateTime.ToString("yyyy-M-d") + " HB: " + $"{hc.GetYear(dateTime)}-{hc.GetMonth(dateTime)}-{hc.GetDayOfMonth(dateTime)}");
}


