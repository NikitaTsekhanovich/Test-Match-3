using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class SaveHandler : MonoBehaviour, IObserver
    {
        public void OnEnable()
        {
            ProgressBarHandler.OnSaveBestScore += LoadSaveData;
        }

        public void OnDisable()
        {
            ProgressBarHandler.OnSaveBestScore -= LoadSaveData;
        }

        private void LoadSaveData(int countStars)
        {
            var indexScene = SceneManager.GetActiveScene().buildIndex;
            var bestScore = GetSaveBestCountStars();
           
            if (bestScore < countStars)
            {
                PlayerPrefs.SetInt($"Level{indexScene}CountStars", countStars);
            }
            // PlayerPrefs.DeleteAll(); // сброс сохраненного счета
        }

        public static int GetSaveBestCountStars()
        {
            var indexScene = SceneManager.GetActiveScene().buildIndex;
            return PlayerPrefs.GetInt($"Level{indexScene}CountStars");
        }
    }
}
