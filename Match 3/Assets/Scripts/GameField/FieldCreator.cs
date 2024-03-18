using System.Collections.Generic;
using ItemsEssence;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GameField
{
    public class FieldCreator : FieldUpdater
    {
        public void CreateField(
            bool isRetry, 
            int countBlockingItems, 
            Button blockingItem,
            int[,] setupCoordItems)
        {
            if (isRetry)
            {
                ClearField(coordItems);
                ClearField(coordBlockingItems);
            }
            
            inputHandler = gameObject.AddComponent<InputHandler>();
            var availableItems = GetAvailableItems();
            FillGameField(availableItems);
            
            if (setupCoordItems != null &&
                blockingItem.GetComponent<Item>().ItemType == ItemTypes.Rock)
            {
                SpawnBlockingItem(countBlockingItems, blockingItem, setupCoordItems);
            }
        }

        private void SpawnBlockingItem(
            int countBlockingItems, 
            Button blockingItem, 
            int[,] setupCoordItems)
        {
            for (var i = 0; i < countBlockingItems; i++)
            {
                var newBlockingItem = Instantiate(blockingItem);
                
                SpawnItem(
                    newBlockingItem,
                    setupCoordItems[i, 0],
                    setupCoordItems[i, 1],
                    coordBlockingItems);
            }
        }

        private void ClearField(GameObject[,] coord)
        {
            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    Destroy(coord[i, j]);
                    coord[i, j] = null;
                }
            }
        }

        private void FillGameField(Dictionary<Button, int> availableItems)
        {
            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    var newButton = Instantiate(GetItem(availableItems, items));
                    SpawnItem(newButton, i, j, coordItems);
                }
            }
        }
        
        private Dictionary<Button, int> GetAvailableItems()
        {
            var dictItems = new Dictionary<Button, int>();

            foreach (var item in items)
                dictItems[item] = item.GetComponent<Item>().MaxCountItem;

            return dictItems;
        }

        private Button GetItem(Dictionary<Button, int> availableItems, List<Button> items)
        {
            var random = new Random();
            var randomIndex = random.Next(0, items.Count);

            if (availableItems[items[randomIndex]] > 0)
            {
                availableItems[items[randomIndex]]--;
                return items[randomIndex];
            }
        
            var currIndex = 0;
            for (var i = 0; i < items.Count; i++)
            {
                if (availableItems[items[i]] > 0)
                {
                    availableItems[items[i]]--;
                    currIndex = i;
                    break;
                }
            }
        
            return items[currIndex];
        }
    }
}
