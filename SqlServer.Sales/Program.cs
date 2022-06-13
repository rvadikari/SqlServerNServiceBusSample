using System;
using NServiceBus;
using Messages;
using System.Threading.Tasks;

namespace SqlServer.Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SqlServer.Sales";

            var endPointConfiguration = new EndpointConfiguration("SqlServer.Sales");
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
