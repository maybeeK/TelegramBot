using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Shared.DTOs
{
    public class UserTagDto
    {
        public int Id { get; set; }
        public long UsertId { get; set; }
        public string Tag { get; set; }
    }
}
