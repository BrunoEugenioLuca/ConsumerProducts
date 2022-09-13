using ConsumerProducts.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System;
using System.Text;
using System.Text.Json;


namespace ConsumerProducts
{
    public class Consumer
    {
        private string exchange = "Products";
        public ProductDto? productDto = new ProductDto();
        public ReadSettings? settings;
           

        public void PullMessage()
        {
            
                try
                {
                    var factory = new ConnectionFactory()
                    {
                        Uri = new Uri("amqps://nwthmpgl:4wsjpaRHSPOk-Hy7CfE1rLT289PvT5bv@turkey.rmq.cloudamqp.com/nwthmpgl")
                    };

                    using var connection = factory.CreateConnection();
                    using var channel = connection.CreateModel();

                    channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout, durable: true, autoDelete: false);

                    var queueName = channel.QueueDeclare().QueueName;
                    var consumer = new EventingBasicConsumer(channel);


                    channel.QueueBind(queue: queueName, exchange: exchange, routingKey: "");

                    settings = new ReadSettings();
                    settings.AppSettingsJson();    

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var data = Encoding.ASCII.GetString(body);
                        productDto = JsonSerializer.Deserialize<ProductDto>(data);
                       
                        Console.WriteLine($" [x]:Sku: {productDto.Id}, " +
                                                $"Nome: {productDto.ProductName}, " +
                                                $"FD: {productDto.FullDescription}, " +
                                                $"SD: {productDto.ShortDescription}, " +
                                                $"Price: {productDto.Price}" +
                                                $"Pub: {productDto.Published}");

                        Task.Run(async () =>
                        {
                            await RequestApiAsync();

                        });
                    };

                 
                    
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    Console.ReadLine();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            
        }

        public async Task RequestApiAsync()
        {

            try
            {

                var url = "https://api.fattureincloud.it/v1/prodotti/lista";
                var client = new RestClient(url);


                var requestList = new RestRequest();
                //Console.WriteLine($"UID: {settings.Uid}  KEY: {settings.Key}");

                var bodyList = new FattureInCloudDto
                {
                    api_uid = settings.Uid,
                    api_key = settings.Key,
                    cod = Convert.ToString(productDto.Id)
                };

                requestList.AddJsonBody(bodyList);
                var responseList = await client.PostAsync<Lista_Prodotti>(requestList);
                //Console.WriteLine(responseList.Content.ToString());

                if (responseList == null || !responseList.lista_prodotti.Any())
                {
                    url = "https://api.fattureincloud.it/v1/prodotti/nuovo";
                    client = new RestClient(url);
                    var requestInsert = new RestRequest();
                    var bodyInsert = new FattureInCloudDto
                    {
                        api_uid = settings.Uid,
                        api_key = settings.Key,
                        nome = productDto.ProductName,
                        cod = Convert.ToString(productDto.Id),
                        //prezzo_netto = Convert.ToString(productDto.Price),
                        prezzo_ivato = true,
                        prezzo_lordo = Convert.ToString(productDto.Price),
                        cod_iva = 0,
                        desc = productDto.FullDescription
                    };

                    requestInsert.AddJsonBody(bodyInsert);
                    var response = await client.PostAsync(requestInsert);



                    Console.WriteLine($"{response.StatusCode} {response.Content}");
                }
                else
                {
                    var targetProduct = responseList.lista_prodotti.First();

                    url = "https://api.fattureincloud.it/v1/prodotti/modifica";
                    client = new RestClient(url);
                    var requestUpdate = new RestRequest();

                    targetProduct.api_uid = settings.Uid;
                    targetProduct.api_key = settings.Key;
                    targetProduct.nome = productDto.ProductName;
                    targetProduct.cod = Convert.ToString(productDto.Id);
                    targetProduct.desc = productDto.FullDescription;
                    targetProduct.prezzo_lordo = Convert.ToString(productDto.Price);

                    requestUpdate.AddJsonBody(targetProduct);
                    var response = await client.PostAsync(requestUpdate);

                    Console.WriteLine($"{response.StatusCode} {response.Content}");
                }

                Console.ReadLine();
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        static void Main(string[] args)
        {
            new Consumer().PullMessage();
            
        }
    }
}
