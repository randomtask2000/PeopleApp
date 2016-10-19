using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace PeopleApp.Models
{
    public class Settings
    {
        public string ApplicationName { get; set; } = "PeopleApp";
        public const string Author = "Emilio Nicoli";
    }
}
