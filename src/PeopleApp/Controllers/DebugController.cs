using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeopleApp.Filters.ActionFilters;
using PeopleApp.Filters.ExceptionFilters;
using PeopleApp.Models;

namespace PeopleApp.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class DebugController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Settings _settings;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public DebugController(ILoggerFactory loggerFactory
            , IHostingEnvironment hostingEnvironment
            , IOptions<Settings> settings
            , IConfiguration configuration
            , IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger("DebugController");
            _hostingEnvironment = hostingEnvironment;
            _settings = settings.Value;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        [ServiceFilter(typeof(LoggingExceptionFilter))]
        [ServiceFilter(typeof(LogActionFilter))]
        public ActionResult Debug()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string dataPath = System.IO.Path.Combine(_hostingEnvironment.ContentRootPath, "Data");
            string databaseFile = _configuration.GetConnectionString("Database");
            string dbPath = System.IO.Path.Combine(dataPath, databaseFile);
            var connection = _configuration.GetConnectionString("DefaultConnection");

            // Print configuration settings
            return Content(webRootPath + "\n" 
                + contentRootPath + "\n"
                + dataPath + "\n"
                + dbPath + "\n"
                + connection);
        }

        [ServiceFilter(typeof(LoggingExceptionFilter))]
        [ServiceFilter(typeof(LogActionFilter))]
        public ActionResult SeedData()
        {
            int rowsInserted = 0;
            PeopleApp.Models.SeedData.Initialize(_serviceProvider, _hostingEnvironment, out rowsInserted);
            return Content("Number of rows inserted: "+ rowsInserted.ToString());
        }
    }
}
