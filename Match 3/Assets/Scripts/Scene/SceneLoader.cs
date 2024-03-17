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
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel1,
                    LevelsSettings.instance.ImageGoalItemLevel1);
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel2,
                    LevelsSettings.instance.ImageGoalItemLevel2);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel3,
                    LevelsSettings.instance.ImageGoalItemLevel3);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(
                    LevelsSettings.GoalLevel4,
                    LevelsSettings.instance.ImageGoalItemLevel4);
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
