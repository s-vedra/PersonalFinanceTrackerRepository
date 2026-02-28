using System.Text.Json;

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

        public static string ToJson<T>(this T entity)
        {
            return JsonSerializer.Serialize(entity, new JsonSerializerOptions
            {
                WriteIndented = false, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
