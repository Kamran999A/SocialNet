using System;

namespace Exceptions
{
    class MailFormatException:Exception
    { 
        public override string Message { get; }

        public MailFormatException(in string message)
        {
            Message = message;
        }
        
    }

    class PasswordFormatException : Exception
    {
        public override string Message { get; }

        public PasswordFormatException(in string message)
        {
            Message = message;
        }

    }

    class DatabaseException : Exception
    {
        public override string Message { get; }

        public DatabaseException(in string message)
        {
            Message = message;
        }
    }

    class LoginException : Exception
    {
        public override string Message { get; }

        public LoginException(in string message)
        {
            Message = message;
        }
    }
    class UserException : Exception
    {
        public override string Message { get; }

        public UserException(in string message)
        {
            Message = message;
        }
    }
}
