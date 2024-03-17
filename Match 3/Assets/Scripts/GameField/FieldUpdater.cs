using ItemsEssence;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GameField
{
    public class FieldUpdater : Field
    {
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

        protected void SpawnItem(Button newButton, int i, int j)
        {
            var transformNewItem = newButton.transform;

            newButton.onClick.AddListener(inputHandler.Click);
            transformNewItem.SetParent(GameObject.FindWithTag("GameField").transform);
            transformNewItem.position = new Vector3(-2 + i, -4 + j, 1);
            transformNewItem.localScale = new Vector3(0, 0, 0);
            newButton.name = $"{newButton.GetComponent<Item>().ItemType} {i}{j}";

            coordItems[i, j] = newButton.gameObject;
            coordItems[i, j].GetComponent<ItemAnimator>().AnimationInstantiateItem();
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
            if (fieldLimitation)
            {
                if (coordItems[i, j] != null)
                {
                    if (coordItems[i, j].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        DestroyItem(i, j);
                        FindItemMatches(i, j, typeCurrItem);
                    }
                }
            }
        }

        private void DestroyItem(int i, int j)
        {
            coordItems[i, j].GetComponent<ItemAnimator>().AnimationDestroyItem();
            coordItems[i, j] = null;
            counterDestroyItem++;
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
                        SpawnItem(newButton, i, j);
                    }
                }
            }
        }
    }
}
