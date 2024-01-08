namespace PersonalFinanceApplication_Services.ExtensionMethods
{
    public static class ExtensionMethod
    {
        public static decimal Subtract(this decimal minuend, decimal subtrahend)
        {
            return minuend - subtrahend;
        }
    }
}
