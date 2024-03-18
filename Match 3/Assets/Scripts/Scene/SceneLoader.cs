using System;
using GameField;
using ItemsEssence;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private FieldCreator _fieldCreator;
        
        public static Action OnMoveChanged;
        public static Action<ItemTypes, int> OnScoreChanged;
        public static Action<int, GameObject> OnLoadScore;
        public static Action OnRetryProgressBar;

        private void Start()
        {
            LoadGameField(false);
        }

        private void LoadGameField(bool isRetry)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _fieldCreator.CreateField(
                    isRetry, 
                    LevelsSettings.GoalLevel1,
                    null,
                    null);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel1,
                    LevelsSettings.instance.ImageGoalItemLevel1.gameObject);
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _fieldCreator.CreateField(
                    isRetry, 
                    LevelsSettings.GoalLevel2,
                    null,
                    null);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel2,
                    LevelsSettings.instance.ImageGoalItemLevel2.gameObject);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                _fieldCreator.CreateField(
                    isRetry, 
                    LevelsSettings.GoalLevel3,
                    LevelsSettings.instance.ImageGoalItemLevel3,
                    LevelsSettings.instance.coordBlockingItemsLevel3);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel3,
                    LevelsSettings.instance.ImageGoalItemLevel3.gameObject);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                _fieldCreator.CreateField(
                    isRetry, 
                    LevelsSettings.GoalLevel4,
                    LevelsSettings.instance.ImageGoalItemLevel4,
                    LevelsSettings.instance.coordBlockingItemsLevel4);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel4,
                    LevelsSettings.instance.ImageGoalItemLevel4.gameObject);
            }
        }

        public void RetryGameField()
        {
            LoadGameField(true);
            OnMoveChanged?.Invoke();
            OnScoreChanged?.Invoke(ItemTypes.Empty, -1);
            OnRetryProgressBar?.Invoke();
        }
    }
}
