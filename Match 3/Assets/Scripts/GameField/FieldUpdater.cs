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
                // DontDestroyOnLoad(this.gameObject);
                return;
            }
            
            Destroy(gameObject);
        }
        
        public int GetCountDestroyItem() => counterDestroyItem;
        
        public ItemTypes GetTypeCurrentItem(char[] indexesItem) =>
            coordItems[indexesItem[0] - '0', indexesItem[1] - '0'].GetComponent<Item>().ItemType;

        protected void SpawnItem(Button newButton, int i, int j)
        {
            var transformNewItem = newButton.transform;

            newButton.onClick.AddListener(inputHandler.Click);
            transformNewItem.SetParent(GameObject.FindWithTag("GameField").transform);
            transformNewItem.position = new Vector3(-2 + i, -4 + j, 1);
            transformNewItem.localScale = new Vector3(1, 1, 1);
            newButton.name = $"{newButton.GetComponent<Item>().ItemType} {i}{j}";

            coordItems[i, j] = newButton.gameObject;
        }

        public void UpdateGameField(char[] indexesItem)
        {
            CheckIntersectionItem(indexesItem);
            // SpawnItem();
        }

        private void CheckIntersectionItem(char[] indexesItem)
        {
            var iCurrItem = indexesItem[0] - '0';
            var jCurrItem = indexesItem[1] - '0';
            var typeCurrItem = GetTypeCurrentItem(indexesItem);
          
            counterDestroyItem = 0;
            FindItemMatches(iCurrItem, jCurrItem, typeCurrItem);
            // UpdateSpawnItem();
        }

        private void FindItemMatches(int i, int j, ItemTypes typeCurrItem)
        {
            // Move left
            // CheckSide(i - 1, j, typeCurrItem);
            if (i - 1 > -1)
            {
                if (coordItems[i - 1, j] != null)
                {
                    if (coordItems[i - 1, j].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        // _coordItems[i - 1, j].GetComponent<AudioSource>().Play();
                        DestroyItem(i - 1, j);
                        FindItemMatches(i - 1, j, typeCurrItem);
                    }
                }
            }
            // Move up
            if (j + 1 < 8)
            {
                if (coordItems[i, j + 1] != null)
                {
                    if (coordItems[i, j + 1].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        // _coordItems[i, j + 1].GetComponent<AudioSource>().Play();
                        DestroyItem(i, j + 1);
                        FindItemMatches(i, j + 1, typeCurrItem);
                    }
                }
            }
            // Move down
            if (j - 1 > -1)
            {
                if (coordItems[i, j - 1] != null)
                {
                    if (coordItems[i, j - 1].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        // _coordItems[i, j - 1].GetComponent<AudioSource>().Play();
                        DestroyItem(i, j - 1);
                        FindItemMatches(i, j - 1, typeCurrItem);
                    }
                }
            }
            // Move right
            if (i + 1 < 5)
            {
                if (coordItems[i + 1, j] != null)
                {
                    if (coordItems[i + 1, j].GetComponent<Item>().ItemType == typeCurrItem)
                    {
                        // _coordItems[i + 1, j].GetComponent<AudioSource>().Play();
                        DestroyItem(i + 1, j);
                        FindItemMatches(i + 1, j, typeCurrItem);
                    }
                }
            }
        }

        private void DestroyItem(int i, int j)
        {
            coordItems[i, j].GetComponent<ItemAnimator>().AnimationDestroyItem();
            // Destroy(coordItems[i, j]);
            coordItems[i, j] = null;
            counterDestroyItem++;
        }

        // private void CheckSide(int i, int j, ItemTypes typeCurrItem, int fieldLimitation)
        // {
        //     if (i > fieldLimitation)
        //     {
        //         if (coordItems[i, j] != null)
        //         {
        //             if (coordItems[i, j].GetComponent<Item>().ItemType == typeCurrItem)
        //             {
        //                 // _coordItems[i - 1, j].GetComponent<AudioSource>().Play();
        //                 Destroy(coordItems[i, j]);
        //                 coordItems[i, j] = null;
        //                 counterDestroyItem++;
        //                 FindItemMatches(i, j, typeCurrItem);
        //             }
        //         }
        //     }
        // }

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
