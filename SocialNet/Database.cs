using System;
using Exceptions;

namespace Database
{
    class Database
    {
        public Admin.Admin[] Admins { get; set; }
        public User.User[] Users { get; set; }
        public Notification.Notification[] Notifications { get; set; }
        public void AddUser(ref User.User user)
        {
            var newLength = (Users != null) ? Users.Length + 1 : 1;
            var temp = new User.User[newLength];

            if (temp == null)
                throw new DatabaseException("Can not allocate new memory!");

            if (Users != null)
            {
                Array.Copy(Users, temp, Users.Length);
            }

            temp[newLength - 1] = user;

            Users = temp;
        }

        public void RemoveUser(int id)
        {
            var userIndex = Array.FindIndex(Users, user => user.Id == id);

            if (userIndex < 0)
                throw new DatabaseException($"There is no user associated this id -> {id}");

            var temp = new User.User[Users.Length - 1];

            if (temp != null)
            {
                Array.Copy(Users, temp, userIndex);
                Array.Copy(Users, userIndex + 1, temp, userIndex, Users.Length - userIndex - 1);
            }

            Users = temp;
        }

        public void AddAdmin(ref Admin.Admin admin)
        {
            var newLength = (Admins != null) ? Admins.Length + 1 : 1;
            var temp = new Admin.Admin[newLength];

            if (temp == null)
                throw new DatabaseException("Can not allocate new memory!");

            if (Admins != null)
            {
                Array.Copy(Admins, temp, Admins.Length);
            }

            temp[newLength - 1] = admin;

            Admins = temp;
        }

        public void RemoveAdmin(int id)
        {
            var adminIndex = Array.FindIndex(Admins, admin => admin.Id == id);

            if (adminIndex < 0)
                throw new DatabaseException($"There is no admin associated this id -> {id}");

            var temp = new Admin.Admin[Admins.Length - 1];

            if (temp != null)
            {
                Array.Copy(Admins, temp, adminIndex);
                Array.Copy(Admins, adminIndex + 1, temp, adminIndex, Admins.Length - adminIndex - 1);
            }

            Admins = temp;
        }

        public void AddNotification(ref Notification.Notification notification)
        {
            var newLength = (Notifications != null) ? Notifications.Length + 1 : 1;
            var temp = new Notification.Notification[newLength];

            if (temp == null)
                throw new DatabaseException("Can not allocate new memory!");

            if (Notifications != null)
            {
                Array.Copy(Notifications, temp, Notifications.Length);
            }

            temp[newLength - 1] = notification;

            Notifications = temp;
        }

        public void RemoveNotification(int id)
        {
            var notificationIndex = Array.FindIndex(Notifications, notification => notification.Id == id);

            if (notificationIndex < 0)
                throw new DatabaseException($"There is no admin associated this id -> {id}");

            var temp = new Notification.Notification[Notifications.Length - 1];

            if (temp != null)
            {
                Array.Copy(Notifications, temp, notificationIndex);
                Array.Copy(Notifications, notificationIndex + 1, temp, notificationIndex, Notifications.Length - notificationIndex - 1);
            }

            Notifications = temp;
        }

        public void ShowAllPosts(bool detail = false)
        {
            bool isExist = false;

            if (detail)
            {
                foreach (var user in Users)
                {
                    if (user.Posts != null)
                    {
                        foreach (var userPost in user.Posts)
                        {
                            isExist = true;
                            Console.WriteLine(userPost);
                            Console.WriteLine();
                        }
                    }
                }
            }
            else
            {
                foreach (var user in Users)
                {
                    if (user.Posts != null)
                    {
                        foreach (var userPost in user.Posts)
                        {
                            isExist = true;
                            userPost.ShortInfo();
                            Console.WriteLine();
                        }
                    }
                }
            }

            if (!isExist)
                throw new DatabaseException("There is no post!");
        }

        public Post.Post GetPost(int id)
        {
            foreach (var user in Users)
            {
                if (user.Posts != null)
                {
                    foreach (var userPost in user.Posts)
                    {
                        if (userPost.Id == id)
                            return userPost;
                    }
                }
            }

            throw new DatabaseException($"There is no post associated this id -> {id}");
        }

        public void ShowAllNotifications(bool detail = false)
        {
            if (Notifications == null)
            {
                throw new DatabaseException("There is no notifications!");
            }

            if (detail)
            {
                foreach (var notification in Notifications)
                {
                    Console.WriteLine(notification);
                    Console.WriteLine();
                }
            }
            else
            {
                foreach (var notification in Notifications)
                {
                    notification.ShortInfo();
                    Console.WriteLine();
                }
            }
        }

        public Notification.Notification GetNotification(int id)
        {
            if (Notifications != null)
            {
                foreach (var notification in Notifications)
                {
                    if (notification.Id == id)
                        return notification;
                }
            }

            throw new DatabaseException($"THere is no notification associated this id -> {id}");
        }

        public void DeletePost(int id)
        {
            if (Users != null)
            {
                var post = GetPost(id);
                foreach (var user in Users)
                {
                    if (user.Username == post.Username)
                    {
                        user.RemovePost(post.Id);
                        return;
                    }
                }
            }

            throw new DatabaseException($"There is no post associated this id -> {id}");
        }
    }
}
