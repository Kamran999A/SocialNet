using System;

namespace Admin
{
    class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
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
        private static int CurrentId { get; set; } = default;

        public Admin()
        {
            Id = ++CurrentId;
        }
    }
}
