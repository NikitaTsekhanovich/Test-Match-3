using UnityEngine;

namespace GameLogic
{
    public class GameStateHandler : MonoBehaviour, IObserver
    {
        [SerializeField] private Canvas _sreenLose;
        [SerializeField] private Canvas _sreenWin;
        private bool _isWin;
        
        public void OnEnable()
        {
            MoveHandler.OnZeroMovePoint += LoseLevel;
            ScoreHandler.OnPointsOverflow += WinLevel;
        }

        public void OnDisable()
        {
            MoveHandler.OnZeroMovePoint -= LoseLevel;
            ScoreHandler.OnPointsOverflow -= WinLevel;
        }

        private void LoseLevel()
        {
            if (!_isWin)
            {
                _sreenLose.gameObject.SetActive(true);
            }
        }

        private void WinLevel()
        {
            _isWin = true;
            _sreenWin.gameObject.SetActive(true);
        }
    }
}
