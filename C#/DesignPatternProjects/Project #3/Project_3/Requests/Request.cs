/*
 * File name: Request.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class executes a request using Route
 */

namespace Assi3
{
    class Request : Command
    {
        // constructor
        public Request(string route, int payload) {
            Route = route;
            Payload = payload;
        }
        
        // properties 
        public string Route;
        public int Payload;

        // returns calculated interger value
        public int Process(Route route)
        {
            return route.Handle(this);
        }
    }
}
