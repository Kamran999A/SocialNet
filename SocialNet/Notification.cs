using System;
using System.Text;

namespace Notification
{
    class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; private set; }
        public User.User FromUser { get; set; }
        public bool isRead { get; set; } = default;
        private static int CurrentId { get; set; } = default;

        public Notification()
        {
            Id = ++CurrentId;
            CreationDate = DateTime.Now;
        }

        public void ShortInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"User: {FromUser.Username}");
            Console.WriteLine($"Creation Date: {CreationDate:g}");
        }
        public override string ToString()
        {
            var post = new StringBuilder();

            post.Append($"User: {FromUser.Username}\n")
                .Append($"Creation date: {CreationDate}\n")
                .Append($"Content: {Text}\n");
            return post.ToString();
        }
    }
}
