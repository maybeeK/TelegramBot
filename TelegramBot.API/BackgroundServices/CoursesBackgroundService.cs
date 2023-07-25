using TelegramBot.API.CourseParsers.Interfaces;
using TelegramBot.API.Entities;
using TelegramBot.API.Extensions;
using TelegramBot.API.Hubs;
using TelegramBot.API.Services;
using TelegramBot.API.Services.Interfaces;

namespace TelegramBot.API.BackgroundServices
{
    public class CoursesBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<ICourseParser> _courseParsers;
        private readonly CourseHub _courseHub;
        private List<Course> _parsedCourses;
        private List<Course> _dbSavedCourses;
        public CoursesBackgroundService(IServiceProvider serviceProvider, CourseHub courseHub)
        {
            var type = typeof(ICourseParser);

            var parserTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);

            _courseParsers = parserTypes.Select(e => (ICourseParser)Activator.CreateInstance(e));
            _serviceProvider = serviceProvider;
            _courseHub = courseHub;
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

                        await ProcessNewCourses(_courseService);
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
        private async Task ProcessNewCourses(ICourseService _courseService)
        {
            var newCourses = _parsedCourses.Where(IsCourseNewToDb).ToList();
            var parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = 1 };

            await Parallel.ForEachAsync(newCourses, parallelOptions, async (course, _) =>
            {
                await AddNewCourse(_courseService, course);
            });

            await _courseHub.NotifyAboutNewCourses(newCourses.ConvertToDto());
        }

        private async Task AddNewCourse(ICourseService _courseService, Course course)
        {
            await _courseService.AddCourse(course.ConvertToDto());
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
        private bool IsCourseNewToDb(Course course)
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
