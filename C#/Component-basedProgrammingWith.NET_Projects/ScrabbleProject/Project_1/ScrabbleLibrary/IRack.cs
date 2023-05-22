/*
 * Authors: Junyeong Jo, Seolan Jin
 * Date: Feb. 3, 2023
 * Description: The interface defines properties and methods for Rack class.
 */
namespace ScrabbleLibrary
{
    /// <summary>
    /// Represents a rack used in the game of Scrabble.
    /// </summary>
    public interface IRack
    {
        /// <summary>
        /// Gets the total points of the tiles in the rack.
        /// </summary>
        public uint TotalPoints { get; }

        /// <summary>
        /// Tests if a word is valid and returns its points.
        /// </summary>
        /// <param name="candidate">The word to test.</param>
        /// <returns>The points of the word if valid, otherwise 0.</returns>
        public uint TestWord(string candidate);

        /// <summary>
        /// Attempts to play a word.
        /// </summary>
        /// <param name="candidate">The word to play.</param>
        /// <returns>True if the word is played successfully, otherwise false.</returns>
        public bool PlayWord(string candidate);

        /// <summary>
        /// Adds tiles to the rack.
        /// </summary>
        /// <returns>The number of tiles added to the rack.</returns>
        public uint AddTiles();

        /// <summary>
        /// Returns a string representation of the rack.
        /// </summary>
        public string ToString();
    }
}

