using HtmlAgilityPack;
using TelegramBot.API.CourseParsers.Interfaces;
using TelegramBot.API.Entities;

namespace TelegramBot.API.CourseParsers
{
    public class ITeaParser:ICourseParser
    {
        private readonly string _url;
        private HtmlDocument? document;
        public ITeaParser()
        {
            _url = "https://onlineitea.com.ua/courses-list/";
        }
        public async Task<IEnumerable<Course>> Parse()
        {
            try
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync(_url);
                var info = document.DocumentNode.SelectNodes("//div[@class='card__main']");
                List<Course> courses = new List<Course>();

                foreach (var item in info)
                {
                    courses.Add(new Course()
                    {
                        Name = item.FirstChild.InnerText,
                        Description = item.LastChild.InnerText
                    });
                    //courses.Add(new CourseInfo($"{item.FirstChild.InnerText}", $"{item.LastChild.InnerText}", $""));
                }

                info = document.DocumentNode.SelectNodes("//a[@class='card']");
                int i = 0;
                foreach (var item in info)
                {
                    courses[i++].Link = item.GetAttributeValue("href", string.Empty);
                }

                return courses;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Course>();
            }
        }
    }
}
