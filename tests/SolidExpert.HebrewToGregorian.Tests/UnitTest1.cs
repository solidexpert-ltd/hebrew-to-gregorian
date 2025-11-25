using SolidExpert.HebrewToGregorian;

namespace TestProject1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldContainsExpectedDates()
    {
        var hb = new HBHolidays();

     var dates =    hb.GetHolidaysForGregorianYear(new DateTime(2023,1,1)).OrderBy(x=>x.Date).ToList();

        var expected = new[]
        {
            new DateTime(2023, 01, 03),
            new DateTime(2023, 03, 06),
            new DateTime(2023, 03, 07),
            new DateTime(2023, 03, 08),
            new DateTime(2023, 04, 05),
            new DateTime(2023, 04, 06),
            new DateTime(2023, 04, 07),
            new DateTime(2023, 04, 08),
            new DateTime(2023, 04, 09),
            new DateTime(2023, 04, 10),
            new DateTime(2023, 04, 11),
            new DateTime(2023, 04, 12),
            new DateTime(2023, 04, 13),
            new DateTime(2023, 05, 09),
            new DateTime(2023, 05, 25),
            new DateTime(2023, 05, 26),
            new DateTime(2023, 05, 27),
            new DateTime(2023, 07, 06),
            new DateTime(2023, 07, 26),
            new DateTime(2023, 07, 27),
            new DateTime(2023, 09, 15),
            new DateTime(2023, 09, 16),
            new DateTime(2023, 09, 17),
            new DateTime(2023, 09, 18),
            new DateTime(2023, 09, 24),
            new DateTime(2023, 09, 25),
            new DateTime(2023, 09, 29),
            new DateTime(2023, 09, 30),
            new DateTime(2023, 10, 01),
            new DateTime(2023, 10, 02),
            new DateTime(2023, 10, 03),
            new DateTime(2023, 10, 04),
            new DateTime(2023, 10, 05),
            new DateTime(2023, 10, 06),
            new DateTime(2023, 10, 07),
            new DateTime(2023, 10, 08),
            new DateTime(2023, 12, 22)
        };
        
        foreach (var dateTime in expected)
        {
            Assert.True(dates.Contains(dateTime));
        }
    }

    [Test]
    public void ShouldConvertHebrewToGregorianAndBack()
    {
        var converter = new HebrewGregorianConverter();

        var hebrewDate = new HebrewDate(5783, 7, 15); // 15 Tishrei 5783 -> Oct 10 2022
        var gregorian = converter.ToGregorian(hebrewDate.Year, hebrewDate.Month, hebrewDate.Day);

        Assert.That(gregorian, Is.EqualTo(new DateTime(2022, 10, 10)));

        var hebrewBack = converter.ToHebrew(gregorian);
        Assert.That(hebrewBack, Is.EqualTo(hebrewDate));
    }

    [Test]
    public void ShouldEnumerateRangeWithHebrewDates()
    {
        var converter = new HebrewGregorianConverter();
        var results = converter.Range(new DateTime(2023, 9, 15), new DateTime(2023, 9, 17)).ToList();

        Assert.That(results.Count, Is.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(results[0].gregorian, Is.EqualTo(new DateTime(2023, 9, 15)));
            Assert.That(results[0].hebrew, Is.EqualTo(new HebrewDate(5784, 1, 1)));
            Assert.That(results[2].gregorian, Is.EqualTo(new DateTime(2023, 9, 17)));
            Assert.That(results[2].hebrew, Is.EqualTo(new HebrewDate(5784, 1, 3)));
        });
    }

    [Test]
    public void ShouldAddDaysAcrossHebrewDates()
    {
        var converter = new HebrewGregorianConverter();
        var sukkot = new HebrewDate(5784, 1, 15); // 15 Tishrei 5784

        var afterWeek = converter.AddDays(sukkot, 7);
        var beforeFast = converter.AddDays(sukkot, -1);

        Assert.Multiple(() =>
        {
            Assert.That(afterWeek, Is.EqualTo(new HebrewDate(5784, 1, 22)));
            Assert.That(beforeFast, Is.EqualTo(new HebrewDate(5784, 1, 14)));
        });
    }

    [Test]
    public void ShouldMeasureDaysBetweenHebrewDates()
    {
        var converter = new HebrewGregorianConverter();
        var roshHashana = new HebrewDate(5784, 1, 1);
        var yomKippur = new HebrewDate(5784, 1, 10);

        Assert.Multiple(() =>
        {
            Assert.That(converter.DaysBetween(roshHashana, yomKippur), Is.EqualTo(9));
            Assert.That(converter.DaysBetween(yomKippur, roshHashana), Is.EqualTo(-9));
        });
    }

    [Test]
    public void HebrewDateShouldExposeMonthMetadata()
    {
        var date = new HebrewDate(5784, 6, 10); // Adar I in a leap year
        var info = date.GetMonthInfo();

        Assert.Multiple(() =>
        {
            Assert.That(date.IsLeapYear, Is.True);
            Assert.That(date.GetMonthName(), Is.EqualTo("Adar I"));
            Assert.That(info.Name, Is.EqualTo("Adar I"));
            Assert.That(info.Days, Is.EqualTo(date.DaysInMonth));
            Assert.That(info.Start, Is.EqualTo(date.StartOfMonth));
            Assert.That(info.End.Day, Is.EqualTo(date.EndOfMonth.Day));
        });
    }

    [Test]
    public void ShouldProvideMonthInfoForEntireYear()
    {
        var converter = new HebrewGregorianConverter();
        var months = converter.GetYearMonths(5784);

        Assert.Multiple(() =>
        {
            Assert.That(months.Count, Is.EqualTo(13));
            Assert.That(months[0].Name, Is.EqualTo("Tishrei"));
            Assert.That(months[5].Name, Is.EqualTo("Adar I"));
            Assert.That(months[6].Name, Is.EqualTo("Adar II"));
        });
    }
}