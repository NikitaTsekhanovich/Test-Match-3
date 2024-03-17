using System.Collections.Generic;
using ItemsEssence;
using UnityEngine.UI;
using Random = System.Random;

namespace GameField
{
    public class FieldCreator : FieldUpdater
    {
        public void CreateField(bool isRetry)
        {
            if (isRetry)
            {
                ClearField();
            }
            inputHandler = gameObject.AddComponent<InputHandler>();
            var availableItems = GetAvailableItems();
            FillGameField(availableItems);
        }

        private void ClearField()
        {
            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    Destroy(coordItems[i, j]);
                    coordItems[i, j] = null;
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
                    SpawnItem(newButton, i, j);
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
