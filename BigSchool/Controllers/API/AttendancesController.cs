﻿
using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();

        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendance.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
            {
                var attendance = new Attendance
                {
                    CourseId = attendanceDto.CourseId,
                    AttendeeId = userId
                };

                _dbContext.Attendance.Attach(attendance);
                _dbContext.Attendance.Remove(attendance);
                _dbContext.SaveChanges();

                return Ok();
            }
            else
            {
                var attendance = new Attendance
                {
                    CourseId = attendanceDto.CourseId,
                    AttendeeId = userId
                };

                _dbContext.Attendance.Add(attendance);
                _dbContext.SaveChanges();
                return Ok();
            }

        }
    }
}
