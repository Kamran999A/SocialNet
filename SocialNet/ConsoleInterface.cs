using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms; 
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
                Runner.Draw();
                for (int i = 1; i < options.Length; i++)
                {
                    Console.SetCursorPosition(31, 10+i);
                    Console.Write($"{i}. {options[i]}\n");
                }
            }
        }

        public static int InputChoice(in int length)
        {
            var choice = 0;
            do
            {
                Console.SetCursorPosition(31, 14);
                Console.Write(">> ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //MessageBox.Show(e.Message);
                    Helper.ConsoleHelper.ClearConsole(length - 1, 10);
                }
               
            } while (choice < 1 || choice > length);

            return choice;
        }
    }
}