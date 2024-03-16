using UnityEngine;

namespace GameLogic
{
    public class LevelHelper : MonoBehaviour, IObserver
    {
        [SerializeField] private Canvas _sreenLose;
        [SerializeField] private Canvas _sreenWin;
        private bool _isWin;
        
        public void OnEnable()
        {
            MoveHelper.OnZeroMovePoint += LoseLevel;
            ScoreHelper.OnPointsOverflow += WinLevel;
        }

        public void OnDisable()
        {
            MoveHelper.OnZeroMovePoint -= LoseLevel;
            ScoreHelper.OnPointsOverflow -= WinLevel;
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
