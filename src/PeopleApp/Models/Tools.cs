using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApp.Models
{
    public class Tools
    {
        public void GetImage(Uri url)
        {
            
        }

        private Random _gen = new Random();
        private DateTime _start = new DateTime(1973, 1, 1);
        private int _range = (DateTime.Today - new DateTime(1973, 1, 1)).Days;
        public DateTime RandomDay()
        {
            return _start.AddDays(_gen.Next(_range));
        }
    }
}
