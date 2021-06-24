using System;
using Exceptions;
using Network;

namespace Session
{
    struct UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentials(ref string username, ref string password)
        {
            Username = username;
            Password = Hash.Hash.GetHashSha256(password);
        }
    }
    static class CreateAccountSession
    {
        private static string ConfirmationCode { get; set; }
        public static void Registration(ref Database.Database db, ref User.User user)
        {
            try
            {
                db.AddUser(ref user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SendConfirmationCode(in string mail)
        {
            
            ConfirmationCode = Helper.MailHelper.GenerateCode();
            Mail.SendMail(mail, "Final Configuration.", $"Dear user Your confirmation code is {ConfirmationCode}");
        }
        public static bool ConfirmAccount(ref User.User user, in string code)
        {
            if (code == ConfirmationCode)
            {
                user.Activation = true;
                return true;
            }
            return false;
        }
    }
    class LoginAccountSession
    {
        public bool Status { get; set; }
        public object User{ get; set; }
        public void LoginAsUser(in Database.Database db, UserCredentials credentials)
        {
            User.User user = null;

            if(db.Users != null)
                user = Array.Find(db.Users, user1 => user1.Username == credentials.Username);

            if (user == null)
                throw new LoginException($"There is no account associated this username -> {credentials.Username}");

            if (user.Password != credentials.Password)
                throw new LoginException($"Password is wrong!");

            User = user;
            Status = true;
        }
        public void LoginAsAdmin(in Database.Database db, UserCredentials credentials)
        {
            Admin.Admin admin = null;

            if (db.Users != null)
                admin = Array.Find(db.Admins, admin1 => admin1.Username == credentials.Username);
            if (admin == null)
                throw new LoginException($"There is no account associated this username -> {credentials.Username}");
            if (admin.Password != credentials.Password)
                throw new LoginException($"Password is wrong!");
            User = admin;
            Status = true;
        }
        public void Logout()
        {
            Status = false;
            User = null;
        }
    }
}
