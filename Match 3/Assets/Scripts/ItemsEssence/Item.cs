using UnityEngine;

namespace ItemsEssence
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemTypes _itemTypes;

        public ItemTypes ItemType => _itemTypes;
        public int MaxCountItem => 8;
    }
}
