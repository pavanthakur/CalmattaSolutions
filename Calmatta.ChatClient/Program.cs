﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Calmatta.ChatClient
{
    internal class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        static async Task Main(string[] args)
        {
            await PostMessage("Hello");
        }

        private static async Task PostMessage(string message)
        {
            Console.WriteLine("Starting chat session...");

            var isSuccessStatusCode = true;

            while (isSuccessStatusCode)
            {
                var json = JsonConvert.SerializeObject(new { message = $"[{DateTime.Now.ToLongTimeString()}] {message}" });

                var response = await Client
                    .PostAsync("https://localhost:44305/api/chat", new StringContent(json, Encoding.UTF8, "application/json"))
                    .ConfigureAwait(false);

                isSuccessStatusCode = response.IsSuccessStatusCode;

                if (isSuccessStatusCode)
                {
                    Console.WriteLine($"Chat session in progress - {DateTime.Now.ToLongTimeString()}");
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                else
                {
                    Console.WriteLine("Chat session rejected");
                }
            }
        }
    }
}
