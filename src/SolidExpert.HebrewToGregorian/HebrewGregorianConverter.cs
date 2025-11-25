using System;
using System.Collections.Generic;
using System.Globalization;

namespace SolidExpert.HebrewToGregorian;

/// <summary>
/// Provides helper methods for converting between Hebrew and Gregorian calendars.
/// </summary>
public class HebrewGregorianConverter
{
    private readonly HebrewCalendar _hebrewCalendar = new();

    /// <summary>
    /// Converts a Hebrew date into a Gregorian <see cref="DateTime"/>.
    /// </summary>
    public DateTime ToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay)
    {
        return _hebrewCalendar.ToDateTime(hebrewYear, hebrewMonth, hebrewDay, 0, 0, 0, 0, Calendar.CurrentEra);
    }

    /// <summary>
    /// Converts a Hebrew date value into Gregorian time.
    /// </summary>
    public DateTime ToGregorian(HebrewDate hebrewDate) => ToGregorian(hebrewDate.Year, hebrewDate.Month, hebrewDate.Day);

    /// <summary>
    /// Converts a Gregorian <see cref="DateTime"/> into a Hebrew calendar triple.
    /// </summary>
    public HebrewDate ToHebrew(DateTime gregorianDate)
    {
        return new HebrewDate(
            _hebrewCalendar.GetYear(gregorianDate),
            _hebrewCalendar.GetMonth(gregorianDate),
            _hebrewCalendar.GetDayOfMonth(gregorianDate));
    }

    /// <summary>
    /// Enumerates inclusive Gregorian ranges with their Hebrew representations.
    /// </summary>
    public IEnumerable<(DateTime gregorian, HebrewDate hebrew)> Range(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("End date must not be before start date.", nameof(endDate));
        }

        for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            yield return (date, ToHebrew(date));
        }
    }

    /// <summary>
    /// Adds (or subtracts) days from a given Hebrew date by leveraging Gregorian arithmetic.
    /// </summary>
    public HebrewDate AddDays(HebrewDate hebrewDate, int days)
    {
        var gregorian = ToGregorian(hebrewDate);
        return ToHebrew(gregorian.AddDays(days));
    }

    /// <summary>
    /// Returns the signed number of days between two Hebrew dates.
    /// </summary>
    public int DaysBetween(HebrewDate start, HebrewDate end)
    {
        var startGregorian = ToGregorian(start);
        var endGregorian = ToGregorian(end);
        return (endGregorian - startGregorian).Days;
    }

    /// <summary>
    /// Provides metadata for a specific Hebrew month (name, length, boundaries).
    /// </summary>
    public HebrewMonthInfo GetMonthInfo(int hebrewYear, int hebrewMonth) =>
        HebrewMonthCatalog.BuildMonthInfo(_hebrewCalendar, hebrewYear, hebrewMonth);

    /// <summary>
    /// Returns every month that exists within the given Hebrew year.
    /// </summary>
    public IReadOnlyList<HebrewMonthInfo> GetYearMonths(int hebrewYear) =>
        HebrewMonthCatalog.BuildMonthsForYear(_hebrewCalendar, hebrewYear);
}

/// <summary>
/// Lightweight representation of a Hebrew calendar date.
/// </summary>
public readonly record struct HebrewDate(int Year, int Month, int Day)
{
    public string GetMonthName() => HebrewMonthCatalog.GetMonthName(Year, Month);

    public bool IsLeapYear => HebrewMonthCatalog.IsLeapYear(Year);

    public int DaysInMonth => HebrewMonthCatalog.GetDaysInMonth(Year, Month);

    public HebrewDate StartOfMonth => new(Year, Month, 1);

    public HebrewDate EndOfMonth => new(Year, Month, DaysInMonth);

    public HebrewMonthInfo GetMonthInfo() => HebrewMonthCatalog.BuildMonthInfo(Year, Month);

    public override string ToString() => $"{Year}-{Month}-{Day}";
}

/// <summary>
/// Represents descriptive information about a Hebrew month.
/// </summary>
public readonly record struct HebrewMonthInfo(int Year, int MonthNumber, string Name, int Days)
{
    public HebrewDate Start => new(Year, MonthNumber, 1);
    public HebrewDate End => new(Year, MonthNumber, Days);
    public override string ToString() => $"{Name} {Year} ({Days} days)";
}

internal static class HebrewMonthCatalog
{
    private static readonly string[] CommonYearMonths =
    {
        "Tishrei",
        "Cheshvan",
        "Kislev",
        "Tevet",
        "Shevat",
        "Adar",
        "Nisan",
        "Iyar",
        "Sivan",
        "Tamuz",
        "Av",
        "Elul"
    };

    private static readonly string[] LeapYearMonths =
    {
        "Tishrei",
        "Cheshvan",
        "Kislev",
        "Tevet",
        "Shevat",
        "Adar I",
        "Adar II",
        "Nisan",
        "Iyar",
        "Sivan",
        "Tamuz",
        "Av",
        "Elul"
    };

    public static bool IsLeapYear(int year, HebrewCalendar? calendar = null)
    {
        calendar ??= new HebrewCalendar();
        return calendar.IsLeapYear(year);
    }

    public static string GetMonthName(int year, int month) =>
        GetMonthName(new HebrewCalendar(), year, month);

    public static string GetMonthName(HebrewCalendar calendar, int year, int month)
    {
        ValidateMonth(calendar, year, month);
        var catalog = calendar.IsLeapYear(year) ? LeapYearMonths : CommonYearMonths;
        return catalog[month - 1];
    }

    public static int GetDaysInMonth(int year, int month) =>
        GetDaysInMonth(new HebrewCalendar(), year, month);

    public static int GetDaysInMonth(HebrewCalendar calendar, int year, int month)
    {
        ValidateMonth(calendar, year, month);
        return calendar.GetDaysInMonth(year, month);
    }

    public static HebrewMonthInfo BuildMonthInfo(int year, int month) =>
        BuildMonthInfo(new HebrewCalendar(), year, month);

    public static HebrewMonthInfo BuildMonthInfo(HebrewCalendar calendar, int year, int month)
    {
        var name = GetMonthName(calendar, year, month);
        var days = GetDaysInMonth(calendar, year, month);
        return new HebrewMonthInfo(year, month, name, days);
    }

    public static IReadOnlyList<HebrewMonthInfo> BuildMonthsForYear(int year) =>
        BuildMonthsForYear(new HebrewCalendar(), year);

    public static IReadOnlyList<HebrewMonthInfo> BuildMonthsForYear(HebrewCalendar calendar, int year)
    {
        var totalMonths = calendar.GetMonthsInYear(year);
        var months = new List<HebrewMonthInfo>(totalMonths);
        for (var month = 1; month <= totalMonths; month++)
        {
            months.Add(BuildMonthInfo(calendar, year, month));
        }

        return months;
    }

    private static void ValidateMonth(HebrewCalendar calendar, int year, int month)
    {
        var maxMonth = calendar.GetMonthsInYear(year);
        if (month < 1 || month > maxMonth)
        {
            throw new ArgumentOutOfRangeException(nameof(month),
                $"Month must be between 1 and {maxMonth} for Hebrew year {year}.");
        }
    }
}

