using System;

namespace SocialNet
{
    enum MainMenuOptions
    {
        ADMIN = 1, USER, EXIT
    }

    enum UserMenuOptions
    {
        SHOWALLPOSTS = 1, SHOW, LIKE, CREATEPOST, LOGOUTUSER
    }

    enum AdminMenuOptions
    {
        SHOWALLPOSTS = 1, SHOW, SHOWALLNOTIFICATION, SHOWNOTIFICATION, REMOVEPOST, LOGOUTADMIN
    }

    enum LoginMenuOptions
    {
        LOGIN = 1, REGISTER, BACK
    }
    static class ConsoleInterface
    {
        public static string[] MainMenuOptions { get; set; }
        public static string[] UserMenuOptions { get; set; }
        public static string[] AdminMenuOptions { get; set; }
        public static string[] LoginMenuOptions { get; set; }

        static ConsoleInterface()
        {
            MainMenuOptions = new string[] 
                {"Main Menu", "Admin", "User", "Exit"};
            UserMenuOptions = new string[] 
                {"User", "Show All Posts", "Show Post", "Like", "Create Post", "Logout"};
            AdminMenuOptions = new string[] 
                {"Admin", "Show All Posts", "Show Post", "Show All Notification", "Show Notification", "Remove Post", "Logout"};

            LoginMenuOptions = new string[]
                {"Login/Register", "Login", "Register", "Back"};
        }

        public static void PrintMenu(in string[] options)
        {
            if (options != null)
            {
                Console.Clear();
                Console.Title = "Social Network: " + options[0];
                for (int i = 1; i < options.Length; i++)
                {
                    Console.Write($"{i}. {options[i]}\n");
                }
            }
        }

        public static int InputChoice(in int length)
        {
            var choice = 0;
            do
            {
                Console.Write(">> ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Helper.ConsoleHelper.ClearConsole(length - 1, 10);
                }
            } while (choice < 1 || choice > length);

            return choice;
        }
    }
}