/*
 * File name: FlyweightFactory.cs
 * Author: Seolan Jin
 * Date: Feb. 24, 2023
 * Description: This class has a method that returns flyweights
 */
using Assi1.Strategies;
using System;
using System.Collections.Generic;

namespace Assi1.Flyweights
{
    public class FlyweightFactory
    {
        protected Dictionary<string, StackingStrategy> strategies;

        // constructor
        public FlyweightFactory()
        {
            strategies = new Dictionary<string, StackingStrategy>();
        }

        // returns strategy by strategy's name, if the strategy is already in use, returns the strategy already existing
        public StackingStrategy GetFlyweight(string type)
        {
            StackingStrategy strategy = null;

            //check if the strategy exists already
            if (strategies.ContainsKey(type))
            {
                strategy = strategies[type];
            }
            else
            {
                //create a new instance of strategy class
                switch (type)
                {
                    case "bottomWeight":
                        strategy = new BottonWeightStrategy();
                        break;

                    case "pyramid":
                        strategy = new PyramidStrategy();
                        break;

                    case "topple":
                        strategy = new ToppleStrategy();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Bad Strategy Type");
                }

                // add a newly created strategy as it needs to be made as Singleton
                strategies[type] = strategy;
            }

            return strategy;
        }
    }
}
