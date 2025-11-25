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

