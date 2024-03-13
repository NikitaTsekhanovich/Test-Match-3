using UnityEngine;
using UnityEngine.EventSystems;

namespace GameField
{
    public class InputHandler : MonoBehaviour
    {
        public void Click()
        {
            var item = EventSystem.current.currentSelectedGameObject;
            var indexesItem = item.name.Split(' ')[1].ToCharArray();
            
            Field.Instance.UpdateGameField(indexesItem);
        }
    }
}
