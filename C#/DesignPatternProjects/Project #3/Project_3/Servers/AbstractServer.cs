/*
 * File name: AbstractServer.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class has a useful interface for Server class
 */

namespace Assi3
{
    interface AbstractServer // Element
    {
        bool Accept(Query query);
        string HandleCurrentRequest();
    }
}
