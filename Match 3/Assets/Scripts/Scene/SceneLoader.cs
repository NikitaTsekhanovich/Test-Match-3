using System;
using GameField;
using ItemsEssence;
using UnityEngine;

namespace Scene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private FieldCreator _fieldCreator;
        public static Action OnMoveChanged;
        public static Action<ItemTypes, int> OnScoreChanged;

        private void Start()
        {
            LoadGameField(false);
        }

        private void LoadGameField(bool isRetry)
        {
            _fieldCreator.CreateField(isRetry);
        }

        public void RetryGameField()
        {
            LoadGameField(true);
            OnMoveChanged?.Invoke();
            OnScoreChanged?.Invoke(ItemTypes.Empty, -1);
        }
    }
}
