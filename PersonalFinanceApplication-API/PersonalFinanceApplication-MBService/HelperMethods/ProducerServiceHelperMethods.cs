using PersonalFinanceApplication_MBService.ServiceProperties;
using RabbitMQ.Client;

namespace PersonalFinanceApplication_MBService.HelperMethods
{
    public static class ProducerServiceHelperMethods
    {
        public static ConnectionFactory GetConnectionFactory(RabbitMQSettings settings)
        {
            return new ConnectionFactory()
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password
            };
        }
    }
}
