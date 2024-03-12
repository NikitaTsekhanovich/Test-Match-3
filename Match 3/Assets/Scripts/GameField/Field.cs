using System.Collections.Generic;
using ItemsEssence;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GameField
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private List<Button> _items;
        private Button[,] _coordItems;

        private Field()
        {
            _coordItems = new Button[SettingsGameField.Width, SettingsGameField.Height];
            _items = new List<Button>();
        }
        
        void Start()
        {
            var dictItems = GetDictItems();
            CreateGameField(dictItems);
        }

        private Dictionary<Button, int> GetDictItems()
        {
            var dictItems = new Dictionary<Button, int>();

            foreach (var item in _items)
                dictItems[item] = item.GetComponent<Item>().MaxCountItem;

            return dictItems;
        }

        private void CreateGameField(Dictionary<Button, int> dictItems)
        {
            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    var newButton = Instantiate(GetItem(dictItems, _items));
                    var transformNewItem = newButton.transform;
                    
                    transformNewItem.SetParent(GameObject.FindWithTag("GameField").transform);
                    transformNewItem.position = new Vector3(-2 + i, -4 + j , 1);
                    transformNewItem.localScale = new Vector3(1, 1, 1);
                    
                    _coordItems[i, j] = newButton;
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
