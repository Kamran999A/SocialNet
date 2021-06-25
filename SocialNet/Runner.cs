using System;
using System.Collections.Generic;
using System.Text;
using Conclusion;
using Helper;
using Network;

namespace SocialNet
{
    class Runner
    {
        private static int Width;
        private static int Hieght;
        private static int LocationX;
        private static int LocationY;
        private static int count = 0;
        private static ConsoleColor BorderColor;



        public static void Draw()
        {
            string s = "╔";
            string space = "";
            string temp = "";
            for (int i = 0; i < Width; i++)
            {
                space += " ";
                s += "═";
            }

            for (int j = 0; j < LocationX; j++)
                temp += " ";

            s += "╗" + "\n";

            for (int i = 0; i < Hieght; i++)
                s += temp + "║" + space + "║" + "\n";

            s += temp + "╚";
            for (int i = 0; i < Width; i++)
                s += "═";

            s += "╝" + "\n";

            Console.ForegroundColor = BorderColor;
            Console.CursorTop = LocationY;
            Console.CursorLeft = LocationX;
            Console.Write(s);
            if (count == 0)
            {

            }
            Console.ResetColor();
        }
        private static void ConsoleRectangle(int width, int hieght, int locationX, int locationY, ConsoleColor borderColor)
        {
            Width = width;
            Hieght = hieght;
            LocationX = locationX;
            LocationY = locationY;
            BorderColor = borderColor;
        }

        public static void Runnerofit()
        {
            ConsoleRectangle(73, 22, 20, 4, ConsoleColor.DarkRed);
            Draw();
            var db = new Database.Database();
            var Conclusion = new Conclusion.LoginAccountConclusion();
            var user = new User.User
            {
                Email = "unbrealla@junbo.com",
                Name = "unbrella",
                Surname = "unbrella",
                Username = "unbrella",
                Password = "unbrella123",
                Activation = true
            };
            var admin = new Admin.Admin
            {
                Email = "admin@admin.com",
                Username = "admin",
                Password = "admin"
            };
            db.AddUser(ref user);
            db.AddAdmin(ref admin);
            var mainMenuLoop = true;
            while (mainMenuLoop)
            {
                Console.SetCursorPosition(30, 10);
                Draw();
                ConsoleInterface.PrintMenu(ConsoleInterface.MainMenuOptions);

                switch ((MainMenuOptions)ConsoleInterface.InputChoice(ConsoleInterface.MainMenuOptions.Length))
                {
                    case MainMenuOptions.ADMIN:
                        {
                            if (!Conclusion.Status)
                            {
                                Console.Clear();
                                string username, password;

                                Draw();
                                Console.SetCursorPosition(30, 10);
                                Console.Write("Username: ");

                                username = Console.ReadLine();

                                Console.SetCursorPosition(31, 11);
                                Console.Write("Password: ");

                                password = Console.ReadLine();

                                var userCredentials = new UserCredentials(ref username, ref password);
                                try
                                {
                                    Conclusion.LoginAsAdmin(db, userCredentials);
                                }
                                catch (Exception e)
                                {
                                    Console.SetCursorPosition(33, 13);
                                    Console.WriteLine(e.Message);
                                    Helper.ConsoleHelper.ClearConsole();
                                    continue;
                                }
                            }
                            AdminHelper.AdminHelper.Start(ref db, ref Conclusion);
                            break;
                        }
                    case MainMenuOptions.USER:
                        {
                            var loginMenuLoop = true;

                            while (loginMenuLoop)
                            {
                                ConsoleInterface.PrintMenu(ConsoleInterface.LoginMenuOptions);

                                switch ((LoginMenuOptions)ConsoleInterface.InputChoice(ConsoleInterface.LoginMenuOptions
                                    .Length))
                                {
                                    case LoginMenuOptions.LOGIN:
                                        {
                                            if (!Conclusion.Status)
                                            {
                                                Console.Clear();
                                                string username, password;


                                                Console.Write("Username: ");

                                                username = Console.ReadLine();

                                                Console.Write("Password: ");

                                                password = Console.ReadLine();

                                                var userCredentials = new UserCredentials(ref username, ref password);

                                                try
                                                {
                                                    Conclusion.LoginAsUser(db, userCredentials);
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                    Helper.ConsoleHelper.ClearConsole();
                                                    continue;
                                                }
                                            }

                                            UserSide.UserSide.Start(ref db, ref Conclusion);
                                            break;
                                        }
                                    case LoginMenuOptions.REGISTER:
                                        {
                                            Console.Clear();

                                            var username = String.Empty;

                                            while (true)
                                            {
                                                Draw();
                                                Console.SetCursorPosition(32, 12);
                                                Console.Write("Username: ");

                                                username = Console.ReadLine();

                                                try
                                                {
                                                    UserHelper.CheckUsernameLength(username);
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

                                            var name = String.Empty;

                                            while (true)
                                            {
                                                Draw();
                                                Console.SetCursorPosition(33, 10);
                                                Console.Write("Name: ");

                                                name = Console.ReadLine();

                                                try
                                                {
                                                    UserHelper.CheckForbiddenCharacter(name);
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

                                            var surname = String.Empty;

                                            while (true)
                                            {
                                                Draw();
                                                Console.SetCursorPosition(33, 10);
                                                Console.Write("Surname: ");

                                                surname = Console.ReadLine();

                                                try
                                                {
                                                    UserHelper.CheckForbiddenCharacter(surname);
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

                                            var age = 0;

                                            while (true)
                                            {
                                                try
                                                {
                                                    Draw();
                                                    Console.SetCursorPosition(33, 10);
                                                    Console.Write("Age: ");

                                                    age = Convert.ToInt32(Console.ReadLine());
                                                    UserHelper.CheckAge(age);
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

                                            var email = String.Empty;

                                            while (true)
                                            {
                                                Draw();
                                                Console.SetCursorPosition(33, 10);
                                                Console.Write("E-mail: ");

                                                email = Console.ReadLine();

                                                try
                                                {
                                                    MailHelper.ValidateMail(email);
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

                                            var password = String.Empty;

                                            while (true)
                                            {
                                                Draw();
                                                Console.SetCursorPosition(33, 10);
                                                Console.Write("Password: ");

                                                password = Console.ReadLine();

                                                try
                                                {
                                                    UserHelper.CheckPasswordStrong(password);
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

                                            var newUser = new User.User()
                                            {
                                                Username = username,
                                                Name = name,
                                                Surname = surname,
                                                Age = age,
                                                Email = email,
                                                Password = password
                                            };

                                            try
                                            {
                                                CreateAccountConclusion.SendConfirmationCode(newUser.Email);
                                                Console.Clear();
                                                Draw();
                                                Console.SetCursorPosition(32, 9);
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                Console.WriteLine("Confirmation code sent! Check your mail!");
                                                Console.ResetColor();

                                                var accountCreated = false;
                                                var tryCount = 3;
                                                while (tryCount-- > 0)
                                                {
                                                    
                                                    Console.SetCursorPosition(33, 10);
                                                    Console.Write("Code > ");
                                                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                                                    var code = Console.ReadLine();

                                                    Console.ResetColor();

                                                    if (CreateAccountConclusion.ConfirmAccount(ref newUser, code))
                                                    {
                                                        CreateAccountConclusion.Registration(ref db, ref newUser);
                                                        Conclusion.Status = true;
                                                        Conclusion.User = newUser;
                                                        accountCreated = true;
                                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                                        Console.WriteLine("Account activated!");
                                                        Console.ResetColor();
                                                        break;
                                                    }

                                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                                    Console.WriteLine("Code is wrong! Try again.");
                                                    Console.WriteLine($"You can try {tryCount} more!");
                                                    Helper.ConsoleHelper.ClearConsole();
                                                }

                                                if (!accountCreated)
                                                    Console.WriteLine("You can try later!");

                                            }
                                            catch (Exception e)
                                            {
                                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                                Console.WriteLine(e.Message);
                                                Helper.ConsoleHelper.ClearConsole();
                                            }


                                            break;
                                        }
                                    case LoginMenuOptions.BACK:
                                        {
                                            loginMenuLoop = false;
                                            break;
                                        }
                                }
                                //user choices
                            }
                            break;
                        }
                    case MainMenuOptions.EXIT:
                        {
                            mainMenuLoop = false;
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
