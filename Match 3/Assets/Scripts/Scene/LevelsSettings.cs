using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Scene
{
    public class LevelsSettings : MonoBehaviour
    {
        public static LevelsSettings instance { get; private set; }

        [SerializeField] private List<GameObject> _imagesItem = new List<GameObject>();
        [SerializeField] private List<GameObject> _blockingItems = new List<GameObject>();

        internal const int GoalLevel1 = 5;
        internal GameObject ImageGoalItemLevel1 { get; private set; }

        internal const int GoalLevel2 = 10;
        internal GameObject ImageGoalItemLevel2 { get; private set; }
        
        internal const int GoalLevel3 = 15;
        internal GameObject ImageGoalItemLevel3 { get; private set; }
        
        internal const int GoalLevel4 = 25;
        internal GameObject ImageGoalItemLevel4 { get; private set; }
        
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
            
            var random = new Random();
            var randomIndex = random.Next(0, _imagesItem.Count);
            
            ImageGoalItemLevel1 = _imagesItem[randomIndex];
            ImageGoalItemLevel2 = _imagesItem[randomIndex];
            ImageGoalItemLevel3 = _imagesItem[randomIndex];
            ImageGoalItemLevel4 = _imagesItem[randomIndex];
        }
    }
}
