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
    public class BeetRootAcademyParser : ICourseParser
    {
        private readonly string _url;
        private HtmlDocument? document;
        public BeetRootAcademyParser()
        {
            _url = "https://beetroot.academy/courses/online";
        }
        public async Task<IEnumerable<Course>> Parse()
        {
            try
            {
                var web = new HtmlWeb();
                document = await web.LoadFromWebAsync(_url);
                var info = document.DocumentNode.SelectNodes("//a[(contains(@class, 'intro_box'))]");
                List<Course> courses = new List<Course>();

                foreach (var item in info)
                {
                    courses.Add(new Course()
                    {
                        Name = item.FirstChild.FirstChild.InnerText,
                        Description = item.FirstChild.LastChild.InnerText,
                        Link = $"{_url}{item.GetAttributeValue("href", string.Empty)}"
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
