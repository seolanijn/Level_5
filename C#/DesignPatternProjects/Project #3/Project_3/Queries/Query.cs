/*
 * File name: Query.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class has a useful interface for ServerQuery class 
 */

namespace Assi3
{
    interface Query // Visitor
    {
        bool VisitServer(Server server);
    }
}
