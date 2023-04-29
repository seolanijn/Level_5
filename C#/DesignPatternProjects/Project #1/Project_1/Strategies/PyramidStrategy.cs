/*
 * File name: PyramidStrategy.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class defines EvaluateStack as PyramidStrategy
 */
using Assi1.Aggregates;

namespace Assi1.Strategies
{
    public class PyramidStrategy : StackingStrategy
    {
        // rewards stacks where it is shaped like a pyramid (boxes with larger dimensions on bottom)
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

                    float dimensionPre = preObject.Width * preObject.Length;
                    float dimensionCurr = currObject.Width * currObject.Length;

                    if (dimensionPre > dimensionCurr)
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
