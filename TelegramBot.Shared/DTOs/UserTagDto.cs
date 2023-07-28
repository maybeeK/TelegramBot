namespace TelegramBot.Shared.DTOs
{
    public class UserTagDto
    {
        public int Id { get; set; }
        public long UsertId { get; set; }
        public string Tag { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"UserId: {UsertId}\n" +
                $"Tag: {Tag}";
        }
    }
}
