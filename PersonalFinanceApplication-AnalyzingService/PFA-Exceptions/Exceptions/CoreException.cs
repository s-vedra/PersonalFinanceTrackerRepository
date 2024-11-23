namespace PFA_Exceptions.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException()
        {

        }

        public CoreException(string message) : base(message)
        {

        }

        public CoreException(Exception ex, string message) : base(message)
        {

        }
    }
}
