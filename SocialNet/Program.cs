using System;
using Session;
using Helper;
using Network;

namespace SocialNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Database.Database();
            var session = new Session.LoginAccountSession();
            var user = new User.User
            {
                Email = "unbrealla@junbo.com", Name = "unbrella", Surname = "unbrella", Username = "unbrella", Password = "unbrella123",
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
                ConsoleInterface.PrintMenu(ConsoleInterface.MainMenuOptions);

                switch ((MainMenuOptions)ConsoleInterface.InputChoice(ConsoleInterface.MainMenuOptions.Length))
                {
                    case MainMenuOptions.ADMIN:
                    {
                        if (!session.Status)
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
                                session.LoginAsAdmin(db, userCredentials);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Helper.ConsoleHelper.ClearConsole();
                                continue;
                            }
                        }
                        AdminHelper.AdminHelper.Start(ref db, ref session);
                        break;
                    }
                    case MainMenuOptions.USER:
                    {
                        var loginMenuLoop = true;

                        while (loginMenuLoop)
                        {
                            ConsoleInterface.PrintMenu(ConsoleInterface.LoginMenuOptions);

                            switch ((LoginMenuOptions) ConsoleInterface.InputChoice(ConsoleInterface.LoginMenuOptions
                                .Length))
                            {
                                case LoginMenuOptions.LOGIN:
                                {
                                    if (!session.Status)
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
                                            session.LoginAsUser(db, userCredentials);
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Helper.ConsoleHelper.ClearConsole();
                                            continue;
                                        }
                                    }
                                    
                                    UserSide.UserSide.Start(ref db, ref session);
                                    break;
                                }
                                case LoginMenuOptions.REGISTER:
                                {
                                    Console.Clear();

                                    var username = String.Empty;

                                    while (true)
                                    {
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
                                        Username = username, Name = name, Surname = surname, Age = age, Email = email,
                                        Password = password
                                    };

                                    try
                                    {
                                        CreateAccountSession.SendConfirmationCode(newUser.Email);
                                        Console.Clear();
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Confirmation code sent! Check your mail!");
                                        Console.ResetColor();

                                        var accountCreated = false;
                                        var tryCount = 3;
                                        while (tryCount-- > 0)
                                        {
                                            Console.Write("Code > ");
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                                            var code = Console.ReadLine();

                                            Console.ResetColor();

                                            if (CreateAccountSession.ConfirmAccount(ref newUser, code))
                                            {
                                                CreateAccountSession.Registration(ref db, ref newUser);
                                                session.Status = true;
                                                session.User = newUser;
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

                                        if(!accountCreated)
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
