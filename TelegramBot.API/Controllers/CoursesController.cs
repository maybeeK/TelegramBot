using Microsoft.AspNetCore.Mvc;
using TelegramBot.API.Services.Interfaces;

namespace TelegramBot.API.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
    }
}
