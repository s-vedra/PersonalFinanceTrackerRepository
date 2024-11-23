namespace PFA_Services.HelperMethods
{
    public static class ExtenstionMethods
    {
        public static bool IsNull<T>(this T obj)
        {
            if (obj is not null)
                return false;
            return true;
        }
    }
}
