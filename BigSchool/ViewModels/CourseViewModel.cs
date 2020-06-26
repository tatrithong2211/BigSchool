using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Place { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [ValidTime]
        public string Time { get; set; }
        [Required]
        public byte Category { get; set; }
        public string Heading { get; set; }
        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Course> UpCommingCourses { get; set; }

        public string searching { get; set; }
        public List<Attendance> ListOfAttendedCourses { get; set; }

        public List<Following> ListOfFollowings { get; set; }
        public List<Following> ListOfFollowers { get; set; }



        public bool ShowAction { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}