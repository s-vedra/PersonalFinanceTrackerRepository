namespace PFA_Services.RequestService
{
    public interface IRequestService
    {
        bool ValidateRequest(string firstField);
        bool ValidateType(string request, IList<string> types);
    }
}
