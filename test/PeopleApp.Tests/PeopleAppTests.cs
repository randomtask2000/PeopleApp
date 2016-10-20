using System;
using System.Globalization;
using PeopleApp.Models;
using Xunit;

namespace PeopleApp.Tests
{
    public class PeopleAppTests
    {
        private readonly Settings _appSettings;

        public PeopleAppTests()
        {
            _appSettings = new Settings();
        }
        [Theory]
        [InlineData("1973-01-01")]
        [InlineData("1995-01-01")]
        public void GetAgeTest(string sBirthDate)
        {
            DateTime date;
            if (DateTime.TryParseExact(sBirthDate, "yyyy-MM-dd",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out date))
            {
                Assert.True(date.GetAge() > 0, $"Age should be greater than zero");
                Assert.True(date.GetAge() > 20, $"Age should be greater than 20");
            }
            else
            {
                throw new Exception("Test scenario did not conform to DateTime mask");
            }
        }
        [Theory]
        [InlineData("PeopleApp1")]
        [InlineData("SomeApp")]
        public void ApplicationNameTest(string value)
        {
            var result = _appSettings.ApplicationName;
            Assert.False(result.Contains(value), $"{value} should not have been the correct application name");
        }
    }
}
