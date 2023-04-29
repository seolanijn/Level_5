/*
 * File name: AddRoute.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class overrides a virtual method from Route class
 */

namespace Assi3
{
    class AddRoute : Route
    {
        // constructor
        public AddRoute(string path, Route next = null) : base(path, next) {}

        // returns a calculated integer
        public override int Calculate(int payload)
        {
            return payload + 8;
        }
    }
}
