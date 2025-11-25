using System.Globalization;

namespace SolidExpert.HebrewToGregorian;

public class HBHolidays
{
    HebrewCalendar hebrewCalendar = new HebrewCalendar();

    private int currentYear;
    private int Tishrei => 1;
    private int Cheshvan => 2;
    private int Kislev => 3;
    private int Tevet => 4;
    private int Shevat => 5;
    private int Adar => 6;
    private int AdarBeit => 7;
    private int Nissan => hebrewCalendar.IsLeapYear(currentYear) ? 8 : 7;
    private int Iyar => hebrewCalendar.IsLeapYear(currentYear) ? 9 : 8;
    private int Sivan => hebrewCalendar.IsLeapYear(currentYear) ? 10 : 9;
    private int Tamuz => hebrewCalendar.IsLeapYear(currentYear) ? 11 : 10;
    private int Av => hebrewCalendar.IsLeapYear(currentYear) ? 12 : 11;
    private int Elul => hebrewCalendar.IsLeapYear(currentYear) ? 13 : 12;
    
    public List<DateTime> Holidays(int year)
    {
        currentYear = year;
        return new List<DateTime>
        {
            //Nisan
            // hebrewCalendar.ToDateTime(year,1,14,0, 0, 0, 0, Calendar.CurrentEra),
            new DateTime(year, Nissan, 14, hebrewCalendar), // Erev Pesach
            new DateTime(year, Nissan, 15, hebrewCalendar), // Pesach I
            new DateTime(year, Nissan, 16, hebrewCalendar), // Pesach II
            new DateTime(year, Nissan, 17, hebrewCalendar), // Pesach III (CH''M)
            new DateTime(year, Nissan, 18, hebrewCalendar), // Pesach IV (CH''M)
            new DateTime(year, Nissan, 19, hebrewCalendar), // Pesach V (CH''M)
            new DateTime(year, Nissan, 20, hebrewCalendar), // Pesach VI (CH''M)
            new DateTime(year, Nissan, 21, hebrewCalendar), // Pesach VII
            new DateTime(year, Nissan, 22, hebrewCalendar), // Pesach VIII
            // Iyar
            // new DateTime(year, 2, 14, hebrewCalendar),
            new DateTime(year, Iyar, 18, hebrewCalendar), // Lag BaOmer
            // Sivan
            new DateTime(year, Sivan, 5, hebrewCalendar), // Erev Shavuot
            new DateTime(year, Sivan, 6, hebrewCalendar), // Shavuot I
            new DateTime(year, Sivan, 7, hebrewCalendar), // Shavuot II
            //Tammuz
            new DateTime(year, Tamuz, 17, hebrewCalendar), // Tzom Tammuz
            //Av
            new DateTime(year, Av, 8, hebrewCalendar), // Erev Tish'a B'Av
            DelayIfShabat(new DateTime(year, Av, 9, hebrewCalendar)), // Tish'a B'Av
            //Elul
            new DateTime(year, Elul, 29, hebrewCalendar),
            // Tishrei
            new DateTime(year, Tishrei, 1, hebrewCalendar),
            new DateTime(year, Tishrei, 2, hebrewCalendar),
            TzomGedaliah(new DateTime(year, Tishrei, 3, hebrewCalendar)),
            new DateTime(year, Tishrei, 9, hebrewCalendar),
            new DateTime(year, Tishrei, 10, hebrewCalendar),
            new DateTime(year, Tishrei, 14, hebrewCalendar),
            new DateTime(year, Tishrei, 15, hebrewCalendar),
            new DateTime(year, Tishrei, 16, hebrewCalendar),
            new DateTime(year, Tishrei, 17, hebrewCalendar),
            new DateTime(year, Tishrei, 18, hebrewCalendar),
            new DateTime(year, Tishrei, 19, hebrewCalendar),
            new DateTime(year, Tishrei, 20, hebrewCalendar),
            new DateTime(year, Tishrei, 21, hebrewCalendar),
            new DateTime(year, Tishrei, 22, hebrewCalendar),
            new DateTime(year, Tishrei, 23, hebrewCalendar),
            //Cheshvan
            //Kislev
            //Tevet
            new DateTime(year, Tevet, 10, hebrewCalendar),
            //Shevat
            //Adar I
            new DateTime(year, Adar, 13, hebrewCalendar),
            new DateTime(year, Adar, 14, hebrewCalendar),
            new DateTime(year, Adar, 15, hebrewCalendar),
            //Adar / Adar II
        };
    }

    public List<DateTime> GetHolidaysForGregorianYear(DateTime year)
    {
        var hbYear = hebrewCalendar.GetYear(year);
        var list = Holidays(hbYear);
        list.AddRange(Holidays(hbYear + 1));
        return list.Where(x => x.Year == year.Year).OrderBy(x => x.Date).ToList();
    }


    // Ta'anit Esther -  13th day of Adar (12) at dawn (if Shabbat, then 11th day of Adar at dawn)
    private DateTime FastOfEsther(DateTime dateTime) => dateTime.DayOfWeek == DayOfWeek.Saturday
        ? new DateTime(dateTime.Year, dateTime.Month, 11)
        : dateTime;

    // One day before TishABav
    private bool IsErevTishABav(int day, int month, DayOfWeek dayOfWeek)
    {
        if (month != 5)
        {
            return false;
        }

        return day == 9 && dayOfWeek == DayOfWeek.Saturday || day == 8 && dayOfWeek != DayOfWeek.Friday;
    }

    // Tzom Gedaliah - 3 7 	3rd day of Tishrei at dawn (if Shabbat, then 4th day of Tishrei at dawn)
    private DateTime TzomGedaliah(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday ? date.AddDays(1) : date;
    private DateTime DelayIfShabat(DateTime date) => date.DayOfWeek == DayOfWeek.Saturday ? date.Date.AddDays(1) : date;
}