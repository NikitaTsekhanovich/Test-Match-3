using System;
using ItemsEssence;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GameField
{
    public class FieldUpdater : Field
    {
        public static Action<ItemTypes, int> OnScoreChanged;
        public static FieldUpdater Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            
            Destroy(gameObject);
        }
        
        public int GetCountDestroyItem() => counterDestroyItem;
        
        public ItemTypes GetTypeCurrentItem(char[] indexesItem) =>
            coordItems[indexesItem[0] - '0', indexesItem[1] - '0'].GetComponent<Item>().ItemType;
        
        public void UpdateGameField(char[] indexesItem)
        {
            CheckIntersectionItem(indexesItem);
        }

        protected void SpawnItem(Button newButton, int i, int j, GameObject[,] coords)
        {
            var transformNewItem = newButton.transform;

            if (newButton.GetComponent<Item>().ItemType != ItemTypes.Rock)
                newButton.onClick.AddListener(inputHandler.Click);

            transformNewItem.SetParent(GameObject.FindWithTag("GameField").transform);
            transformNewItem.localPosition = new Vector3(-330 + i * 163, -610 + j * 177, 1);
            newButton.name = $"{newButton.GetComponent<Item>().ItemType} {i}{j}";

            coords[i, j] = newButton.gameObject;
            coords[i, j].GetComponent<ItemAnimator>().AnimationInstantiateItem();
        }

        private void CheckIntersectionItem(char[] indexesItem)
        {
            var iCurrItem = indexesItem[0] - '0';
            var jCurrItem = indexesItem[1] - '0';
            var typeCurrItem = GetTypeCurrentItem(indexesItem);
          
            counterDestroyItem = 0;
            FindItemMatches(iCurrItem, jCurrItem, typeCurrItem);
            UpdateSpawnItem();
        }

        private void FindItemMatches(int i, int j, ItemTypes typeCurrItem)
        {
            // Move left
            CheckSide(i - 1, j, typeCurrItem, i - 1 > -1);
            // Move up
            CheckSide(i, j + 1, typeCurrItem, j + 1 < 8);
            // Move down
            CheckSide(i, j - 1, typeCurrItem, j - 1 > -1);
            // Move right
            CheckSide(i + 1, j, typeCurrItem, i + 1 < 5);
        }
        
        private void CheckSide(int i, int j, ItemTypes typeCurrItem, bool fieldLimitation)
        {
            if (fieldLimitation && 
                coordBlockingItems[i, j] != null &&
                counterDestroyItem >= 1)
            {
                OnScoreChanged?.Invoke(
                    coordBlockingItems[i, j].GetComponent<Item>().ItemType, 1);
                DestroyItem(i, j, coordBlockingItems);
                return;
            }
            
            if (fieldLimitation && 
                coordItems[i, j] != null &&
                coordItems[i, j].GetComponent<Item>().ItemType == typeCurrItem)
            {
                DestroyItem(i, j, coordItems);
                counterDestroyItem++;
                FindItemMatches(i, j, typeCurrItem);
            }
        }

        private void DestroyItem(int i, int j, GameObject[,] coord)
        {
            coord[i, j].GetComponent<ItemAnimator>().AnimationDestroyItem();
            coord[i, j] = null;
        }

        private void UpdateSpawnItem()
        {
            var random = new Random();

            for (var i = 0; i < SettingsGameField.Width; i++)
            {
                for (var j = 0; j < SettingsGameField.Height; j++)
                {
                    if (coordItems[i, j] == null)
                    {
                        var randomIndex = random.Next(0, 5);

                        var newButton = Instantiate(items[randomIndex]);
                        SpawnItem(newButton, i, j, coordItems);
                    }
                }
            }
        }
    }
}
