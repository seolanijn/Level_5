/*
 * File name: IAggregate.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class has a useful interface for HeavyObjectList class
 */
namespace Assi1.Aggregates
{
    public interface IAggregate
    {
        IIterator CreateIterator();
    }
}
