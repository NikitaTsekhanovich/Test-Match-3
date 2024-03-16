using System;
using Scene;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class ProgressBarHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Image _star1;
        [SerializeField] private Image _star2;
        [SerializeField] private Image _star3;


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

                if (currentProgress >= 0.3f)
                    _star1.gameObject.SetActive(true);
                if (currentProgress >= 0.64f)
                    _star2.gameObject.SetActive(true);
                if (currentProgress >= 0.96f)
                    _star3.gameObject.SetActive(true);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void RetryProgressBar()
        {
            _progressBar.fillAmount = 0;
            _star1.gameObject.SetActive(false);
            _star2.gameObject.SetActive(false);
            _star3.gameObject.SetActive(false);
        }
    }
}
