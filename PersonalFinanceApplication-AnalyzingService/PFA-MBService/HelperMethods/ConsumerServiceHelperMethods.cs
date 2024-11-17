using PFA_MBService.ServiceProperties;
using RabbitMQ.Client;

namespace PFA_MBService.HelperMethods
{
    public class ConsumerServiceHelperMethods
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
