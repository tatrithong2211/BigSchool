using BigSchool.Models;
using BigSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public ActionResult Index()
        {

            var userId = User.Identity.GetUserId();

            var listOfAttendedCourses = _dbContext.Attendance
                .Include(a => a.Course)
                .Include(a => a.Attendee)
                .Where(a => a.AttendeeId == userId).ToList();

            var upcommingCourses = _dbContext.Course
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now).ToList();

            var followingLecturers = _dbContext.Followings
                    .Include(f => f.Followee)
                    .Include(f => f.Follower)
                    .Where(a => a.FollowerId == userId)
                    .ToList();

            var viewModel = new CourseViewModel
            {
                ListOfAttendedCourses = listOfAttendedCourses,
                ListOfFollowings = followingLecturers,
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}