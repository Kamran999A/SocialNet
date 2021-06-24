using System;
using System.Text;
using Exceptions;

namespace User
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Password can not be null!");
                _password = Hash.Hash.GetHashSha256(value);
            }
        }
        public Post.Post[] Posts { get; private set; }
        public bool Activation { get; set; } = default;
        private static int CurrentId { get; set; } = default;

        public User()
        {
            Id = ++CurrentId;
        }
        public void AddPost(ref Post.Post post)
        {
            var newLength = (Posts != null) ? Posts.Length + 1 : 1;
            var temp = new Post.Post[newLength];
            if (temp == null)
                throw new DatabaseException("Can not allocate new memory!");
            if (Posts != null)
            {
                Array.Copy(Posts, temp, Posts.Length);
            }
            temp[newLength - 1] = post;
            Posts = temp;
        }

        public void RemovePost(int id)
        {
            var postIndex = Array.FindIndex(Posts, post => post.Id == id);
            if (postIndex < 0)
                throw new DatabaseException($"There is no post associated this id -> {id}");
            var temp = new Post.Post[Posts.Length - 1];
            if (temp != null)
            {
                Array.Copy(Posts, temp, postIndex);
                Array.Copy(Posts, postIndex + 1, temp, postIndex, Posts.Length - postIndex - 1);
            }
            Posts = temp;
        }
    }
}
