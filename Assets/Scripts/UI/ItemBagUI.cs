using UnityEngine;

public class ItemBagUI : MonoBehaviour
{
    private ItemSlotUI[] itemSlots = null;

    private void Awake()
    {
        itemSlots = GetComponentsInChildren<ItemSlotUI>();
    }

    private void Start()
    {
        for(int i = 0; i < itemSlots.Length; ++i)
        {
            itemSlots[i].slotInd = i;
        }
    }

    public void UpdateItemSlot(int _slotInd, string _iconPath, int _count)
    {
        if (_slotInd < 0 || _slotInd >= itemSlots.Length)
        {
            Debug.LogWarningFormat("Item Slot Index {0} is not valid!", _slotInd);
            return;
        }

        itemSlots[_slotInd].SetItemSlot(_iconPath, _count);
    }
}
