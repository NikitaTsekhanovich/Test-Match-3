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
            MoveHelper.ZeroMovePoint += LoseLevel;
            ScoreHelper.PointsOverflow += WinLevel;
        }

        public void OnDisable()
        {
            MoveHelper.ZeroMovePoint -= LoseLevel;
            ScoreHelper.PointsOverflow -= WinLevel;
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
