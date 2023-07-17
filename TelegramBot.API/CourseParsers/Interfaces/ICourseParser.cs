using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.Entities;

namespace TelegramBot.API.CourseParsers.Interfaces
{
    public interface ICourseParser
    {
        Task<IEnumerable<Course>> Parse();
    }
}
