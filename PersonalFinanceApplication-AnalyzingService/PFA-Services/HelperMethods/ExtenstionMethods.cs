namespace PFA_Services.HelperMethods
{
    public static class ExtenstionMethods
    {
        public static bool IsNull<T>(this T entity)
        {
            if (entity is not null)
                return false;
            return true;
        }
    }
}
