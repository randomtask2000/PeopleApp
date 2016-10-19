
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeopleApp.Filters.ActionFilters;
using PeopleApp.Filters.ExceptionFilters;
using PeopleApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace PeopleApp.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    [Route("api/[controller]")]
    public class RestController : Controller
    {
        private PeopleContext _context;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Settings _settings;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public RestController(ILoggerFactory loggerFactory
            , IHostingEnvironment hostingEnvironment
            , IOptions<Settings> settings
            , IConfiguration configuration
            , IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger("PeopleController");
            _hostingEnvironment = hostingEnvironment;
            _settings = settings.Value;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _context = new PeopleContext(_serviceProvider.GetRequiredService<DbContextOptions<PeopleContext>>());
        }

        [ServiceFilter(typeof(LoggingExceptionFilter))]
        [ServiceFilter(typeof(LogActionFilter))]
        [HttpGet]
        public IQueryable<Person> GetPeople()
        {
            return _context.People.Take(10); //todo: paging
        }
    }
}
