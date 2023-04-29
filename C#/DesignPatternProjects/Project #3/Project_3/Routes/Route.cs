/*
 * File name: Route.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class uses the Chain of Responsibility pattern 
 */

namespace Assi3
{
    class Route
    {
        // properties
        private Route Next;
        private string Path;

        // constructor
        public Route(string path, Route next = null) {
            this.Path = path;
            this.Next = next;
        }

        // returns a calculated interger value
        // The return value is set to 404 by default when the provided path does not match any existing routes
        public virtual int Calculate(int payload)
        {
            return 404;
        }

        // 1. If the current route path and the requested route match,
        //    it will return an integer value that is calculated in a specific manner using a virtual Calculate method.
        // 2. If the current route path and the requested route doesn't match, and the next Route exists,
        //    it will run the current method again on the next route.
        // 3. If the current route path and the requested route doesn't match, and the next Route doesn't exist,
        //    it will return 404 error.
        public int Handle(Request request)
        {
            if (request.Route.ToLower() == Path.ToLower()) // see if both pathes match
            {
                return Calculate(request.Payload);
            }
            if (Next != null)
            {
                return Next.Handle(request);
            }
            
            return 404;
        }
    }
}
