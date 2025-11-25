// See https://aka.ms/new-console-template for more information

using SolidExpert.HebrewToGregorian;

HBHolidays holidays = new();
var converter = new HebrewGregorianConverter();

Console.WriteLine("Major holidays in Gregorian year 2023:");
foreach (var dateTime in holidays.GetHolidaysForGregorianYear(new DateTime(2023, 1, 1)))
{
    var hebrew = converter.ToHebrew(dateTime);
    Console.WriteLine($"GR: {dateTime:yyyy-MM-dd} -> HB: {hebrew}");
}

Console.WriteLine();
Console.WriteLine("Sample conversion flow:");

var hebrewDate = new HebrewDate(5784, 7, 15);
var gregorian = converter.ToGregorian(hebrewDate.Year, hebrewDate.Month, hebrewDate.Day);
Console.WriteLine($"HB {hebrewDate} converts to GR {gregorian:yyyy-MM-dd}");

var backToHebrew = converter.ToHebrew(new DateTime(2024, 10, 3));
Console.WriteLine($"GR 2024-10-03 converts to HB {backToHebrew}");

var shifted = converter.AddDays(backToHebrew, 5);
Console.WriteLine($"HB {backToHebrew} plus 5 days => HB {shifted}");

var span = converter.DaysBetween(new HebrewDate(5784, 1, 1), new HebrewDate(5784, 1, 10));
Console.WriteLine($"Days between Rosh Hashanah 5784 and Yom Kippur 5784: {span}");

Console.WriteLine();
Console.WriteLine("Month overview for Hebrew year 5784:");
foreach (var month in converter.GetYearMonths(5784))
{
    var monthInfo = month;
    Console.WriteLine($"{monthInfo.Name}: {monthInfo.Days} days (start {converter.ToGregorian(monthInfo.Start):yyyy-MM-dd})");
}

