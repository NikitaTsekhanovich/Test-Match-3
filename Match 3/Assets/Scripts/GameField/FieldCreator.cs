using System;
using System.Collections.Generic;
using ItemsEssence;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = System.Random;

namespace GameField
{
    public class FieldCreator : FieldUpdater
    {
        public void CrField()
        {
            inputHandler = gameObject.AddComponent<InputHandler>();
            var availableItems = GetAvailableItems();
            CreateGameField(availableItems);
        }

        private Dictionary<Button, int> GetAvailableItems()
        {
            var dictItems = new Dictionary<Button, int>();

            foreach (var item in items)
                dictItems[item] = item.GetComponent<Item>().MaxCountItem;

            return dictItems;
        }

        private void CreateGameField(Dictionary<Button, int> dictItems)
        {
            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    var newButton = Instantiate(GetItem(dictItems, items));
                    SpawnItem(newButton, i, j);
                }
            }
        }

        private Button GetItem(Dictionary<Button, int> dictItems, List<Button> items)
        {
            var random = new Random();
            var randomIndex = random.Next(0, 5);

            if (dictItems[items[randomIndex]] > 0)
            {
                dictItems[items[randomIndex]]--;
                return items[randomIndex];
            }
        
            var currIndex = 0;
            for (var i = 0; i < items.Count; i++)
            {
                if (dictItems[items[i]] > 0)
                {
                    dictItems[items[i]]--;
                    currIndex = i;
                    break;
                }
            }
        
            return items[currIndex];
        }
    }
}
