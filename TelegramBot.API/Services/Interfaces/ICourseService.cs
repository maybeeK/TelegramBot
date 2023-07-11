﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.Entities;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<IEnumerable<Course>> GetCoursesByTag(string tag);
        Task<Course> GetCourse(int id);
        Task<Course> AddCourse(CourseDto newCourse);
        Task<Course> DeleteCourse(int id);
    }
}
