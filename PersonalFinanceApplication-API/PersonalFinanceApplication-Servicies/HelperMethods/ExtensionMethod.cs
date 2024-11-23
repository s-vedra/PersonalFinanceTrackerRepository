namespace PersonalFinanceApplication_Services.ExtensionMethods
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
}
