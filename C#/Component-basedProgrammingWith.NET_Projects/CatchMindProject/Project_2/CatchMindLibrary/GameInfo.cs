/*
 * Class:           GameInfo.cs
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     handles the game data
 */

using System;
using System.Windows; // WindowBase
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;  // WCF types
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace CatchMindLibrary
{
    // Implements the WCF service contract and manages the Shoe as a singleton
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class GameInfo : IGameInfo
    {
        private List<User> users = new List<User>();

        private string brushColour = "Black";

        private bool isDrawing = false;
        private Point lastPoint;
        private string answer = "";
        private string sharedMessage = "";
        private double brushThickness = 3;
        private double x1 = 0;
        private double y1 = 0;
        private double x2 = 0;
        private double y2 = 0;
        private bool isClear = false;
        private bool isGameOver = false;
        private bool isAnswerSet = false;

        //callbacks
        private readonly Dictionary<int, ICallback> callbacks = new Dictionary<int, ICallback>();

        //private int nextClientId;
        private int currentIndex;

        //Constructor
        public GameInfo()
        {
            currentIndex = 0;
            Console.WriteLine($"Creating a game lobby");
        }

        /// <summary>
        /// Property <c>IsGameOver</c> is for checking if the game is over
        /// </summary>
        public bool IsGameOver
        {
            get { return isGameOver; }
            set
            {
                if (value != isGameOver)
                {
                    isGameOver = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>IsClear</c> is for checking if the clear button has been clicked
        /// </summary>
        public bool IsClear
        {
            get { return isClear; }
            set
            {
                if (value != isClear)
                {
                    isClear = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>Users</c> is for users list in the session
        /// </summary>
        public List<User> Users
        {
            get { return users; }
            set
            {
                if (value != users)
                {
                    users = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Method <c>AddUser</c> adds user in the users list 
        /// </summary>
        public void AddUser(int id, bool isDrawer)
        {
            users.Add(new User(id, 0, isDrawer));
            UpdateAllClients();
        }

        /// <summary>
        /// Method <c>AddScore</c> adds a score whoever gets the painting right
        /// </summary>
        public void AddScore(int id)
        {
            users[id].Score++;
            UpdateAllClients();
        }


        /// <summary>
        /// Property <c>BrushColour</c> is for tracking lines for drawing
        /// </summary>
        public string BrushColour
        {
            get { return brushColour; }
            set
            {
                if (value != brushColour)
                {
                    brushColour = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>BrushThickness</c> is for tracking lines for drawing
        /// </summary>
        public double BrushThickness
        {
            get { return brushThickness; }

            set
            {
                // Changes only if brushThickness is assigned a different value 
                if (value != brushThickness)
                {
                    brushThickness = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>IsAnswerSet</c> is for the answer has been set
        /// </summary>
        public bool IsAnswerSet
        {
            get { return isAnswerSet; }
            set
            {
                if (value != isAnswerSet)
                {
                    isAnswerSet = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>IsDrawing</c> is for checking if a painting is being drawn
        /// </summary>
        public bool IsDrawing
        {
            get { return isDrawing; }

            set
            {
                // Changes only if isDrawing is assigned a different value 
                if (value != isDrawing)
                {
                    isDrawing = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>X1</c> is for tracking lines for drawing
        /// </summary>
        public double X1
        {
            get { return x1; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != x1)
                {
                    x1 = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>Y1</c> is for tracking lines for drawing
        /// </summary>
        public double Y1
        {
            get { return y1; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != y1)
                {
                    y1 = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>X2</c> is for tracking lines for drawing
        /// </summary>
        public double X2
        {
            get { return x2; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != x2)
                {
                    x2 = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>Y2</c> is for tracking lines for drawing
        /// </summary>
        public double Y2
        {
            get { return y2; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != y2)
                {
                    y2 = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>LastPoint</c> is for tracking lines for drawing
        /// </summary>
        public Point LastPoint
        {
            get { return lastPoint; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != lastPoint)
                {
                    lastPoint = value;
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>LastPoint</c> is for the answer of the painting
        /// </summary>
        public string Answer
        {
            get { return answer; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != answer)
                {
                    answer = value;
                    Console.WriteLine($"The answer for this round is {answer}.");
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Property <c>LastPoint</c> is for the message to inform the current status of the game
        /// </summary>
        public string SharedMessage
        {
            get { return sharedMessage; }

            set
            {
                // Changes only if lastPoint is assigned a different value 
                if (value != sharedMessage)
                {
                    sharedMessage = value;
                    Console.WriteLine(sharedMessage);
                    UpdateAllClients();
                }
            }
        }

        /// <summary>
        /// Method <c>RegisterForCallbacks</c> registers the client who calls this method into callbacks
        /// </summary>
        public int RegisterForCallbacks()
        {
            if (currentIndex > 3)
            {
                return 4;
            }
            else
            {
                // Identify which client is calling this method
                ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();

                // Add client's proxy object to the collection
                if (callbacks.ContainsValue(cb))
                {
                    int i = callbacks.Values.ToList().IndexOf(cb);
                    return callbacks.Keys.ElementAt(i);
                }

                callbacks.Add(currentIndex, cb);
                return currentIndex++;
            }
        }

        /// <summary>
        /// Method <c>UnregisterFromCallbacks</c> unregisters the client who calls this method into callbacks
        /// </summary>
        public void UnregisterFromCallbacks()
        {
            // Identify which client is calling this method
            ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();
            try
            {
                if (callbacks.ContainsValue(cb))
                {
                    // Identify which client is currently calling this method
                    int i = callbacks.Values.ToList().IndexOf(cb);
                    // - Get the unique id of the client as stored in the collection
                    int id = callbacks.ElementAt(i).Key;

                    // Remove this client from receiving callbacks from the service
                    callbacks.Remove(id);

                    Dictionary<int, ICallback> tempCallbacks = new Dictionary<int, ICallback>();

                    for (int k = 0; k < callbacks.Count; k++)
                    {
                        // i = 1
                        // 0, 1, 2

                        if (i <= k)
                            tempCallbacks.Add(callbacks.ElementAt(k).Key - 1, callbacks.ElementAt(k).Value);
                        else
                            tempCallbacks.Add(callbacks.ElementAt(k).Key, callbacks.ElementAt(k).Value);
                    }

                    callbacks.Clear();

                    foreach (KeyValuePair<int, ICallback> pair in tempCallbacks)
                    {
                        callbacks.Add(pair.Key, pair.Value);
                    }
                    currentIndex = callbacks.Count;

                    users.RemoveAt(i);

                    for (int k = 0; k < users.Count; k++)
                    {
                        users[k].Score = 0;
                        users[k].IsDrawer = false;
                    }

                    users[0].IsDrawer = true;
                    UpdateAllClients();
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }         
        }

        /// <summary>
        /// Method <c>UnregisterFromCallbacks</c> Invokes the callback method ICallback.UpdateClient() on all client proxies
        /// </summary>
        private void UpdateAllClients()
        {
            CallbackInfo info = new CallbackInfo(users, brushColour, isDrawing, lastPoint, answer, brushThickness, x1, y1, x2, y2, sharedMessage, isClear, isGameOver, isAnswerSet);

            foreach (KeyValuePair<int, ICallback> cb in callbacks)
            {
                cb.Value.UpdateClient(info, cb.Key);
            }
        }
    }
}
