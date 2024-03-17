using System;
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
            
            var currItemType = FieldUpdater.Instance.GetTypeCurrentItem(indexesItem);
            FieldUpdater.Instance.UpdateGameField(indexesItem);
            var counterDestroyItem = FieldUpdater.Instance.GetCountDestroyItem();
      
            OnScoreChanged?.Invoke(currItemType, counterDestroyItem);
            OnMoveChanged?.Invoke();
        }
    }
}
