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

