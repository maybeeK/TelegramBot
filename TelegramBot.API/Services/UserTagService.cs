using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.API.Data;
using TelegramBot.API.Entities;
using TelegramBot.API.Services.Interfaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Services
{
    public class UserTagService : IUserTagService
    {
        private readonly AppDbContext _context;
        public UserTagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserTag>> AddUserTags(IEnumerable<UserTagDto> userTagsDto)
        {
            var userTags = from userTag in userTagsDto
                           select new UserTag()
                           {
                               Id = userTag.Id,
                               UsertId = userTag.UsertId,
                               Tag = userTag.Tag
                           };

            await _context.UserTags.AddRangeAsync(userTags);

            var addedTags = _context.ChangeTracker.Entries<UserTag>().Where(e => e.State == EntityState.Added).Select(e => e.Entity).ToList();

            await _context.SaveChangesAsync();
            
            return addedTags;
        }

        public async Task<IEnumerable<UserTag>> DeleteTagsByUserId(long userId)
        {
            var tagsToDelete = await GetTagsByUserId(userId);

            if (tagsToDelete == null)
            {
                return default;
            }

            _context.UserTags.RemoveRange(tagsToDelete);

            await _context.SaveChangesAsync();
            
            return tagsToDelete;
        }

        public async Task<IEnumerable<UserTag>> GetAllUserTags()
        {
            return await _context.UserTags.ToListAsync();
        }

        public async Task<IEnumerable<UserTag>> GetTagsByUserId(long userId)
        {
            return await _context.UserTags.Where(e=>e.UsertId==userId).ToListAsync();
        }
    }
}
