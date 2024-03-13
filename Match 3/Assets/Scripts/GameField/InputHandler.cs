using System;
using GameLogic;
using ItemsEssence;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameField
{
    public class InputHandler : MonoBehaviour
    {
        public static Action OnMoveChanged;
        public static Action<ItemTypes, int> OnScoreChanged;

        public void Click()
        {
            var item = EventSystem.current.currentSelectedGameObject;
            var indexesItem = item.name.Split(' ')[1].ToCharArray();
            
            var currItemType = Field.Instance.GetTypeCurrentItem(indexesItem);
            Field.Instance.UpdateGameField(indexesItem);

            OnMoveChanged?.Invoke();
            OnScoreChanged?.Invoke(currItemType, 1);
        }

        
    }
}
