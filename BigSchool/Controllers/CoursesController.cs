using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categorie.ToList(),
                Heading = "Add Course",
            };
            return View(viewModel);
        }
        // GET: Courses
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categorie.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                Place = viewModel.Place,
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
            };

            _dbContext.Course.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Detail(int id)
        {

            var userId = User.Identity.GetUserId();
            var viewModel = new CourseViewModel();
            var course = _dbContext.Course.Where(c => c.Id == id).Include(a => a.Lecturer).Include(a => a.Category);

            var listOfAttendedCourses = _dbContext.Attendance
                    .Include(a => a.Course)
                    .Include(a => a.Attendee)
                    .Where(a => a.AttendeeId == userId).ToList();

            viewModel = new CourseViewModel
            {
                ListOfAttendedCourses = listOfAttendedCourses,
                UpCommingCourses = course,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var listOfAttendedCourses = _dbContext.Attendance
               .Include(a => a.Course)
               .Include(a => a.Attendee)
               .Where(a => a.AttendeeId == userId).ToList();

            var followingLecturers = _dbContext.Following
               .Include(f => f.Followee)
               .Include(f => f.Follower)
               .Where(a => a.FollowerId == userId)
               .ToList();

            var courses = _dbContext.Attendance
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            var viewModel = new CourseViewModel
            {
                ListOfAttendedCourses = listOfAttendedCourses,
                ListOfFollowings = followingLecturers,
                UpCommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Course
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            return View(courses);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.Id == id && c.LecturerId == userId);
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categorie.ToList(),
                Date = course.DateTime.ToString("dd/MM/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place,
                Heading = "Edit Course",
                Id = course.Id
            };
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categorie.ToList();
                return View("Create", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Course.Single(c => c.Id == viewModel.Id && c.LecturerId == userId);

            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



    }
}