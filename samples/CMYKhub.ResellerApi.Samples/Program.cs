using System;
using System.IO;
using System.Linq;
using CMYKhub.ResellerApi.Client.Manufacturing;
using CMYKhub.ResellerApi.Client.Prepress;

namespace CMYKhub.ResellerApi.Samples
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("CMYKhub Reseller API Sample Client");

            if (!args.Any())
            {
                PrintHelp();
            }
            else
            {
                var apiUrl = "https://hublink.api.cmykhub.com";
                var resellerId = "9999";
                var apiKey = "INSERT KEY HERE";
                var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
                var manufacturingClient = new HublinkManufacturingClient(clientFactory, apiUrl, resellerId, apiKey);
                var prepressClient = new HublinkPrepressClient(clientFactory, apiUrl, resellerId, apiKey);
                if (args[0] == "order-get" && args.Count() == 2)
                {
                    GetOrder(args[1]) ;
                }
                else if (args[0] == "customer-get" && args.Count() == 2)
                {
                    var customerId = args[1];
                    var customer = manufacturingClient.GetCustomerAsync(customerId).Result;
                    Console.WriteLine($"Name: {customer.Name}");
                    Console.WriteLine($"Contact: {customer.ContactName}");
                }
                else if (args[0] == "order-upload" && args.Count() >= 3)
                {
                    var orderId = args[1];
                    var fileNames = args.Skip(2).ToArray();
                    foreach (var file in fileNames)
                        if (!File.Exists(file)) throw new Exception($"File not found: {file}");

                    var order = manufacturingClient.GetOrderAsync(orderId).Result;
                    var ppTask = prepressClient.UploadArtworkAsync(orderId, order.HubId, fileNames);
                    ppTask.Wait();
                    Console.WriteLine("Files Uploaded");
                }
                else
                {
                    Console.WriteLine("Invalid arguments");
                    PrintHelp();
                }
            }


            Console.WriteLine("Finished, press any key to exit");
            Console.ReadKey();
        }

        private static HublinkManufacturingClient GetManufacturingClient()
        {
            var apiUrl = "https://hublink.api.cmykhub.com";
            var resellerId = "9999";
            var apiKey = "INSERT KEY HERE";
            var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
            return new HublinkManufacturingClient(clientFactory, apiUrl, resellerId, apiKey);

        }

        private static HublinkPrepressClient GetPrepressClient()
        {
            var apiUrl = "https://hublink.api.cmykhub.com";
            var resellerId = "9999";
            var apiKey = "INSERT KEY HERE";
            var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
            return new HublinkPrepressClient(clientFactory, apiUrl, resellerId, apiKey);

        }

        private static void GetOrder(string number)
        {
            var manufacturingClient = GetManufacturingClient();
            var order = manufacturingClient.GetOrderAsync(number).Result;
            Console.WriteLine($"Date Ordered: {order.DateOrdered}");
            Console.WriteLine($"Description: {order.Description}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Status: {order.StatusName}");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Options");
            Console.WriteLine("  order-get [orderId] : this will return the order information for the order with the given id");
            Console.WriteLine("");
        }

    }

}
