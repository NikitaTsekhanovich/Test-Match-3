using System;
using UnityEngine;
using TMPro;
using GameField;
using Unity.VisualScripting;

namespace GameLogic
{
    public class MoveHelper : MonoBehaviour, IObserver
    {
        [SerializeField] private TextMeshProUGUI _moveText;
        public static Action ZeroMovePoint;

        public void OnEnable()
        {
            InputHandler.OnMoveChanged += UpdateValue;
        }
        
        public void OnDisable()
        {
            InputHandler.OnMoveChanged -= UpdateValue;
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
                ZeroMovePoint?.Invoke();
            }
        }
    }
}
