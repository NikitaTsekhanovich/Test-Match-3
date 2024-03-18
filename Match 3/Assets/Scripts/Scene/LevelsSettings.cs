using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Scene
{
    public class LevelsSettings : MonoBehaviour
    {
        public static LevelsSettings instance { get; private set; }

        [SerializeField] private List<GameObject> _imagesItem = new List<GameObject>();
        [SerializeField] private List<Button> _blockingItems = new List<Button>();

        internal const int GoalLevel1 = 5;
        internal GameObject ImageGoalItemLevel1 { get; private set; }
        

        internal const int GoalLevel2 = 10;
        internal GameObject ImageGoalItemLevel2 { get; private set; }
        
        
        internal const int GoalLevel3 = 4;
        internal Button ImageGoalItemLevel3 { get; private set; }
        internal int[,] coordBlockingItemsLevel3;
        
        
        internal const int GoalLevel4 = 8;
        internal Button ImageGoalItemLevel4 { get; private set; }
        internal int[,] coordBlockingItemsLevel4;
        
        
        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            LoadSettingsLevel1();
            LoadSettingsLevel2();
            LoadSettingsLevel3();
            LoadSettingsLevel4();
        }

        private void LoadSettingsLevel1()
        {
            var random = new Random();
            var randomIndex = random.Next(0, _imagesItem.Count);
            ImageGoalItemLevel1 = _imagesItem[randomIndex];
        }
        
        private void LoadSettingsLevel2()
        {
            var random = new Random();
            var randomIndex = random.Next(0, _imagesItem.Count);
            ImageGoalItemLevel2 = _imagesItem[randomIndex];
        }
        
        private void LoadSettingsLevel3()
        {
            ImageGoalItemLevel3 = _blockingItems[0];
            coordBlockingItemsLevel3 = new [,]
            {
                {1, 1},
                {3, 6},
                {1, 6},
                {3, 1}
            };
        }
        
        private void LoadSettingsLevel4()
        {
            ImageGoalItemLevel4 = _blockingItems[0];
            coordBlockingItemsLevel4 = new [,]
            {
                {1, 1},
                {3, 6},
                {1, 6},
                {3, 1},
                {2, 5},
                {4, 0},
                {1, 2},
                {4, 6}
            };
        }
    }
}
