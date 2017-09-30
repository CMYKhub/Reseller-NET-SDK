using System;
using System.IO;
using System.Linq;
using System.Net.Http;
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
                var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
                var manufacturingClient = new HublinkManufacturingClient(clientFactory, "https://hublink.api.cmykhub.com", "9999", "INSERT KEY HERE");
                var prepressClient = new HublinkPrepressClient(clientFactory, "https://hublink.api.cmykhub.com", "9999", "INSERT KEY HERE");
                if (args[0] == "order-get" && args.Count() == 2)
                {
                    var orderId = args[1];
                    var order = manufacturingClient.GetOrderAsync(orderId).Result;
                    Console.WriteLine($"Date Ordered: {order.DateOrdered}");
                    Console.WriteLine($"Description: {order.Description}");
                    Console.WriteLine($"Quantity: {order.Quantity}");
                    Console.WriteLine($"Status: {order.StatusName}");
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

        private static void PrintHelp()
        {

            Console.WriteLine("Options");
            Console.WriteLine("  order-get [orderId] : this will return the order information for the order with the given id");
            Console.WriteLine("");
        }

    }

}
