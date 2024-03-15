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
    public class ScoreHelper : MonoBehaviour, IObserver
    {
        [SerializeField] private List<GameObject> _imagesItem;
        [SerializeField] private SpriteRenderer _currentImage;
        [SerializeField] private TextMeshProUGUI _goalText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _randomGoal;
        private ItemTypes _currentItemType;

        public static Action PointsOverflow; 

        public ScoreHelper()
        {
            _imagesItem = new List<GameObject>();
        }

        private void Start()
        {
            LoadImageScore();
            LoadGoalItem();
        }

        private void LoadImageScore()
        {
            var random = new Random();
            var randomIndex = random.Next(0, 5);

            _currentItemType = _imagesItem[randomIndex].GetComponent<Item>().ItemType;
            _currentImage.sprite = _imagesItem[randomIndex].GetComponent<Image>().sprite;
        }

        private void LoadGoalItem()
        {
            var random = new Random();
            _randomGoal = random.Next(4, 6);

            _goalText.text = $"Goal: {_randomGoal}x";
        }
        
        public void OnEnable()
        {
            InputHandler.OnScoreChanged += UpdateValue;
            SceneLoader.OnScoreChanged += ResetScore;
        }
        
        public void OnDisable()
        {
            InputHandler.OnScoreChanged -= UpdateValue;
            SceneLoader.OnScoreChanged -= ResetScore;
        }

        private void ResetScore(ItemTypes itemType, int countItem)
        {
            _scoreText.text = "0";
        }

        private void UpdateValue(ItemTypes itemType, int countItem)
        {
            if (itemType == _currentItemType)
            {
                // var newScore = 0;
                //
                // for (var i = 1; i < countItem + 1; i++)
                //     newScore += i * 2;
                //
                // _scoreText.text = $"{newScore + int.Parse(_scoreText.text)}";
                _scoreText.text = $"{countItem + int.Parse(_scoreText.text)}";
            }

            CheckState();
        }

        private void CheckState()
        {
            if (int.Parse(_scoreText.text) >= _randomGoal)
            {
                PointsOverflow?.Invoke();
            }
        }
    }
}
