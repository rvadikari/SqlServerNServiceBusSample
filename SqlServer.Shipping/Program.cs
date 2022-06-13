using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;

namespace SqlServer.Shipping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SqlServer.Shipping";

            var endPointConfiguration = new EndpointConfiguration("SqlServer.Shipping");
            endPointConfiguration.EnableInstallers();
            var transport = endPointConfiguration.UseTransport<SqlServerTransport>();
            var connection = @"Data Source=localhost;Initial Catalog=SqlServerSimple1;Integrated Security=True;Max Pool Size=100;TrustServerCertificate=True";
            transport.ConnectionString(connection);
            transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            SqlHelper.EnsureDatabaseExists(connection);

            var endpoint = await Endpoint.Start(endPointConfiguration).ConfigureAwait(false);
            Console.WriteLine("Press any key to exit");
            Console.WriteLine("Waiting for the message from sender");
            Console.ReadKey();
            await endpoint.Stop().ConfigureAwait(false);
        }

    }
}
