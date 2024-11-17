namespace PFA_MBService.ServiceProperties
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string AnalyzingQueue { get; set; }
        public string DefaultAnalyzingExchange { get; set; }
    }
}
