using System;
using GameField;
using UnityEngine;

namespace Scene
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private FieldCreator _fieldCreator;

        private void Start()
        {
            LoadGameField();
        }

        public void LoadGameField()
        {
            _fieldCreator.CrField();
        }
    }
}
