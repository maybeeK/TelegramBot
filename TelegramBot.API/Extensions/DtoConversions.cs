using TelegramBot.API.Entities;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Extensions
{
    public static class DtoConversions
    {
        public static CourseDto ConvertToDto(this Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Link = course.Link
            };
        }

        public static IEnumerable<CourseDto> ConvertToDto(this IEnumerable<Course> courses)
        {
            return (from course in courses
                   select new CourseDto { 
                        Id = course.Id,
                        Name = course.Name,
                        Description = course.Description,
                        Link = course.Link
                   }).ToList();
        }

        public static IEnumerable<UserTagDto> ConvertToDto(this IEnumerable<UserTag> userTags)
        {
            return (from userTag in userTags
                    select new UserTagDto
                    {
                        Id = userTag.Id,
                        UsertId= userTag.UsertId,
                        Tag = userTag.Tag
                    }).ToList();
        }
    }
}
