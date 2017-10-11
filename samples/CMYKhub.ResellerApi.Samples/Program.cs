using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMYKhub.ResellerApi.Client;
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
                if (args[0] == "order-get" && args.Count() == 2)
                {
                    GetOrder(args[1]);
                }
                else if (args[0] == "customer-get" && args.Count() == 2)
                {
                    GetCustomer(args[1]);
                }
                else if (args[0] == "order-upload" && args.Count() >= 3)
                {
                    UploadOrder(args[1], args.Skip(2).ToArray());
                }
                else if (args[0] == "products-list" && args.Count() >= 1)
                {
                    ListProducts(args.Length > 1 ? args[1] : null);
                }
                else if (args[0] == "papers-list" && args.Count() >= 1)
                {
                    ListPapers(args.Length > 1 ? args[1] : null);
                }
                else if (args[0] == "finishings-list" && args.Count() >= 1)
                {
                    ListFinishings(args.Length > 1 ? args[1] : null);
                }
                else if (args[0] == "price-product" && args.Count() >= 2)
                {
                    GetProductPrice(args[1]);
                }
                else if (args[0] == "price-booklet")
                {
                    GetBookletPrice();
                }
                else if (args[0] == "order-create-product" && args.Count() >= 2)
                {
                    CreateOrderFromProduct(args[1]);
                }
                else if (args[0] == "order-create-booklet")
                {
                    CreateOrderFromBooklet();
                }
                else if (args[0] == "order-create-token-product" && args.Count() >= 2)
                {
                    CreateOrderFromProductToken(args[1]);
                }
                else if (args[0] == "order-create-quote" && args.Count() >= 2)
                {
                    CreateOrderFromQuote(args[1]);
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

        private static ClientSettings GetSettings()
        {
            var apiUrl = "http://localhost:50595";//"https://hublink.api.cmykhub.com";
            var resellerId = "4926";// "9999";
            var apiKey = "xBZBrw6mDuS1bAA8G9E4quTmOHSlAPbRuyewj+0ujnY=";// "INSERT KEY HERE";
            return new ClientSettings(apiUrl, resellerId, apiKey);
        }

        private static HublinkManufacturingClient GetManufacturingClient()
        {
            var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
            return new HublinkManufacturingClient(clientFactory, GetSettings());
        }

        private static HublinkPrepressClient GetPrepressClient()
        {
            var clientFactory = new CMYKhub.ResellerApi.Client.HttpClientFactory();
            return new HublinkPrepressClient(clientFactory, GetSettings());
        }

        private static void GetOrder(string number)
        {
            var manufacturingClient = GetManufacturingClient();
            var order = manufacturingClient.GetOrderAsync(number).Result;
            Console.WriteLine($"Date Ordered: {order.DateOrdered}");
            Console.WriteLine($"Description: {order.Description}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine($"Status: {order.Status.Name}");
        }

        private static void UploadOrder(string number, params string[] fileNames)
        {
            var manufacturingClient = GetManufacturingClient();
            var prepressClient = GetPrepressClient();
            foreach (var file in fileNames)
                if (!File.Exists(file)) throw new Exception($"File not found: {file}");

            var order = manufacturingClient.GetOrderAsync(number).Result;
            //var ppTask = prepressClient.UploadArtworkAsync(number, order.HubId, fileNames);
            //ppTask.Wait();
            Console.WriteLine("Files Uploaded");
        }

        private static void GetCustomer(string number)
        {
            var manufacturingClient = GetManufacturingClient();
            var customer = manufacturingClient.GetCustomerAsync(number).Result;
            Console.WriteLine($"Name: {customer.Name}");
            Console.WriteLine($"Contact: {customer.ContactName}");
        }

        private static void ListProducts(string name = null)
        {
            var manufacturingClient = GetManufacturingClient();
            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(name))
                products = manufacturingClient.GetProductsByNameAsync(name).Result;
            else
                products = manufacturingClient.GetProductsAsync().Result;
            foreach (var product in products)
                Console.WriteLine($"{product.Id} : {product.Name}");
        }

        private static void ListFinishings(string name = null)
        {
            var manufacturingClient = GetManufacturingClient();
            IEnumerable<Finishing> finishings;
            if (!string.IsNullOrEmpty(name))
                finishings = manufacturingClient.GetFinishingsByNameAsync(name).Result;
            else
                finishings = manufacturingClient.GetFinishingsAsync().Result;
            foreach (var finishing in finishings)
                Console.WriteLine($"{finishing.Id} : {finishing.Name}");
        }

        private static void ListPapers(string name = null)
        {
            var manufacturingClient = GetManufacturingClient();
            IEnumerable<Paper> papers;
            if (!string.IsNullOrEmpty(name))
                papers = manufacturingClient.GetPapersByNameAsync(name).Result;
            else
                papers = manufacturingClient.GetPapersAsync().Result;
            foreach (var paper in papers)
                Console.WriteLine($"{paper.Id} : {paper.Description}");
        }

        private static void GetProductPrice(string productId)
        {
            var manufacturingClient = GetManufacturingClient();
            var price = manufacturingClient.CreatePriceAsync(new StandardPriceRequest { ProductId = productId, Quantity = 1000, Kinds = 1 }).Result;

            Console.WriteLine($"Ex Tax: {price.Price.ExTax}");
            Console.WriteLine($"Expires: {price.Expires.ToString("dd MMM yyyy")}");
        }

        private static void GetBookletPrice()
        {
            var manufacturingClient = GetManufacturingClient();
            var price = manufacturingClient.CreatePriceAsync(new BookletProductRequest
            {
                BindingId = "1",
                Quantity = 1000,
                Body = new BookletBodySection
                {
                    PaperId = "151",
                    Pp = 32
                },
                FinishedSize = new Size { Width = 210, Height = 297 },
                Orientation = 0,
                PrintType = 1,
            }).Result;

            if (price == null)
                Console.WriteLine("Unable to retrieve price");
            else
            {
                Console.WriteLine($"Ex Tax: {price.Price.ExTax}");
                Console.WriteLine($"Expires: {price.Expires.ToString("dd MMM yyyy")}");
            }
        }

        private static void CreateOrderFromProduct(string productId)
        {
            var manufacturingClient = GetManufacturingClient();
            var request = new CreateOrderFromProductRequest
            {
                Product = new StandardPriceRequest { ProductId = productId, Quantity = 1000, Kinds = 1 },
                Notes = "Generated from the Sample Code project",
                Reference = "SAMPLE CODE"
            };
            var orderSummary = manufacturingClient.CreateOrderAsync(request).Result;

            Console.WriteLine($"Order Number: {orderSummary.OrderId}");
        }

        private static void CreateOrderFromBooklet()
        {
            var manufacturingClient = GetManufacturingClient();
            var request = new CreateOrderFromBookletRequest
            {
                Booklet = new BookletProductRequest
                {
                    BindingId = "1",
                    Quantity = 1000,
                    Body = new BookletBodySection
                    {
                        PaperId = "151",
                        Pp = 32
                    },
                    FinishedSize = new Size { Width = 210, Height = 297 },
                    Orientation = 0,
                    PrintType = 1,
                },
                Notes = "Generated from the Sample Code project",
                Reference = "SAMPLE CODE"
            };
            var orderSummary = manufacturingClient.CreateOrderAsync(request).Result;

            Console.WriteLine($"Order Number: {orderSummary.OrderId}");
        }

        private static void CreateOrderFromProductToken(string productId)
        {
            var manufacturingClient = GetManufacturingClient();
            var price = manufacturingClient.CreatePriceAsync(new StandardPriceRequest { ProductId = productId, Quantity = 1000, Kinds = 1 }).Result;
            var request = new CreateOrderFromTokenRequest
            {
                Token = price.Token,
                Notes = "Generated from the Sample Code project",
                Reference = "SAMPLE CODE"
            };
            var orderSummary = manufacturingClient.CreateOrderAsync(request).Result;

            Console.WriteLine($"Order Number: {orderSummary.OrderId}");
        }

        private static void CreateOrderFromQuote(string quoteId)
        {
            var manufacturingClient = GetManufacturingClient();
            var request = new CreateOrderFromQuoteRequest
            {
                QuoteId = quoteId
            };
            var orderSummary = manufacturingClient.CreateOrderAsync(request).Result;

            Console.WriteLine($"Order Number: {orderSummary.OrderId}");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Options");
            Console.WriteLine("  order-get [orderId] : this will return the order information for the order with the given id");
            Console.WriteLine("  order-upload [orderId] [filePaths]... : this will uploads the given files for the order with the given id");
            Console.WriteLine("  customer-get [customerId] : this will return the customer information for the customer with the given id");
            Console.WriteLine("  papers-list [name] : this will return papers optionally filtered by name");
            Console.WriteLine("  finishings-list [name] : this will return finishings optionally filtered by name");
            Console.WriteLine("  products-list [name] : this will return products optionally filtered by name");
            Console.WriteLine("  price-product [productId] : this will return a price for the product with the given id");
            Console.WriteLine("  price-booklet : this will return a price for a sample booklet");
            Console.WriteLine("  order-create-product [productId] : this will create an order for the product with the given id");
            Console.WriteLine("  order-create-booklet : this will create an order for a sample booklet");
            Console.WriteLine("  order-create-token-product [productId] : this will get a price and create an order to honour the provided price");
            Console.WriteLine("  order-create-quote [quoteId] : this will create an order for an existing quote");

            Console.WriteLine("");
        }

    }

}
