namespace PFA_Services.RequestService
{
    public class RequestService : IRequestService
    {
        public bool ValidateRequest(string request)
        {
            if (request.Validate())
                return true;
            throw new Exception("Please fill in the mandatory fields");
        }

        public bool ValidateType(string request, IList<string> types)
        {
            if (request.ValidateType(types))
                return true;
            throw new Exception("Please provide valid information, type can only be fiat or crypto");
        }
    }

    public static class RequestValidator
    {
        public static bool Validate(this string request)
        {
            if (!string.IsNullOrEmpty(request))
                return true;
            return false;
        }

        public static bool ValidateType(this string request, IList<string> types)
        {
            if (!string.IsNullOrEmpty(request) && types.Any(type => type.ToLower().Equals(request.ToLower())))
                return true;
            return false;
        }
    }
}
