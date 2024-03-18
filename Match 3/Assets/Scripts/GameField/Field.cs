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
        protected GameObject[,] coordBlockingItems;
        protected int counterDestroyItem;

        public Field()
        {
            coordBlockingItems = new GameObject[SettingsGameField.Width, SettingsGameField.Height];
            coordItems = new GameObject[SettingsGameField.Width, SettingsGameField.Height];
            items = new List<Button>();
        }
    }
}
