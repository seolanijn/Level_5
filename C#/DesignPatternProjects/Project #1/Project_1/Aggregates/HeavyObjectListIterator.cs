/*
 * File name: HeavyObjectListIterator.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class implements operations for a list
 */
namespace Assi1.Aggregates
{
    public class HeavyObjectListIterator : IIterator
    {
        protected HeavyObjectList list;
        protected int index;

        // constructor
        public HeavyObjectListIterator(HeavyObjectList list)
        {
            this.list = list;
            index = 0;
        }

        // returns current HeavyObject item
        public HeavyObject CurrentItem()
        {
            return list.At(index);
        }
        // returns the first HeavyObject item
        public HeavyObject First()
        {
            return list.At(0);
        }
        // returns true if the iterator is the last one, false if not
        public bool IsDone()
        {
            return index >= list.Length();
        }
        // increments index
        public void Next()
        {
            ++index;
        }
        // returns the previous HeavyObject
        public HeavyObject PreviousItem()
        {
            if (index == 0)
            {
                return null;
            }
            return list.At(index - 1);
        }
    }
}
