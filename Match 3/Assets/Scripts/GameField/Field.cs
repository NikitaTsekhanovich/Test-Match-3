using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameField
{
    public class Field : MonoBehaviour
    {
        [SerializeField] protected List<Button> items;
        [SerializeField] protected InputHandler inputHandler;
        protected GameObject[,] coordItems;
        protected int counterDestroyItem;

        public Field()
        {
            coordItems = new GameObject[SettingsGameField.Width, SettingsGameField.Height];
            items = new List<Button>();
        }
    }
}
