/*
 * File Name: Program.cs
 * Author: Seolan Jin
 * Date: March 10, 2023
 * Description: This program works with social media posts.
 *              Revision> 1. Handle IndexOutOfRangeException when the user inputs "settitle:0" (no title specified)
 *                        2. This program does not handle NullReferenceException as the provided Demo program does not
 */
using System;
using System.Collections.Generic;

namespace Assi2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PostProxy> Posts = new List<PostProxy>();

            // Creates three proxy with 'new' keyword
            PostProxy p1 = new PostProxy();
            Posts.Add(p1);

            PostProxy p2 = new PostProxy();
            Posts.Add(p2);

            PostProxy p3 = new PostProxy();
            Posts.Add(p3);

            Console.WriteLine("Welcome to the Social Network!\nEnter a command to get started, or 'help' to see a list of commands:");
            string command = "";

            while(command != "quit") {
                string[] commandArgs = command.Split(":");
                int postNum = -1;
                if(commandArgs.Length > 1) {
                    try {
                        postNum = int.Parse(commandArgs[1]);
                    } catch(FormatException) {
                        Console.WriteLine("Error: Invalid post number specified!");
                    }

                    if(postNum < 0 || postNum > Posts.Count) {
                        Console.WriteLine("Error: Invalid post number specified!");
                        break;
                    } 
                }

                switch(commandArgs[0]) {
                    case "help":
                        Console.WriteLine("help\t\t\tDisplay this menu");
                        Console.WriteLine("new\t\t\tCreate a new post.");
                        Console.WriteLine("list\t\t\tList all posts.");
                        Console.WriteLine("download:[id]\t\tDownload a post.");
                        Console.WriteLine("settitle:[id]:[title]\tSet a post's title.");
                        Console.WriteLine("setbody:[id]:[body]\tSet a post's body.");
                        Console.WriteLine("view:[id]\t\tView a post.");
                        Console.WriteLine("quit\t\t\tQuit the application");
                        break;
                    case "new":
                        // Adds new proxy using Clone()
                        Posts.Add(p1.Clone());
                        Console.WriteLine("Added a new Post.");
                        break;
                    case "list":
                        for (int i = 0; i < Posts.Count; i++)
                        {
                            Console.Write($"[{i}] : ");
                            Console.WriteLine(Posts[i].GetPrintableTitle());
                        }
                        break;
                    case "download":
                        Posts[postNum].Download();
                        Console.WriteLine($"Post {postNum} has been downloaded.");
                        break;
                    case "settitle":
                        // Handle IndexOutOfRangeException when the user inputs "settitle:<postNum>" (no title specified)
                        if (commandArgs.Length >= 3)
                        {
                            Posts[postNum].SetTitle(commandArgs[2]);
                            Console.WriteLine($"Title of post ID {postNum} has been set to {commandArgs[2]}.");
                        }
                        else
                        {
                            Console.WriteLine("Error: No post number or no title specified!");
                        }
                        break;
                    case "setbody":
                        // Handle IndexOutOfRangeException when the user inputs "setbody:<postNum>" (no body specified)
                        if (commandArgs.Length >= 3)
                        {
                            Posts[postNum].SetBody(commandArgs[2]);
                            Console.WriteLine($"Body of post ID {postNum} has been set to {commandArgs[2]}.");
                        }
                        else
                        {
                            Console.WriteLine("Error: No post number or no body specified!");
                        }
                        break;
                    case "view":
                        Posts[postNum].Print();
                        break;
                    default:
                        if(command != "") {
                            Console.WriteLine("Invalid command.");
                        }
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}