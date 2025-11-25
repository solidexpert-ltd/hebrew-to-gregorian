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
}