using System;
using System.Collections.Generic;
using GameField;
using ItemsEssence;
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
        private ItemTypes _currentItemType;

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
            var randomNumber = random.Next(3, 5);

            _goalText.text = $"Goal: {randomNumber}x";
        }
        
        public void OnEnable()
        {
            InputHandler.OnScoreChanged += UpdateValue;
        }
        
        public void OnDisable()
        {
            InputHandler.OnScoreChanged -= UpdateValue;
        }

        private void UpdateValue(ItemTypes itemType, int countItem)
        {
            Debug.Log(_currentItemType);
            if (itemType == _currentItemType)
            {
                var newScore = 0;

                for (var i = 1; i < countItem + 1; i++)
                    newScore += i * 2;

                _scoreText.text = $"{newScore + int.Parse(_scoreText.text)}";
            }

            // if (int.Parse(_scoreText.text) >= int.Parse(_goalText.text))
            // {
            //     Debug.Log("Победа и некст левел");
            //     // если больше _goalText.text, то экран победы и переход на некст левел
            // }
        }
    }
}
