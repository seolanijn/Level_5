/*
 * File name: Server.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class contains several helpful properties and methods that simulate a server-like functionality.
 */

namespace Assi3
{
    class Server : AbstractServer
    {
        // properties
        string path = "";
        int input, result;

        Route FirstRoute;
        Request CurrRequest;

        public bool isAvailable = true;

        // constructor
        public Server()
        {
            // CoR pattern
            AddRoute ar = new AddRoute("/add");
            Multiply4Route m4r = new Multiply4Route("/mul/4", ar);
            MultiplyRoute mr = new MultiplyRoute("/mul", m4r);

            FirstRoute = mr;
        }

        // returns true if this server is not busy, otherwise, false
        public bool Accept(Query query)
        {
            return query.VisitServer(this); // Visitor pattern
        }

        // sets the values to the corresponding properties.
        public void SetRequest(Request request)
        {
            this.CurrRequest = request;
            this.path = request.Route;
            this.input = request.Payload;

            this.isAvailable = false;
        }

        // processes the current request and returns the processed information
        public string HandleCurrentRequest()
        {
            
            if (CurrRequest == null)    // if there is no current request
                return "No work to do.";
            else
            {
                this.result = CurrRequest.Process(FirstRoute);
                CurrRequest = null;         // sets to null as the requested task has been completed
                this.isAvailable = true;    // sets to true as the current request is empty
                return $"Path:    {this.path}\nInput:   {this.input}\nResult:  {this.result}\n";
            }
        }
        
    }
}
