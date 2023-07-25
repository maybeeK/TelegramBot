using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelegramBot.API.Data;
using TelegramBot.API.Entities;
using TelegramBot.API.Extensions;
using TelegramBot.API.Services.Interfaces;
using TelegramBot.Shared.DTOs;

namespace TelegramBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTagsController : ControllerBase
    {
        private readonly IUserTagService _userTagService;

        public UserTagsController(IUserTagService userTagService)
        {
            _userTagService = userTagService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTagDto>>> GetAllUserTags()
        {
            try
            {
                var tags = await _userTagService.GetAllUserTags();

                var tagsDto = tags.
                    ConvertToDto();

                return Ok(tagsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{userId:long}")]
        public async Task<ActionResult<IEnumerable<UserTagDto>>> GetTagsByUserId(int userId)
        {
            try
            {
                var userTags = await _userTagService.GetTagsByUserId(userId);

                if (userTags == null)
                {
                    return NoContent();
                }

                var userTagsDto = userTags.ConvertToDto();

                return Ok(userTagsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserTag>>> AddUserTags([FromBody] IEnumerable<UserTagDto> userTags)
        {
            try
            {
                var added = await _userTagService.AddUserTags(userTags);

                if (added == null)
                {
                    return BadRequest();
                }

                var addedDto = added.ConvertToDto();

                return Ok(addedDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("{userId:long}")]
        public async Task<ActionResult<IEnumerable<UserTag>>> DeleteTagsByUserId(long userId)
        {
            try
            {
                var deletedTags = await _userTagService.DeleteTagsByUserId(userId);

                if (deletedTags == null)
                {
                    return NoContent();
                }

                var deletedTagsDto = deletedTags.ConvertToDto();

                return Ok(deletedTagsDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
