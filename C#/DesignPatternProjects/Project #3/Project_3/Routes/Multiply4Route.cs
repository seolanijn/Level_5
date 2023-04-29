/*
 * File name: Multiply4Route.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class overrides a virtual method from Route class
 */

namespace Assi3
{
    class Multiply4Route : Route
    {
        // constructor
        public Multiply4Route(string path, Route next = null) : base(path, next) { }

        // returns a calculated integer
        public override int Calculate(int payload)
        {
            return payload * 4;
        }
    }
}
