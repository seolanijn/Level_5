/*
 * Authors: Seolan Jin, Junyeong Jo
 * Date: Feb. 3, 2023
 * Description: This class defines properties for the author and tile count, as well as methods for 
 *              generating a IRack and converting the object to a string.
 */
using Microsoft.Office.Interop.Word;

namespace ScrabbleLibrary
{
    public class Bag : IBag
    {
        internal List<Tile> tiles;    // Internal list of Tile objects
        internal Application application; // Internal reference to a Microsoft Word Application object

        public string Author => "S. Jin and J. Jo - January 28, 2023";    // Property for the author string

        public uint TileCount => (uint)tiles.Count; // Property for the count of tiles in the bag

        public Bag()  // Constructor to initialize the class properties
        {
            tiles = new List<Tile>();   // Initializing the tile list
            Dictionary<string, int> countOfTilesPair = new Dictionary<string, int>();   // Initializing the dictionary for tiles count
            // Adding tile letter and its count to the dictionary
            countOfTilesPair.Add("jkqxz", 1);
            countOfTilesPair.Add("bcfhmpvwy", 2);
            countOfTilesPair.Add("g", 3);
            countOfTilesPair.Add("dlsu", 4);
            countOfTilesPair.Add("nrt", 6);
            countOfTilesPair.Add("o", 8);
            countOfTilesPair.Add("ai", 9);
            countOfTilesPair.Add("e", 12);

            // Iterating through the dictionary and creating tiles
            foreach (KeyValuePair<string, int> pair in countOfTilesPair)
            {
                foreach (char c in pair.Key)
                {
                    for (int i = 0; i < pair.Value; i++)
                    {
                        tiles.Add(new Tile(c));   // Adding tile to the list
                    }
                }
            }

            tiles = ShuffleTiles(tiles); // Shuffling the tiles
            application = new Application();  // Initializing the Word Application object
        }
        private List<Tile> ShuffleTiles(List<Tile> tiles)  // Private method to shuffle tiles
        {
            Random random = new Random();  // Initializing a random object
            // Returning shuffled tiles
            return tiles.OrderBy(tile => random.Next(tiles.Count)).ToList();
        }

        internal Tile PopTile()   // Internal method to remove a tile from the bag
        {
            Tile removedTile = tiles[0];    // Getting the first tile from the list to save before removing
            tiles.RemoveAt(0);  // Removing the first tile
            return removedTile;  // Returning the removed tile
        }

        IRack IBag.GenerateRack()  // GenerateRack method defined in the IBag interface
        {
            if (tiles.Count < 7)  // Check if there are at least 7 tiles in the bag
            {
                throw new IndexOutOfRangeException("There are fewer than seven tiles remaining in the Bag.");
            }
            Rack rack = new Rack(this);    // Initialize the Rack object
            rack.AddTiles();


            return rack;

        }

        // The method converts the `Bag` object to a string and returns the result.
        string IBag.ToString()
        {
            string result = "";

            // The maximum number of columns in the output.
            const int max_column = 13;
            int currentColumn = 0;

            // Iterate through all letters in the alphabet (a-z).
            for (char c = 'a'; c <= 'z'; c++)
            {
                int count = 0;

                // Count the number of tiles with the current letter.
                foreach (Tile tile in tiles)
                {
                    if (c == tile.Alphabet)
                    {
                        count++;
                    }
                }

                // If there are no tiles with the current letter, move on to the next letter.
                if (count == 0)
                {
                    continue;
                }
                else
                {
                    // If the current column number has reached the maximum, start a new line.
                    if (currentColumn == max_column)
                    {
                        currentColumn = 0;
                        result += '\n';
                    }

                    // Increment the current column number and add the letter and count to the result string.
                    currentColumn++;
                    result += $"{c}({count})  ";
                }
            }

            // Return the result string.
            return result;
        }

        // The `Dispose` method closes the Microsoft Word application if it is open.
        public void Dispose()
        {
            // Check if the `application` object is null. If it is not, quit the application.
            if (application != null)
                application.Quit();
        }

    }
}