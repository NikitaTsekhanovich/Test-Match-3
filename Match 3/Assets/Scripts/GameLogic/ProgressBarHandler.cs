using System;
using System.Collections.Generic;
using Scene;
using UnityEngine;
using UnityEngine.Serialization;
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
            ScoreHelper.OnChangeProgressBar += ChangeProgressBar;
            SceneLoader.OnRetryProgressBar += RetryProgressBar;
        }

        public void OnDisable()
        {
            ScoreHelper.OnChangeProgressBar -= ChangeProgressBar;
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

        private void ChangeStarsScore(float currentProgress)
        {
            OnSaveBestScore?.Invoke(0);

            if (currentProgress >= 0.3f)
            {
                foreach (var star1 in _currentStars1)
                {
                    star1.gameObject.SetActive(true);
                }

                OnSaveBestScore?.Invoke(1);
            }

            if (currentProgress >= 0.64f)
            {
                foreach (var star2 in _currentStars2)
                {
                    star2.gameObject.SetActive(true);
                }

                OnSaveBestScore?.Invoke(2);
            }

            if (currentProgress >= 0.96f)
            {
                foreach (var star3 in _currentStars3)
                {
                    star3.gameObject.SetActive(true);
                }

                OnSaveBestScore?.Invoke(3);
            }
        }

        private void ChangeBestStarsScore()
        {
            var countStars = SaveHandler.GetSaveBestCountStars();

            if (countStars >= 1)
            {
                foreach (var star1 in _bestScoreStars1)
                {
                    star1.gameObject.SetActive(true);
                }
            }
            if (countStars >= 2)
            {
                foreach (var star2 in _bestScoreStars2)
                {
                    star2.gameObject.SetActive(true);
                }
            }
            if (countStars >= 3)
            {
                foreach (var star3 in _bestScoreStars3)
                {
                    star3.gameObject.SetActive(true);
                }
            }
        }

        private void RetryProgressBar()
        {
            _progressBar.fillAmount = 0;
            foreach (var star1 in _currentStars1)
            {
                star1.gameObject.SetActive(false);
            }
            foreach (var star2 in _currentStars2)
            {
                star2.gameObject.SetActive(false);
            }
            foreach (var star3 in _currentStars3)
            {
                star3.gameObject.SetActive(false);
            }
        }
    }
}
