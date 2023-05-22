/*
 * Authors: Seolan Jin, Junyeong Jo
 * Date: Feb. 3, 2023
 * Description: The Rack class implements the IRack interface and represents a player's rack in the Scrabble game. 
 *              It holds information about the score and tiles that the player has.
 */

namespace ScrabbleLibrary
{
    internal class Rack : IRack
    {
        private IBag bag;
        private List<Tile> tiles;
        public uint TotalPoints { get; private set; }

        
        public Rack(Bag bag)
        {
            this.bag = bag;
            tiles = new List<Tile>();
            TotalPoints = 0;
        }

        // Adds tiles from the IBag object to the Rack, up to 7 tiles or until the IBag object has no more tiles left.
        public uint AddTiles()
        {
            while (tiles.Count < 7 && bag.TileCount != 0)
            {
                tiles.Add(((Bag)bag).PopTile());
            }

            return (uint)tiles.Count;
        }
        // Plays the given candidate word if it can be made using the tiles in the rack.
        public bool PlayWord(string candidate)
        {
            uint score = TestWord(candidate);
            if (score == 0)
            {
                return false;
            }
            // Removes the used tiles and updates the TotalPoints property with the score of the candidate word.
            foreach (char c in candidate)
            {
                for (int i = 0; i < tiles.Count; i++)
                {
                    if (tiles[i].Alphabet == c)
                    {
                        tiles.RemoveAt(i);
                        break;
                    }
                }
            }
            TotalPoints += score;
            return true;
        }
        // Tests the given candidate word to see if it can be made using the tiles in the rack. Returns the score if the word can be made, or 0 if not.
        public uint TestWord(string candidate)
        {
            string lowerCandidate = candidate.ToLower();
            //The letters of the candidate string are a subset of the letters in the current Rack object.
            List<char> presentLetter = new List<char>();

            foreach (Tile tile in tiles)
            {
                presentLetter.Add(tile.Alphabet);
            }

            int score = 0;
            foreach (char c in lowerCandidate)
            {
                if (!presentLetter.Remove(c))
                {
                    return 0;
                }
            }

            // The candidate provided is a valid word as tested using the Application class’s CheckSpelling() method.
            
            bool isValid = ((Bag)bag).application.CheckSpelling(lowerCandidate);
            if (!isValid)
            {
                return 0;
            }
            
            // Add score
            foreach(char c in lowerCandidate)
            {
                foreach (Tile tile in tiles)
                {
                    if (c == tile.Alphabet)
                    {
                        score += tile.score;
                        break;
                    }
                }
            }
            return (uint)score;
            
        }

 
        // Returns a string representation of the rack.
        public string ToString()
        {
            string rackContent = "";
            foreach (Tile tile in tiles)
            {
                rackContent += tile.Alphabet;
            }

            return $"Your rack contains [{rackContent}].";
        }

    }
}
