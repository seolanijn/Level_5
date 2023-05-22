/*
 * Program:         CatchMindServiceHost.exe
 * Module:          Program.cs
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     The application is for hosting the catch mind game
 */

using System;
using System.ServiceModel;  // WCF types
using CatchMindLibrary;     // GameInfo, IGameInfo types

namespace CatchMindServiceHost
{
    internal class Program
    {
        static void Main()
        {
            ServiceHost servHost = null;

            try
            {
                // Instantiate the SertviceHost (endpoint configuration is 
                // "looked-up" by the CLR in the App.config file)
                servHost = new ServiceHost(typeof(GameInfo));

                // Run the service
                servHost.Open();

                Console.WriteLine("Service started. Press any key to quit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Keep the service running until a keystroke is entered by the system administrator
                Console.ReadKey();
                servHost?.Close();
            }
        }
    }
}
