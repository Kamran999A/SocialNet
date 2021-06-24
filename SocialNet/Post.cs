using System;
using System.Text;

namespace Post
{
    class Post
    {
        public int Id { get; private set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; private set; }
        public int LikeCount { get; private set; } = default;
        public int ViewCount { get; private set; } = default;

        private static int CurrentId { get; set; } = default;

        public Post()
        {
            Id = ++CurrentId;
            CreationDate = DateTime.Now;
        }

        public static Post operator ++(Post post)
        {
            post.LikeCount++;
            return post;
        }

        public void IncreaseView()
        {
            ViewCount++;
        }

        public void ShortInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"User: {Username}");
            Console.WriteLine($"Creation Date: {CreationDate:g}");
        }
        public override string ToString()
        {
            var post = new StringBuilder();

            post.Append($"User: {Username}\n")
                .Append($"Creation date: {CreationDate}\n")
                .Append($"Content: {Content}\n")
                .Append($"Likes: {LikeCount}\n")
                .Append($"View count: {ViewCount}");

            return post.ToString();
        }
    }
}
