using System;
using System.Threading.Tasks;
using NServiceBus;
using Messages;

namespace SqlServer.ClientUI
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.Title = "SqlServer.ClientUI";
            var enpointConfiguration = new EndpointConfiguration("SqlServer.ClientUI");
            enpointConfiguration.EnableInstallers();
            var transport = enpointConfiguration.UseTransport<SqlServerTransport>();
            var connection = @"Data Source=localhost;Initial Catalog=SqlServerSimple1;Integrated Security=True;Max Pool Size=100;TrustServerCertificate=True";
            transport.ConnectionString(connection);
            transport.Routing().RouteToEndpoint(typeof(OrderPlace), "SqlServer.Sales");

            transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);
            SqlHelper.EnsureDatabaseExists(connection);
            var endpointInstance = await Endpoint.Start(enpointConfiguration).ConfigureAwait(false);
            await RunLoop(endpointInstance);
            await endpointInstance.Stop().ConfigureAwait(false);


        }

        static async Task RunLoop(IMessageSession endpointInstance)
        {
            Console.WriteLine("Press P to send Command or Q to exit ");
            while (true)
            {
                var input = Console.ReadKey();
                Console.WriteLine();
                switch (input.Key)
                {
                    case ConsoleKey.P:
                        await endpointInstance.Send(new OrderPlace{ OrderID = Guid.NewGuid().ToString().Substring(0, 8) });
                        break;
                    case ConsoleKey.Q:
                        return;
                }
            }


        }
    }
}
