using System;
using UnityEngine;
using TMPro;
using GameField;
using Scene;

namespace GameLogic
{
    public class MoveHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private TextMeshProUGUI _moveText;
        
        public static Action OnZeroMovePoint;

        public void OnEnable()
        {
            InputHandler.OnMoveChanged += UpdateValue;
            SceneLoader.OnMoveChanged += ResetMove;
        }
        
        public void OnDisable()
        {
            InputHandler.OnMoveChanged -= UpdateValue;
            SceneLoader.OnMoveChanged -= ResetMove;
        }

        private void ResetMove()
        {
            _moveText.text = "40";
        }
        
        private void UpdateValue()
        {
            _moveText.text = (int.Parse(_moveText.text) - 1).ToString();
            CheckStateMove();
        }

        private void CheckStateMove()
        {
            if (int.Parse(_moveText.text) <= 0)
            {
                OnZeroMovePoint?.Invoke();
            }
        }
    }
}
