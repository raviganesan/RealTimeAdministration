using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeAdministration
{
    class Program
    {
        static void Main(string[] args)
        {

            //Set connection
            var connection = new HubConnection("http://localhost:60599/");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("RealTimeHub");
            //Start connection

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            Console.ForegroundColor = ConsoleColor.White;

            //myHub.Invoke<string>("Send", "HELLO World ").ContinueWith(task => {
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error calling send: {0}",
            //                          task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine(task.Result);
            //    }
            //});

            //myHub.On<string>("addMessage", param => {
            //    Console.WriteLine(param);
            //});



            while (true)
            {
                Console.Write("Set Admin Data:");

                string inputData = Console.ReadLine(); // Get string from user
                var isCommand = false;
                if (inputData == "exit") // Check string
                {
                    isCommand = true;
                    break;
                }

                if (inputData == "clear") // Check string
                {
                    isCommand = true;
                    Console.Clear();
                    Console.WriteLine("Administrator Connected");
                }



                if (!isCommand)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Updating Database...");
                    Thread.Sleep(500);
                    //Console.SetCursorPosition(0, Console.CursorTop-1);
                    Console.WriteLine("Database Updated!");


                    myHub.Invoke<string>("ConfigSettings", inputData).Wait();

                    Console.ForegroundColor = ConsoleColor.White;
                    isCommand = false;
                }

            }
          

            Console.Read();
            connection.Stop();
        }
    }
}
