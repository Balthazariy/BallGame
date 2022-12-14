using System.Collections.Generic;

namespace Chebureck.Utilities.Gameplay
{
    public class RandomDrop
    {
        private List<Item> _items;
        private float totalWeight;

        public RandomDrop()
        {
            _items = new List<Item>()
            {
                new Item() { id = "green", weight = 50f },
                new Item() { id = "yellow", weight = 25f },
                new Item() { id = "black", weight = 15f },
                new Item() { id = "pink", weight = 15f },
            };

            GetTotalWeight();

            for (int i = 0; i < 500; i++)
            {
                Logger.Log($"Item - [{GetDrop().id}]", Settings.LogTypeEnumerators.Info);
            }
        }

        private void GetTotalWeight()
        {
            foreach (var item in _items)
            {
                totalWeight += item.weight;
            }
        }

        public Item GetDrop()
        {
            float roll = UnityEngine.Random.Range(0f, totalWeight);

            foreach (var item in _items)
            {
                if (item.weight >= roll)
                    return item;

                roll -= item.weight;
            }

            throw new System.Exception("Reward generation exaption!");
        }
    }

    public class Item
    {
        public string id;
        public float weight;
    }
}