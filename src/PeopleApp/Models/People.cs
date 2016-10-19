using System;
using System.ComponentModel.DataAnnotations.Schema;
using EnPeople.Models;

namespace PeopleApp.Models
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string ImageFilename { get; set; }
        [NotMapped]
        public int Age
        {
            get { return Birthdate.GetAge(); }
        }
        [NotMapped]
        public string FLF// Means FullNameLastNameFirst. More data but faster searching
        {
            get { return LastName + " " + FirstName; }
        }
        [NotMapped]
        public string FFF// Means FullNameFirstNameFirst. Search index
        {
            get { return FirstName + " " + LastName; }
        }
    }

    public enum Gender
    {
        Male = 0, Female = 1,
    }
}