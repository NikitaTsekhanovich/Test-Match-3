using UnityEngine;
using TMPro;
using GameField;

namespace GameLogic
{
    public class MoveHelper : MonoBehaviour, IObserver
    {
        [SerializeField] private TextMeshProUGUI _moveText;

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
            // если меньше 0, то проиграли и вызываем экран проигрыша 
        }
    }
}
