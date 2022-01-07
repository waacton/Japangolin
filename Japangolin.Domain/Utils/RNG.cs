namespace Wacton.Japangolin.Domain.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RNG
    {
        private static readonly Random Random = new Random();
        
        public static T SelectOne<T>(IEnumerable<T> items)
        {
            var chosenItem = default(T);
            var itemList = items.ToList();

            var itemChosen = false;
            var randomNumber = DoubleBetween(0, itemList.Count);
            foreach (var item in itemList)
            {
                if (randomNumber <= 1)
                {
                    chosenItem = item;
                    itemChosen = true;
                    break;
                }

                randomNumber -= 1;
            }

            if (!itemChosen)
            {
                throw new NullReferenceException("An item has not been chosen");
            }

            return chosenItem;
        }

        public static double DoubleBetween(double minimum, double maximum)
        {
            var range = maximum - minimum;
            return (Random.NextDouble() * range) + minimum;
        }
    }
}