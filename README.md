# SolidExpert.HebrewToGregorian

`SolidExpert.HebrewToGregorian` is a lightweight .NET library that converts Hebrew calendar holidays into their corresponding Gregorian dates. It ships with NUnit tests and a GitHub Actions workflow that can automatically pack and publish the library to NuGet.

## Getting Started

```bash
dotnet add package SolidExpert.HebrewToGregorian
```

```csharp
using SolidExpert.HebrewToGregorian;

var holidays = new HBHolidays();
var gregorianDates = holidays.GetHolidaysForGregorianYear(new DateTime(2024, 1, 1));
```

## Features

- Generate the full list of Yomim Tovim and fast days that fall within any Gregorian year.
- Convert any Hebrew date (`year-month-day`) into its Gregorian equivalent (and vice versa).
- Perform Hebrew date arithmetic (add/subtract days, compute day spans) without reinventing calendar math.
- Enumerate inclusive Gregorian ranges and inspect the Hebrew representation day by day.
- Inspect month-level metadata (names, lengths, boundaries) for any Hebrew year.

## Usage Examples

```csharp
var holidays = new HBHolidays();
var converter = new HebrewGregorianConverter();

var upcoming = holidays.GetHolidaysForGregorianYear(new DateTime(2025, 1, 1));

var hebrew = converter.ToHebrew(new DateTime(2025, 10, 3));           // 5786-1-10
var gregorian = converter.ToGregorian(5786, 1, 15);                   // 2025-10-08
var plusFiveDays = converter.AddDays(new HebrewDate(5786, 1, 10), 5); // 5786-1-15
var span = converter.DaysBetween(new HebrewDate(5786, 1, 1), new HebrewDate(5786, 1, 10)); // 9

var months = converter.GetYearMonths(5786);
var tishrei = months.First();                         // HebrewMonthInfo
var adar = months.First(m => m.Name.StartsWith("Adar"));
```

## Projects

- `src/SolidExpert.HebrewToGregorian` – main library
- `tests/SolidExpert.HebrewToGregorian.Tests` – NUnit-based test suite

Build and test everything:

```bash
dotnet test
```

## Publishing to NuGet

Publishing is automated through the GitHub Actions workflow defined in `.github/workflows/nuget-publish.yml`. To publish:

1. Set the `NUGET_API_KEY` secret in the GitHub repository.
2. Trigger the workflow manually with a version (or push a tag like `v1.2.3`).
3. The workflow restores, builds, packs, and pushes the `.nupkg` to NuGet, skipping duplicates.

You can also publish locally:

```bash
dotnet pack src/SolidExpert.HebrewToGregorian/SolidExpert.HebrewToGregorian.csproj -c Release -o ./.artifacts
dotnet nuget push ./.artifacts/*.nupkg --api-key <key> --source https://api.nuget.org/v3/index.json
```

## License

MIT © 2025 Solidexpert LTD

