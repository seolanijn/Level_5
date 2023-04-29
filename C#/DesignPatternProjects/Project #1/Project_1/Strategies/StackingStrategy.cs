/*
 * File name: StackingStrategy.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class has a useful interface for Concrete Strategy classes
 */
using Assi1.Aggregates;

namespace Assi1.Strategies
{
    public interface StackingStrategy
    {
        public float EvaluateStack(HeavyObjectList heavyObjectList);
    }
}
