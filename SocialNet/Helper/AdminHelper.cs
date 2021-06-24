using System;
using Helper;
using SocialNet;

namespace AdminHelper
{
    class AdminHelper
    {
        public static void Start(ref Database.Database db, ref Session.LoginAccountSession session)
        {
            var adminMenuLoop = true;
            while (adminMenuLoop)
            {
                ConsoleInterface.PrintMenu(ConsoleInterface.AdminMenuOptions);

                switch ((AdminMenuOptions) ConsoleInterface.InputChoice(ConsoleInterface.AdminMenuOptions.Length))
                {
                    case AdminMenuOptions.SHOWALLPOSTS:
                        {
                            try
                            {
                                Console.Clear();
                                db.ShowAllPosts(true);
                                ConsoleHelper.ClearConsole();
                            }
                            catch (Exception e)
                            {
                                var line = Console.CursorTop;
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(e.Message);
                                ConsoleHelper.ClearConsole(line - 1, 10);
                            }
                            break;
                        }
                    case AdminMenuOptions.SHOW:
                        {
                            try
                            {
                                Console.Clear();
                                db.ShowAllPosts();

                                var id = 0;
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("ID: ");
                                        id = Convert.ToInt32(Console.ReadLine());
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        var line = Console.CursorTop;
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine(e.Message);
                                        ConsoleHelper.ClearConsole(line - 1, 10);
                                    }
                                }
                                Console.Clear();
                                var post = db.GetPost(id);
                                Console.WriteLine(post);
                                post.IncreaseView();
                                ConsoleHelper.ClearConsole();
                            }
                            catch (Exception e)
                            {
                                var line = Console.CursorTop;
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(e.Message);
                                ConsoleHelper.ClearConsole(line - 1, 10);
                            }
                            break;
                        }
                    case AdminMenuOptions.SHOWALLNOTIFICATION:
                    {
                        try
                        {
                            Console.Clear();
                            db.ShowAllNotifications(true);
                            ConsoleHelper.ClearConsole();
                        }
                        catch (Exception e)
                        {
                            var line = Console.CursorTop;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(e.Message);
                            ConsoleHelper.ClearConsole(line - 1, 10);
                        }
                        break;
                    }
                    case AdminMenuOptions.SHOWNOTIFICATION:
                    {
                        try
                        {
                            Console.Clear();
                            db.ShowAllNotifications();

                            var id = 0;
                            while (true)
                            {
                                try
                                {
                                    Console.Write("ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    break;
                                }
                                catch (Exception e)
                                {
                                    var line = Console.CursorTop;
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(e.Message);
                                    ConsoleHelper.ClearConsole(line - 1, 10);
                                }
                            }
                            Console.Clear();
                            var notification = db.GetNotification(id);
                            Console.WriteLine(notification);
                            notification.isRead = true;
                            ConsoleHelper.ClearConsole();
                        }
                        catch (Exception e)
                        {
                            var line = Console.CursorTop;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(e.Message);
                            ConsoleHelper.ClearConsole(line - 1, 10);
                        }

                        break;
                    }
                    case AdminMenuOptions.REMOVEPOST:
                    {
                        try
                        {
                            Console.Clear();
                            db.ShowAllPosts();

                            var id = 0;
                            while (true)
                            {
                                try
                                {
                                    Console.Write("ID: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    break;
                                }
                                catch (Exception e)
                                {
                                    var line = Console.CursorTop;
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine(e.Message);
                                    ConsoleHelper.ClearConsole(line - 1, 10);
                                }
                            }
                            Console.Clear();
                            db.DeletePost(id);
                            ConsoleHelper.ClearConsole();
                        }
                        catch (Exception e)
                        {
                            var line = Console.CursorTop;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(e.Message);
                            ConsoleHelper.ClearConsole(line - 1, 10);
                        }
                        break;
                    }
                    case AdminMenuOptions.LOGOUTADMIN:
                    {
                        session.Logout();
                        adminMenuLoop = false;
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}
