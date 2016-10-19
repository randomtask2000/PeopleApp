using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EnPeople.Models;
using Microsoft.AspNetCore.Hosting;

namespace PeopleApp.Models
{
    /// <summary>
    /// This method can be used to seed the data base with sample data.  
    /// The names are being being generated from the 1990 US sensus
    /// </summary>
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, IHostingEnvironment hostingEnvironment, out int rowsInserted)
        {
            rowsInserted = 0;
            string contentRootPath = hostingEnvironment.ContentRootPath;
            string dataPath = System.IO.Path.Combine(contentRootPath, "Data");
            string femaleNamePath = System.IO.Path.Combine(dataPath, "names-female.txt");
            string maleNamePath = System.IO.Path.Combine(dataPath, "names-male.txt");
            string lastnamesPath = System.IO.Path.Combine(dataPath, "names-last.txt");

            if (!Directory.Exists(dataPath))
            {
                throw new DirectoryNotFoundException("Exception in SeedData.Initialize - directory does not exist: " + System.IO.Path.Combine(contentRootPath, "Data"));
            }
            if (!File.Exists(femaleNamePath))
            {
                throw new FileNotFoundException("Exception in SeedData.Initialize - file does not exist: " + femaleNamePath);
            }
            if (!File.Exists(maleNamePath))
            {
                throw new FileNotFoundException("Exception in SeedData.Initialize - file does not exist: " + maleNamePath);
            }
            if (!File.Exists(lastnamesPath))
            {
                throw new FileNotFoundException("Exception in SeedData.Initialize - file does not exist: " + lastnamesPath);
            }

            using (var context = new PeopleContext(serviceProvider.GetRequiredService<DbContextOptions<PeopleContext>>()))
            {
                List<string> maleNameList = new List<string>(File.ReadAllLines(maleNamePath));
                List<string> femaleNameList = new List<string>(File.ReadAllLines(femaleNamePath)); 
                List<string> lastNameList = new List<string>(File.ReadAllLines(lastnamesPath));

                // Delete data first - there is no easy way of truncating in EntityFrameworkCore (yet) 
                if (context.People.Any()) 
                {
                    var query = from c in context.People select c;
                    if (query.Count() > 0)
                    {
                        context.People.RemoveRange(query);
                        context.SaveChanges();
                    }
                }

                Tools tools = new Tools();
                int imageId = 1;
                int i = 0;
                for (; i < femaleNameList.Count; i++, imageId++) // female names happens to be the shorter list
                {
                    context.People.Add(
                        new Person
                        {
                            LastName = lastNameList[i].InitialCap(),
                            FirstName = (i % 2 == 0 ? femaleNameList[i] : maleNameList[i]).InitialCap(),
                            Birthdate =  tools.RandomDay(),
                            Gender = i % 2 == 0 ? Gender.Female : Gender.Male,
                            ImageFilename = string.Format("image{0}.gif", imageId)
                        }
                    );
                    if (imageId > 9) imageId = 0;
                }
                context.SaveChanges();
                rowsInserted = i;
            }
        }
    }
}