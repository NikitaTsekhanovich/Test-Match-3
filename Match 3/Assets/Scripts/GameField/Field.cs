using System;
using System.Collections.Generic;
using ItemsEssence;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = System.Random;

namespace GameField
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private List<Button> _items;
        [SerializeField] private InputHandler _inputHandler;
        private GameObject[,] _coordItems;
        public static Field Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(this.gameObject);
                return;
            }
            
            Destroy(this.gameObject);
        }

        public Field()
        {
            _coordItems = new GameObject[SettingsGameField.Width, SettingsGameField.Height];
            _items = new List<Button>();
        }

        void Start()
        {
            _inputHandler = gameObject.AddComponent<InputHandler>();
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
                    
                    newButton.onClick.AddListener(_inputHandler.Click);
                    transformNewItem.SetParent(GameObject.FindWithTag("GameField").transform);
                    transformNewItem.position = new Vector3(-2 + i, -4 + j , 1);
                    transformNewItem.localScale = new Vector3(1, 1, 1);
                    newButton.name = $"{newButton.GetComponent<Item>().ItemType} {i}{j}";
                    
                    _coordItems[i, j] = newButton.gameObject;
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

        public void UpdateGameField(char[] indexesItem)
        {
            DestroyItem(indexesItem);
            SpawnItem();
        }

        private void DestroyItem(char[] indexesItem)
        {
            var iCurrItem = indexesItem[0] - '0';
            var jCurrItem = indexesItem[1] - '0';
            var typeCurrItem = _coordItems[iCurrItem, jCurrItem].GetComponent<Item>().ItemType;

            FindAllItems(iCurrItem, jCurrItem, typeCurrItem);
            foreach (var coordItem in _coordItems)
            {
                if (coordItem == null)
                {
                    Destroy(_coordItems[iCurrItem, jCurrItem]);
                    _coordItems[iCurrItem, jCurrItem] = null;
                    break;
                }
            }
        }

        private void FindAllItems(int i, int j, ItemTypes typeCurrItem)
        {
            //left
            if (i - 1 > -1)
            {
                if (_coordItems[i - 1, j] != null)
                {
                    if (_coordItems[i - 1, j].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        Destroy(_coordItems[i - 1, j]);
                        _coordItems[i - 1, j] = null;
                        FindAllItems(i - 1, j, typeCurrItem);
                    }
                }
            }
            //up
            if (j + 1 < 8)
            {
                if (_coordItems[i, j + 1] != null)
                {
                    if (_coordItems[i, j + 1].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        Destroy(_coordItems[i, j + 1]);
                        _coordItems[i, j + 1] = null;
                        FindAllItems(i, j + 1, typeCurrItem);
                    }
                }
            }
            //down
            if (j - 1 > -1)
            {
                if (_coordItems[i, j - 1] != null)
                {
                    if (_coordItems[i, j - 1].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        Destroy(_coordItems[i, j - 1]);
                        _coordItems[i, j - 1] = null;
                        FindAllItems(i, j - 1, typeCurrItem);
                    }
                }
            }
            //right
            if (i + 1 < 5)
            {
                if (_coordItems[i + 1, j] != null)
                {
                    if (_coordItems[i + 1, j].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        Destroy(_coordItems[i + 1, j]);
                        _coordItems[i + 1, j] = null;
                        FindAllItems(i + 1, j, typeCurrItem);
                    }
                }
            }
        }

        private void SpawnItem()
        {
            
        }
    }
}
