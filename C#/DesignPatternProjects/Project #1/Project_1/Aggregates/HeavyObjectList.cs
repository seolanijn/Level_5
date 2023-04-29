/*
 * File name: HeavyObjectList.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class implements necessary list operators for HeavyObjectList that is private and creates iterator for the list
 */

using System.Collections.Generic;

namespace Assi1.Aggregates
{
    public class HeavyObjectList : IAggregate
    {
        // List of HeavyObject
        private List<HeavyObject> heavyObjectsList = new List<HeavyObject>();

        // creates and returns iterator
        public IIterator CreateIterator()
        {
            return new HeavyObjectListIterator(this);
        }
        // adds HeavyObject to the list
        public void Add(HeavyObject item)
        {
            heavyObjectsList.Add(item);
        }
        // returns HeavyObject by index
        public HeavyObject At(int position)
        {
            return heavyObjectsList[position];
        }
        // returns list's length
        public int Length()
        {
            return heavyObjectsList.Count;
        }
        // prints list items using iterator
        public void Print()
        {
            
            for (IIterator iterator = CreateIterator(); !iterator.IsDone(); iterator.Next())
            {
                iterator.CurrentItem().Print();
            }
        }
    }
}
