/*
 * File name: ServerQuery.cs
 * Author: Seolan Jin
 * Date: Mar. 31, 2023
 * Description: This class defines whether current server is busy or not
 */

namespace Assi3
{
    class ServerQuery : Query
    {
        // return true if the server is not busy, false if not
        public bool VisitServer(Server server)
        {
            return server.isAvailable;
        }
    }
}
