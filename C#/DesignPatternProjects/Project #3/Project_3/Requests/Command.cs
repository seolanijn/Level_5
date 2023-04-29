/*
 * File name: Command.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class has a useful interface for Request class
 */


namespace Assi3
{
    interface Command
    {
        int Process(Route route);
    }
}
