/*
 * Class:           User.cs
 * Author:          Soohwan Kim, Seolan Jin, Junyeong Jo
 * Date:            30/03/2023
 * Description:     Data for each user
 */


using System;
using System.Runtime.Serialization; // WCF data contract types

namespace CatchMindLibrary 
{
    [DataContract]
    public class User
    {
        //Id for user
        [DataMember]
        public int Id { get; set; }

        //Score for user
        [DataMember]
        public int Score { get; set; }

        //Checks if the user is the drawer
        [DataMember]
        public bool IsDrawer { get;  set; }

        //Constructor
        public User(int id, int score, bool isDrawer)
        {
            Id = id;
            Score = score;
            IsDrawer = isDrawer;
        }


        /// <summary>
        /// Method <c>IsWinner</c> returns bool according to the result that if user is a winner or not.
        /// </summary>
        public bool IsWinner()
        {
            if(Score == 3)
            {
                Console.WriteLine($"Player {Id + 1} has won!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
