using System;
using Helper;
using Network;
using SocialNet;

namespace UserSide
{
    static class UserSide
    {
        public static void Start(ref Database.Database db, ref Session.LoginAccountSession session)
        {
            var userMenuLoop = true;
            while (userMenuLoop)
            {
                ConsoleInterface.PrintMenu(ConsoleInterface.UserMenuOptions);

                switch ((UserMenuOptions)ConsoleInterface.InputChoice(ConsoleInterface.UserMenuOptions.Length))
                {
                    case UserMenuOptions.SHOWALLPOSTS:
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
                    case UserMenuOptions.SHOW:
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

                            var newNotf = new Notification.Notification(){FromUser = session.User as User.User, Text= $"{(session.User as User.User).Username} viewed this post [id] ->{post.Id}"};

                            db.AddNotification(ref newNotf);

                            Mail.SendMail(db.Admins[0].Email, "New views!", newNotf.Text);

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
                    case UserMenuOptions.LIKE:
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
                            post++;

                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("Post liked!");
                            Console.ResetColor();
                            
                            var newNotf = new Notification.Notification() { FromUser = session.User as User.User, Text = $"{(session.User as User.User).Username} liked this post [id] ->{post.Id}" };

                            db.AddNotification(ref newNotf);

                            Mail.SendMail(db.Admins[0].Email, "New likes!", newNotf.Text);
                            
                           
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
                    case UserMenuOptions.CREATEPOST:
                    {
                        Console.Clear();

                        Console.Write("Content: ");

                        var content = string.Empty;

                        do
                        {
                            content = Console.ReadLine();
                        } while (String.IsNullOrWhiteSpace(content));

                        var newPost = new Post.Post() {Username = (session.User as User.User).Username, Content = content};

                        (session.User as User.User).AddPost(ref newPost);
                        break;
                    }
                    case UserMenuOptions.LOGOUTUSER:
                    {
                        session.Logout();
                        userMenuLoop = false;
                        break;
                    }
                    default:
                        break;
                }
            }
        }
    }
}
