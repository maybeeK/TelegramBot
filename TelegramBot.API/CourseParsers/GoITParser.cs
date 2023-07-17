using HtmlAgilityPack;
using TelegramBot.API.CourseParsers.Interfaces;
using TelegramBot.API.Entities;

namespace TelegramBot.API.CourseParsers
{
    public class GoITParser:ICourseParser
    {
        private readonly string _url;
        private HtmlDocument? document;
        public GoITParser()
        {
            _url = "https://goit.global/ua/courses/";
        }
        public async Task<IEnumerable<Course>> Parse()
        {
            try
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync(_url);
                var info = document.DocumentNode.SelectNodes("//div[@class='grid items-end content-between h-full']");
                List<Course> courses = new List<Course>();

                foreach (var item in info)
                {
                    courses.Add(new Course()
                    {
                        Name = item.Element("h3").InnerText.Trim(' ', '\n'),
                        Description = item.Element("p").InnerText.Trim(' ', '\n'),
                        Link = item.Element("a").GetAttributeValue("href", string.Empty)
                    });
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
