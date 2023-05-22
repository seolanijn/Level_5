/*
 * Authors: Junyeong Jo, Seolan Jin
 * Date: Feb. 3, 2023
 * Description: The interface defines properties and methods for Bag class.
 */
namespace ScrabbleLibrary
{
    /// <summary>
    /// The `IBag` interface defines properties and methods for a scrabble tile bag.
    /// </summary>
    public interface IBag : IDisposable
    {
        /// <summary>
        /// Gets the author of the scrabble tile bag.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets the count of tiles in the scrabble tile bag.
        /// </summary>
        public uint TileCount { get; }

        /// <summary>
        /// Generates a scrabble rack from the tile bag.
        /// </summary>
        /// <returns>An instance of the `IRack` interface.</returns>
        public IRack GenerateRack();

        /// <summary>
        /// Converts the scrabble tile bag to a string representation.
        /// </summary>
        /// <returns>A string representation of the scrabble tile bag.</returns>
        public string ToString();

        /// <summary>
        /// Frees resources used by the scrabble tile bag.
        /// </summary>
        public void Dispose();
    }
}