/*
 * File name: BottonWeightStrategy.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class defines EvaluateStack as BottonWeightStrategy
 */
using Assi1.Aggregates;

namespace Assi1.Strategies
{
    public class BottonWeightStrategy : StackingStrategy
    {
        // rewards stacks where weight is concentrated near the bottom of the stack
        public float EvaluateStack(HeavyObjectList heavyObjectList)
        {
            int i = 0;
            float result = 0;
            for (IIterator iterator = heavyObjectList.CreateIterator(); !iterator.IsDone(); iterator.Next())
            {
                // divide current item's mass by index (one-based)
                result += iterator.CurrentItem().Mass / ++i;
            }

            return result;
        }
    }
}
