using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.CourseParsers.Interfaces;
using TelegramBot.API.Entities;

namespace TelegramBot.API.CourseParsers
{
    public class MateAcademyParser:ICourseParser
    {
        private readonly string _url;
        private HtmlDocument? document;
        public MateAcademyParser()
        {
            _url = "https://mate.academy";
        }
        public async Task<IEnumerable<Course>> Parse()
        {
            try
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync(_url);
                var info = document.DocumentNode.SelectNodes("//section[@class='CourseCard_cardContainer__7_4lK']");
                List<Course> courses = new List<Course>();

                foreach (var item in info)
                {
                    courses.Add(new Course()
                    {
                        Name = item.FirstChild.InnerText,
                        Description = item.LastChild.FirstChild.InnerText,
                        Link = $"{_url}{item.FirstChild.GetAttributeValue("href", string.Empty)}"
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
