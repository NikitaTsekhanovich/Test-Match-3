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
        public static Action<int> OnLoadScore;
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
                OnLoadScore?.Invoke(LevelsSettings.GoalLevel1);
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(LevelsSettings.GoalLevel2);
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(LevelsSettings.GoalLevel3);
            }
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                _fieldCreator.CreateField(isRetry);
                OnLoadScore?.Invoke(LevelsSettings.GoalLevel4);
            }
        }

        public void RetryGameField()
        {
            LoadGameField(true);
            OnMoveChanged?.Invoke();
            OnScoreChanged?.Invoke(ItemTypes.Empty, -1);
            OnRetryProgressBar.Invoke();
        }
    }
}
