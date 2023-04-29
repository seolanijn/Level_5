/*
 * File name: IIterator.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class has some useful interfaces for HeavyObjectListIterator class
 */
namespace Assi1.Aggregates
{
    public interface IIterator
    {
        HeavyObject First();
        void Next();
        bool IsDone();
        HeavyObject CurrentItem();
        HeavyObject PreviousItem();
    }
}
