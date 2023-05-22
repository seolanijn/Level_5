/*
 * Authors: Seolan Jin, Junyeong Jo
 * Date: Feb. 3, 2023
 * Description: a class that represents the tiles used in the Scrabble game.
 */
namespace ScrabbleLibrary
{
    internal class Tile
    {
        // alphabet of the tile
        public readonly char Alphabet;
        // score of the tile
        public readonly int score;
        // constructor to create the tile and calculate its score
        public Tile(char alphabet)
        {
            this.Alphabet = alphabet;

            // calls the GetScore method to calculate the score of the tile
            this.score = GetScore(this.Alphabet);

            // if the score is negative, it means the alphabet passed is not valid
            if (this.score < 0)
            {
                throw new ArgumentException("Passed char is not an alphabet.");
            }
        }

        // method to get the score of a tile based on the alphabet
        public int GetScore(char alpha)
        {
            // dictionary to map the alphabet to its score
            Dictionary<string, int> pointOfTilePairs = new Dictionary<string, int>();
            pointOfTilePairs.Add("aeilnorstu", 1);
            pointOfTilePairs.Add("dg", 2);
            pointOfTilePairs.Add("bcmp", 3);
            pointOfTilePairs.Add("fhvwy", 4);
            pointOfTilePairs.Add("k", 5);
            pointOfTilePairs.Add("jx", 8);
            pointOfTilePairs.Add("qz", 10);

            // loop through the dictionary
            // return the value (score) of the key-value pair
            foreach (KeyValuePair<string, int> pointOfTilePair in pointOfTilePairs)
            {
                foreach (char c in pointOfTilePair.Key)
                {
                    if (c == Alphabet)
                    {
                        return pointOfTilePair.Value;
                    }
                }
            }

            // if the alphabet is not found in the dictionary, return -1
            return -1;
        }
    }
}