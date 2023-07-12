using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.Data;
using TelegramBot.API.Entities;
using TelegramBot.API.Services;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.Tests
{
    public class UserTagServiceTests
    {
        private readonly AppDbContext _context;
        public UserTagServiceTests()
        {
            var opts = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _context = new AppDbContext(opts);
            _context.UserTags.AddRange(GetTestUserTags());
            _context.SaveChanges();
        }

        [Fact]
        public async void GetTagByUserIdTest()
        {
            // Arrange
            long userId = 1;
            var userTags = Enumerable.Empty<UserTag>();

            // Act

            using (var context = _context)
            {
                var UTS = new UserTagService(context);
                userTags = await UTS.GetTagsByUserId(userId);
            }

            // Assert
            Assert.Equal(2, userTags.Count());
        }

        [Fact]
        public async void GetTagByNotExistingUserIdTest()
        {
            // Arrange
            long notExistingUserId = 1000;
            var userTags = Enumerable.Empty<UserTag>();

            // Act

            using (var context = _context)
            {
                var UTS = new UserTagService(context);
                userTags = await UTS.GetTagsByUserId(notExistingUserId);
            }

            // Assert
            Assert.Empty(userTags);
        }

        [Fact]
        public async void AddUserTagsTest()
        {
            // Arrange
            var tagsToAdd = new List<UserTagDto>() {
                new UserTagDto
                    {
                        UsertId = 5,
                        Tag = "New Tag"
                    },
                new UserTagDto
                    {
                        UsertId = 5,
                        Tag = "New Tag 2"
                    }
                };
            var userTags = Enumerable.Empty<UserTag>();

            // Act

            using (var context = _context)
            {
                var UTS = new UserTagService(context);
                userTags = await UTS.AddUserTags(tagsToAdd);
            }

            // Assert
            Assert.True(userTags.All(e=>e.UsertId == 5));
        }

        [Fact]
        public async void DeleteUserTagsTest()
        {
            // Arrange
            long id = 1;
            var userTags = Enumerable.Empty<UserTag>();

            // Act

            using (var context = _context)
            {
                var UTS = new UserTagService(context);
                userTags = await UTS.DeleteTagsByUserId(id);
            }

            // Assert
            Assert.True(userTags.All(e=>e.UsertId == id));
        }

        [Fact]
        public async void DeleteNotExistingUserTagsTest()
        {
            // Arrange
            long id = 100;
            var userTags = Enumerable.Empty<UserTag>();

            // Act

            using (var context = _context)
            {
                var UTS = new UserTagService(context);
                userTags = await UTS.DeleteTagsByUserId(id);
            }

            // Assert
            Assert.Empty(userTags);
        }

        private IEnumerable<UserTag> GetTestUserTags()
        {
            for (int i = 1; i <= 10; i++)
            {
                yield return new UserTag
                {
                    UsertId = i % 5,
                    Tag = $"Tag #{i}"
                };
            }
        }
    }
}
