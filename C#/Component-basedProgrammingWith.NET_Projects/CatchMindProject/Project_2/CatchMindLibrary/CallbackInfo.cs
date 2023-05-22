/*
 * Class:           CallbackInfo.cs
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     handles data in order to pass data back and forth from clients to server or vice versa 
 */

using System.Collections.Generic;
using System.Windows; // WindowBase
using System.Runtime.Serialization; // WCF data contract types

namespace CatchMindLibrary
{
    [DataContract]
    public class CallbackInfo
    {
        //Users in the session
        [DataMember]
        public List<User> Users { get; private set; }

        //Data for drawing feature
        [DataMember]
        public string BrushColour { get; private set; }

        //Data for drawing feature
        [DataMember]
        public bool IsDrawing { get; private set; }

        //Data for drawing feature
        [DataMember]
        public Point LastPoint { get; private set; }

        //Data for drawing feature
        [DataMember]
        public double BrushThickness { get; private set; }

        //Data for drawing feature
        [DataMember]
        public double X1 { get; private set; }

        //Data for drawing feature
        [DataMember]
        public double Y1 { get; private set; }

        //Data for drawing feature
        [DataMember]
        public double X2 { get; private set; }

        //Data for drawing feature
        [DataMember]
        public double Y2 { get; private set; }

        //Answer for the painting
        [DataMember]
        public string Answer { get; private set; }

        //Message for current status of the game
        [DataMember]
        public string SharedMessage { get; private set; }

        //Check if the Clear button has been clicked
        [DataMember]
        public bool IsClear { get; private set; }

        //Check if the game is over
        [DataMember]
        public bool IsGameOver { get; private set; }

        //Check if the answer has been set
        [DataMember]
        public bool IsAnswerSet { get; private set; }

        //Constructor
        public CallbackInfo(List<User> users, string color, bool isDrawing, Point point, string answer, double thickness, double x1, double y1, double x2, double y2, string sharedM, bool isClear, bool isGameOver, bool isAnswerSet)
        {
            Users = users;
            BrushColour = color;
            IsDrawing = isDrawing;
            LastPoint = point;
            Answer = answer;
            BrushThickness = thickness;
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            SharedMessage = sharedM;
            IsClear = isClear;
            IsGameOver = isGameOver;
            IsAnswerSet = isAnswerSet;
        }
    }
}
