using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using Exceptions;

namespace Helper
{
    static class MailHelper
    {
        public static void ValidateMail(in string mail)
        {
            try
            {
                CheckForbiddenCharacter(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var mailComponents = mail.Split('@');

            if (mailComponents.Length != 2)
                throw new MailFormatException("Mail format is wrong. Try Again!");
        }
        private static void CheckForbiddenCharacter(in string data)
        {
            var pattern = "!#$%&*()_+-=";
            foreach (var @char in pattern)
            {
                if (data.Contains(@char.ToString()))
                    throw new MailFormatException(
                        $"Mail address can not be contains any of this characters -> {pattern}");
            }
        }
        public static string GenerateCode()
        {
            var random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
    static class UserHelper
    {
        public static void CheckForbiddenCharacter(in string data)
        {
            var pattern = "1234567890-=!#$%&*()_+";
            foreach (var @char in pattern)
            {
                if (data.Contains(@char.ToString()))
                    throw new UserException($"Name can not be contains any of this characters -> {pattern}");
            }
        }
        public static void CheckAge(in int age)
        {
            if (age < 18)
                throw new UserException("Age must be minimum 18 years old!");
        }

        public static void CheckUsernameLength(in string username)
        {
            if (username.Length < 5)
                throw new UserException("Username length must be minimum 5 characters");
        }
        public static void CheckPasswordStrong(in string password)
        {
            if (password.Length < 6)
                throw new PasswordFormatException("Password length must be minimum 6 characters");
            string speccar = "!@#$%^&*()-+=_";
            bool notContain = true;
            foreach (var @char in speccar)
            {
                if (password.Contains(@char.ToString()))
                {
                    notContain = false;
                    break;
                }
            }
            if (notContain)
                throw new PasswordFormatException("Password must be contain minimum one special character");
        }
    }

    static class ConsoleHelper
    {
        public static void ClearConsole()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press enter to continue");
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void ClearConsole(int lineCount, int clearableLine)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press enter to continue");
            Console.ResetColor();
            Console.ReadLine();
            Console.SetCursorPosition(0, lineCount + clearableLine);
            for (int i = 0; i < clearableLine; i++)
            {
                ClearLastLine();
            }
        }

        private static void ClearLastLine()
        {
            if (Console.CursorTop > 1)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
        }
    }
}
