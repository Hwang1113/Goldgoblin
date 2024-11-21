using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]
    private ItemBagUI itemBag = null;

    [SerializeField]
    private ItemDataUI itemData = null;

    public void UpdateItemSlot(int _slotInd, string _iconPath)
    {
        // itemBag.UpdateItemSlot(_slotInd, _iconPath);
    }


}
