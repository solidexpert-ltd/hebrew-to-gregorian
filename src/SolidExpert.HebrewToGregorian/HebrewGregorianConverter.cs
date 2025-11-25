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
}

/// <summary>
/// Lightweight representation of a Hebrew calendar date.
/// </summary>
public readonly record struct HebrewDate(int Year, int Month, int Day)
{
    public override string ToString() => $"{Year}-{Month}-{Day}";
}

