/*
 * File name: Program.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This program will work like a real server cluster, except the user decides when requests are processed
 */

using System;
using System.Collections.Generic;

namespace Assi3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Server> Servers = new List<Server>();
            Queue<Request> PendingRequests = new Queue<Request>();

            Console.WriteLine("Please enter a command.");
            string command = "";

            while(command != "quit") {
                string[] commandArgs = command.Split(":");

                Console.WriteLine();

                switch(commandArgs[0]) {
                    case "help":
                        Console.WriteLine("help\t\t\tDisplay this menu");
                        Console.WriteLine("createserver\t\tCreate a new server.");
                        Console.WriteLine("deleteserver:[id]\tDelete server #ID.");
                        Console.WriteLine("listservers\t\tList all servers.");
                        Console.WriteLine("new:[path]:[payload]\tCreate a new pending request.");
                        Console.WriteLine("dispatch\t\tSend a pending request to a server.");
                        Console.WriteLine("server:[id]\t\tHave server #ID execute its pending request and print the result.");
                        Console.WriteLine("quit\t\t\tQuit the application");
                        break;
                    case "createserver":
                        Servers.Add(new Server());  // adds a new server to the servers list
                        Console.WriteLine($"Created server {Servers.Count-1}.");
                        break;
                    case "deleteserver":
                        try
                        {
                            if (commandArgs.Length < 2)
                            {
                                Console.WriteLine("Error: ID is not specified");
                            }
                            else
                            {
                                Servers.RemoveAt(Convert.ToInt32(commandArgs[1]));  // removes a specified server from the servers list
                                Console.WriteLine($"Deleted server {Convert.ToInt32(commandArgs[1])}");
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Error: ID is out of range");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: ID is not int");
                        }
                        break;
                    case "listservers":
                        if (Servers.Count == 0)
                        {
                            Console.WriteLine("No servers available.");
                        }
                        else
                        {
                            // loop through to print all servers in the servers list
                            for (int i = 0; i < Servers.Count; i++)
                            {
                                Console.WriteLine($"{i}\t{"Server"}");
                            }
                        }
                        break;
                    case "new":
                        if (commandArgs.Length < 3)
                        {
                            Console.WriteLine("Error: Path or Payload is not specified");
                        }
                        else
                        {
                            try
                            {
                                // queue a new Request to the PendingRequests Queue
                                PendingRequests.Enqueue(new Request(commandArgs[1], Convert.ToInt32(commandArgs[2])));
                                Console.WriteLine($"Created request with data {commandArgs[2]} going to {commandArgs[1]}");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Error: Payload is not int");
                            }
                        }
                        break;
                    case "dispatch":
                        if (PendingRequests.Count == 0)
                        {
                            Console.WriteLine("No pending requests.");
                        }
                        else
                        {
                            string result = "";
                            Request r = PendingRequests.Dequeue();  // dequeue 
                            ServerQuery sq = new ServerQuery();     // create a new ServerQuery to see if the server is busy or not
                            for (int i = 0; i < Servers.Count; i++)
                            {
                                if (Servers[i].Accept(sq)) // if the current server is not busy
                                {
                                    Servers[i].SetRequest(r); 
                                    result = $"Sent request to Server {i}.";
                                    break;
                                }
                            }
                            if (result == "") // if there is no available server
                            {
                                Console.WriteLine("No available server.");
                                PendingRequests.Enqueue(r); // enqueue the request that could not be processed.
                            }
                            else
                            {
                                Console.WriteLine(result);
                            }
                        }
                        break;
                    case "server":
                        if (commandArgs.Length < 2)
                        {
                            Console.WriteLine("Error: ID is not specified");
                        }
                        else
                        {
                            try
                            {
                                // process the request inside server and prints the processed information
                                Console.WriteLine(Servers[Convert.ToInt32(commandArgs[1])].HandleCurrentRequest()); 
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Error: ID is out of range");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Error: ID is not int");
                            }
                        }
                        break;
                    default:
                        if(command != "") {
                            Console.WriteLine("Invalid command.");
                        }
                        break;
                }

                Console.WriteLine();
                command = Console.ReadLine();
            }
        }
    }
}
