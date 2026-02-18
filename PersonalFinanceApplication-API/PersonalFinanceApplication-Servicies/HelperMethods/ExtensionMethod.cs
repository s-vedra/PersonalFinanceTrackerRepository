namespace PersonalFinanceApplication_Services.HelperMethods
{
    public static class ExtensionMethod
    {
        public static decimal Subtract(this decimal minuend, decimal subtrahend)
        {
            return minuend - subtrahend;
        }

        public static bool IsNull<T>(this T obj)
        {
            if (obj is not null)
                return false;
            return true;
        }
    }

    public class EnvironmentValidationService : IEnvironmentValidationService
    {
        public bool IsDocker()
        {
            return Environment.GetEnvironmentVariable("DOCKER_LOCAL_CONTAINER_RUNNING") == "true";
        }
    }
}
