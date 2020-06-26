using BigSchool.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
////using Korzh.EasyQuery.Linq;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index(string searching)
        {
            var viewModel = new CourseViewModel();
            if (searching == null)
            {
                var userId = User.Identity.GetUserId();
                var listOfAttendedCourses = _dbContext.Attendance
                    .Include(a => a.Course)
                    .Include(a => a.Attendee)
                    .Where(a => a.AttendeeId == userId).ToList();

                var upCommingCourses = _dbContext.Course
                    .Include(c => c.Lecturer)
                    .Include(c => c.Category)
                    .Where(c => c.DateTime > DateTime.Now).ToList();

                var followingLecturers = _dbContext.Following
                    .Include(f => f.Followee)
                    .Include(f => f.Follower)
                    .Where(a => a.FollowerId == userId)
                    .ToList();

                viewModel = new CourseViewModel
                {
                    ListOfAttendedCourses = listOfAttendedCourses,
                    ListOfFollowings = followingLecturers,
                    UpCommingCourses = upCommingCourses,
                    ShowAction = User.Identity.IsAuthenticated
                };
                return View(viewModel);
            }
            else
            {
                var userId = User.Identity.GetUserId();
                var listOfAttendedCourses = _dbContext.Attendance
                    .Include(a => a.Course)
                    .Include(a => a.Attendee)
                    .Where(a => a.AttendeeId == userId).ToList();

                var upCommingCourses = _dbContext.Course
                    .Include(c => c.Lecturer)
                    .Include(c => c.Category)
                    .Where(c => c.Lecturer.Name.Contains(searching) || c.Category.Name.Contains(searching)).ToList();

                var followingLecturers = _dbContext.Following
                    .Include(f => f.Followee)
                    .Include(f => f.Follower)
                    .Where(a => a.FollowerId == userId)
                    .ToList();

                viewModel = new CourseViewModel
                {
                    ListOfAttendedCourses = listOfAttendedCourses,
                    ListOfFollowings = followingLecturers,
                    UpCommingCourses = upCommingCourses,
                    ShowAction = User.Identity.IsAuthenticated
                };
                return View(viewModel);
            }


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