using System;
using System.Collections.Generic;
using GameField;
using ItemsEssence;
using Scene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace GameLogic
{
    public class ScoreHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private List<GameObject> _imagesItem;
        [SerializeField] private Image _currentImage;
        [SerializeField] private TextMeshProUGUI _goalText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _goalScore;
        private ItemTypes _currentItemType;

        public static Action OnPointsOverflow;
        public static Action<float, int> OnChangeProgressBar;

        public ScoreHandler()
        {
            _imagesItem = new List<GameObject>();
        }

        public void OnEnable()
        {
            InputHandler.OnScoreChanged += UpdateValue;
            SceneLoader.OnScoreChanged += ResetScore;
            SceneLoader.OnLoadScore += LoadScore;
            FieldUpdater.OnScoreChanged += UpdateValue;
        }
        
        public void OnDisable()
        {
            InputHandler.OnScoreChanged -= UpdateValue;
            SceneLoader.OnScoreChanged -= ResetScore;
            SceneLoader.OnLoadScore -= LoadScore;
            FieldUpdater.OnScoreChanged -= UpdateValue;
        }

        private void LoadScore(int goal, GameObject imageGoal)
        {
            LoadImageGoal(imageGoal);
            LoadScoreGoal(goal);
        }

        private void LoadImageGoal(GameObject imageGoal)
        {
            _currentItemType = imageGoal.GetComponent<Item>().ItemType;
            _currentImage.sprite = imageGoal.GetComponent<Image>().sprite;
        }

        private void LoadScoreGoal(int goal)
        {
            _goalScore = goal;
            _goalText.text = $"Goal: {_goalScore}x";
        }

        private void ResetScore(ItemTypes itemType, int countItem)
        {
            _scoreText.text = "0";
        }

        private void UpdateValue(ItemTypes itemType, int countItem)
        {
            if (itemType == _currentItemType)
            {
                _scoreText.text = $"{countItem + int.Parse(_scoreText.text)}";
            }

            OnChangeProgressBar?.Invoke(int.Parse(_scoreText.text), _goalScore);
            CheckState();
        }

        private void CheckState()
        {
            if (int.Parse(_scoreText.text) >= _goalScore)
            {   
                OnPointsOverflow?.Invoke();
            }
        }
    }
}
