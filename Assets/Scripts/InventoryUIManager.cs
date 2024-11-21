using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public delegate void OnClickLogoutBtnDelegate();

    /// <summary>
    /// 로그아웃 버튼 클릭 시 호출되는 델리게이트
    /// </summary>
    public OnClickLogoutBtnDelegate OnClickLogoutBtn = null;

    [SerializeField]
    private ItemBagUI itemBag = null;

    [SerializeField]
    private ItemDataUI itemData = null;

    /// <summary>
    /// 특정 슬롯 왼 쪽 클릭 시 호출되는 델리게이트를 리턴함.
    /// </summary>
    /// <param name="_slotInd">어떤 슬롯인지 판단하는 인덱스 번호</param>
    /// <returns></returns>
    public ItemSlotUI.OnSlotLeftClickDelegate GetSlotLeftClickDelegate(int _slotInd)
    {
        return itemBag.ItemSlots[_slotInd].onSlotLeftClick;
    }

    /// <summary>
    /// 특정 슬롯 오른 쪽 클릭 시 호출되는 델리게이트를 리턴함.
    /// </summary>
    /// <param name="_slotInd">어떤 슬롯인지 판단하는 인덱스 번호</param>
    /// <returns></returns>
    public ItemSlotUI.OnSlotRightClickDelegate GetSlotRightClickDelegate(int _slotInd)
    {
        return itemBag.ItemSlots[_slotInd].onSlotRightClick;
    }

    /// <summary>
    /// 인벤토리 슬롯의 UI를 업데이트한다.
    /// </summary>
    /// <param name="_slotInd">슬롯의 인덱스 번호</param>
    /// <param name="_iconPath">아이콘 이미지 경로</param>
    /// <param name="_ea">개수</param>
    public void SetItemSlotUI(int _slotInd, string _iconPath, int _ea)
    {
        itemBag.ItemSlots[_slotInd].SetItemSlot(_iconPath, _ea);
    }
    /// <summary>
    /// 아이템 슬롯 정보 UI의 활성화 여부를 설정한다
    /// </summary>
    /// <param name="_slotInd">슬롯의 인덱스</param>
    /// <param name="_active">활성화 여부</param>
    public void SetItemSlotActive(int _slotInd, bool _active)
    {
        itemBag.ItemSlots[_slotInd].SetSlotDataActive(_active);
    }

    /// <summary>
    /// 아이템 클릭 시 보이는 ItemData UI의 정보를 업데이트한다.
    /// </summary>
    /// <param name="_itemName"></param>
    /// <param name="_iconPath"></param>
    /// <param name="_itemRarity"></param>
    /// <param name="_itemDesc"></param>
    public void SetItemDataUI(string _itemName, string _iconPath, string _itemRarity, string _itemDesc)
    {
        itemData.SetItemData(_itemName, _iconPath, _itemRarity, _itemDesc);
    }

    /// <summary>
    /// 아이템 데이터 UI의 활성화 여부를 설정한다
    /// </summary>
    /// <param name="_active">활성화 여부</param>
    public void SetItemDataUIActive(bool _active)
    {
        itemData.SetItemDataActive(_active);
    }

    public void OnClickLogoutButton()
    {
        OnClickLogoutBtn?.Invoke();
    }
}
