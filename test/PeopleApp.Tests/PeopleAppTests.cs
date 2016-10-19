using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using PeopleApp.Controllers;
using PeopleApp.Filters;
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
        [InlineData("PeopleApp1")]
        [InlineData("SomeApp")]
        public void ApplicationNameTest(string value)
        {
            var result = _appSettings.ApplicationName;
            Assert.False(result.Contains(value), $"{value} should not have been the correct application name");
        }
    }
}
