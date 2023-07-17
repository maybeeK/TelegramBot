using TelegramBot.API.CourseParsers.Interfaces;
using TelegramBot.API.Entities;
using TelegramBot.API.Extensions;
using TelegramBot.API.Services;
using TelegramBot.API.Services.Interfaces;

namespace TelegramBot.API.BackgroundServices
{
    public class CoursesBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ICourseParser> _courseParsers;
        private List<Course> _parsedCourses;
        private List<Course> _dbSavedCourses;
        public CoursesBackgroundService(IServiceProvider serviceProvider)
        {
            var type = typeof(ICourseParser);

            var parserTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            _courseParsers = parserTypes.Select(e => (ICourseParser)Activator.CreateInstance(e));
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        ICourseService _courseService = scope.ServiceProvider.GetRequiredService<ICourseService>();

                        await UpdateParsedCourses();

                        await UpdateSavedDbCourses(_courseService);

                        await DeleteOldCourses(_courseService);

                        await AddNewParsedCourses(_courseService);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Some problem in BG service!");
                    throw;
                }
                await Task.Delay(TimeSpan.FromHours(24));
            }
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping BG Service!");
            return base.StopAsync(cancellationToken);
        }
        private async Task AddNewParsedCourses(ICourseService _courseService)
        {
            foreach (var item in _parsedCourses)
            {
                if (ShouldCourseBeAddedToDb(item))
                {
                    await _courseService.AddCourse(item.ConvertToDto());
                }
            }
        }
        private async Task DeleteOldCourses(ICourseService courseService)
        {
            foreach (var item in _dbSavedCourses)
            {
                if (ShouldCourseBeDeletedFromDb(item))
                {
                    await courseService.DeleteCourse(item.Id);
                }
            }
        }
        private bool ShouldCourseBeAddedToDb(Course course)
        {
            if (_dbSavedCourses.Any(c => c.Link == course.Link))
            {
                return false;
            }

            return true;
        }
        private bool ShouldCourseBeDeletedFromDb(Course course)
        {
            if (_parsedCourses.Any(c => c.Link == course.Link))
            {
                return false;
            }

            return true;
        }
        private async Task UpdateParsedCourses()
        {
            _parsedCourses = new();
            foreach (var parser in _courseParsers)
            {
                _parsedCourses.AddRange(await parser.Parse());
            }
        }
        private async Task UpdateSavedDbCourses(ICourseService courseService)
        {
            _dbSavedCourses = (await courseService.GetAllCourses()).ToList();
        }

    }
}
