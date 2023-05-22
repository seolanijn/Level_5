/*
 * Authors: Seolan Jin, Junyeong Jo
 * Date: Feb. 3, 2023
 * Description: Build a Scrabble board game using a .NET 6.0 class library and a client to demonstrate the services provided by the class library. 
 *              The client will handle user inputs and present all outputs in the console.
 */

using ScrabbleLibrary;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // initialize an object of type IBag 
                IBag bag = new Bag();

                Console.WriteLine($"Testing ScrabbleLibrary [{bag.Author}].\n");
                Console.WriteLine($"Bag initialized with the following {bag.TileCount} tiles...");
                Console.WriteLine($"{bag.ToString()}\n");

                // Declare a boolean flag `isValid` and an integer `numPlayers` to store the number of players.
                bool isValid;
                int numPlayers;
                do
                {
                    // Prompt the user to enter the number of players and validate that the input is an integer between 1 and 8.
                    // If the input is invalid, display an error message and prompt again. Repeat until a valid input is received.
                    Console.Write("Enter the number of players (1-8): ");
                    isValid = ((int.TryParse(Console.ReadLine(), out numPlayers)) && (numPlayers > 0) && (numPlayers < 9));
                    if (!isValid)
                    {
                        Console.WriteLine("Error: Number of players must be in range of 1 - 8. Try again.\n");
                    }
                } while (!isValid);

                // Declare a list of IRack to store the racks for each player.
                List<IRack> userList = new List<IRack>();

                // If the number of players is valid, generate a rack for each player using the `GenerateRack` method of the `bag` object.
                if (isValid)
                {
                    for (int i = 0; i < numPlayers; i++)
                    {
                        userList.Add(bag.GenerateRack());
                    }

                    Console.WriteLine($"\nRacks for {numPlayers} player{((numPlayers > 1) ? "s were" : " was")} populated.");
                    Console.WriteLine($"Bag now contains the following {bag.TileCount} tiles...");
                    Console.WriteLine($"{bag.ToString()}\n");
                    // Declare an integer `playerNumber` to keep track of the current player.
                    int playerNumber;
                    do
                    {
                        playerNumber = 1;
                        // Start the game
                        foreach (IRack playerRack in userList)
                        {
                            Console.WriteLine("------------------------------------------------------------------------------");
                            Console.WriteLine($"                                   Player {playerNumber}                                   ");
                            Console.WriteLine("------------------------------------------------------------------------------");
                            Console.WriteLine(playerRack.ToString());
                            // Reset the validity flag for each player
                            isValid = false;
                            uint score = 0;
                            string userWord = "";
                            string letters = playerRack.ToString().Split('[')[1].Split(']')[0];
                            string userInput;
                            // Keep prompting the user until they make a valid choice
                            do
                            {
                                Console.Write("Test a word for its points value? (y/n): ");
                                userInput = Console.ReadLine()?.ToLower() ?? "";
                                switch (userInput)
                                {
                                    case "y":
                                        Console.Write($"Enter a word using the letters [{letters}]: ");
                                        userWord = Console.ReadLine() ?? "";
                                        score = playerRack.TestWord(userWord);
                                        break;
                                    case "n":
                                        isValid = true;
                                        Console.WriteLine("Your chance goes to the next player");
                                        break;
                                    default:
                                        Console.WriteLine("Error: Your answer must be either 'y' or 'n'.");
                                        break;
                                }
                                // If the user wants to test a word, check if it's a valid word and prompt the user
                                // if they want to play it
                                if (userInput == "y")
                                {
                                    Console.WriteLine($"The word [{userWord}] is worth {score} points.");
                                    if (score > 0)
                                    {
                                        Console.Write($"Do you want to play the word [{userWord}]? (y/n): ");
                                        switch (Console.ReadLine()?.ToLower())
                                        {
                                            case "y":
                                                if (userList[playerNumber - 1].PlayWord(userWord))
                                                {
                                                    isValid = true;
                                                    userList[playerNumber - 1].AddTiles();
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Fail to pass the word [{userWord}].");
                                                    isValid = false;
                                                }
                                                break;
                                            case "n":
                                                isValid = false;
                                                break;
                                            default:
                                                Console.WriteLine("Error: Your answer must be either 'y' or 'n'.");
                                                break;
                                        }
                                    }
                                }
                            } while (!isValid);

                            // Display information about the play
                            letters = playerRack.ToString().Split('[')[1].Split(']')[0];

                            Console.WriteLine("\n        -------------------------------");
                            Console.WriteLine("        {0,-22}{1}", "Word Played:", (userInput == "y" ? userWord : "-"));
                            Console.WriteLine("        {0,-22}{1}", "Total Points:", playerRack.TotalPoints);
                            Console.WriteLine("        {0,-22}{1}", "Rack now contains:", letters);
                            Console.WriteLine("        -------------------------------\n");

                            Console.WriteLine($"Bag now contains the following {bag.TileCount} tiles...");
                            Console.WriteLine($"{bag.ToString()}\n");

                            playerNumber++;
                        }

                        // If the Bag is empty, the client stops processing turns
                        if (bag.TileCount == 0)
                        {
                            Console.WriteLine("The bag is empty. Unable to process turns.");
                            break;
                        }
                        Console.Write("Would you like each player to take another turn? (y/n): ");
                    } while (Console.ReadLine() == "y");
                    // Display information about score
                    Console.WriteLine("Retiring the game.\n");
                    Console.WriteLine("The final scores are...");
                    Console.WriteLine("------------------------------------------------------------------------------");

                    playerNumber = 1;
                    foreach (IRack userRack in userList)
                    {
                        Console.WriteLine($"Player {playerNumber}: {userRack.TotalPoints} points");
                        playerNumber++;
                    }
                    Console.WriteLine("------------------------------------------------------------------------------");

                }
                // The `Dispose` method closes the Microsoft Word application if it is open.
                bag.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }
    }
}