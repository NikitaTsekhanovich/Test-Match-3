using System;
using System.Collections.Generic;
using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class ProgressBarHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private List<Image> _currentStars1;
        [SerializeField] private List<Image> _currentStars2;
        [SerializeField] private List<Image> _currentStars3;
        [SerializeField] private List<Image> _bestScoreStars1;
        [SerializeField] private List<Image> _bestScoreStars2;
        [SerializeField] private List<Image> _bestScoreStars3;

        public static Action<int> OnSaveBestScore;

        public void OnEnable()
        {
            ScoreHandler.OnChangeProgressBar += ChangeProgressBar;
            SceneLoader.OnRetryProgressBar += RetryProgressBar;
        }

        public void OnDisable()
        {
            ScoreHandler.OnChangeProgressBar -= ChangeProgressBar;
            SceneLoader.OnRetryProgressBar -= RetryProgressBar;
        }

        private void ChangeProgressBar(float currentScore, int goalScore)
        {
            try
            {
                var currentProgress = currentScore / goalScore;

                _progressBar.fillAmount = currentProgress;
                
                ChangeStarsScore(currentProgress);
                ChangeBestStarsScore();
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void ChangeStateStar(List<Image> stars, bool isActive)
        {
            foreach (var star in stars)
            {
                star.gameObject.SetActive(isActive);
            }
        }

        private void ChangeStarsScore(float currentProgress)
        {
            OnSaveBestScore?.Invoke(0);

            if (currentProgress >= 0.3f)
            {
                ChangeStateStar(_currentStars1, true);
                OnSaveBestScore?.Invoke(1);
            }

            if (currentProgress >= 0.64f)
            {
                ChangeStateStar(_currentStars2, true);
                OnSaveBestScore?.Invoke(2);
            }

            if (currentProgress >= 0.96f)
            {
                ChangeStateStar(_currentStars3, true);
                OnSaveBestScore?.Invoke(3);
            }
        }

        private void ChangeBestStarsScore()
        {
            var countStars = SaveHandler.GetSaveBestCountStars();

            if (countStars >= 1)
            {
                ChangeStateStar(_bestScoreStars1, true);
            }
            if (countStars >= 2)
            {
                ChangeStateStar(_bestScoreStars2, true);
            }
            if (countStars >= 3)
            {
                ChangeStateStar(_bestScoreStars3, true);
            }
        }

        private void RetryProgressBar()
        {
            _progressBar.fillAmount = 0;
            ChangeStateStar(_currentStars1, false);
            ChangeStateStar(_currentStars2, false);
            ChangeStateStar(_currentStars3, false);
        }
    }
}
