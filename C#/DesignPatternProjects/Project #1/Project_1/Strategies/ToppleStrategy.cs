/*
 * File name: ToppleStrategy.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class defines EvaluateStack as ToppleStrategy
 */
using Assi1.Aggregates;

namespace Assi1.Strategies
{
    public class ToppleStrategy : StackingStrategy
    {
        // rewards stacks that are likely to topple (fall over)
        public float EvaluateStack(HeavyObjectList heavyObjectList)
        {
            int reward = 0;

            for (IIterator iterator = heavyObjectList.CreateIterator(); !iterator.IsDone(); iterator.Next())
            {
                // check if this iterator is not pointing to the first one
                if (iterator.PreviousItem() != null)
                {
                    HeavyObject preObject = iterator.PreviousItem();
                    HeavyObject currObject = iterator.CurrentItem();

                    if (preObject.Mass > currObject.Mass)
                    {
                        reward++; // increment the reward
                    }
                    else
                    {
                        reward--; // decrement the reward
                    }
                }
            }

            return reward;
        }
    }
}
