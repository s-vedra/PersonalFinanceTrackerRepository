using Newtonsoft.Json;

namespace PersonalFinanceApplication_Services.ExtensionMethods
{
    public static class ExtensionMethod
    {
        public static decimal Subtract(this decimal minuend, decimal subtrahend)
        {
            return minuend - subtrahend;
        }

        public static string ConvertToJson<T>(this T dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
    }
}
