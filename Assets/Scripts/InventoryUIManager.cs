using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public delegate void OnClickLogoutBtnDelegate();

    /// <summary>
    /// �α׾ƿ� ��ư Ŭ�� �� ȣ��Ǵ� ��������Ʈ
    /// </summary>
    public OnClickLogoutBtnDelegate OnClickLogoutBtn = null;

    [SerializeField]
    private ItemBagUI itemBag = null;

    [SerializeField]
    private ItemDataUI itemData = null;

    /// <summary>
    /// Ư�� ���� �� �� Ŭ�� �� ȣ��Ǵ� ��������Ʈ�� ������.
    /// </summary>
    /// <param name="_slotInd">� �������� �Ǵ��ϴ� �ε��� ��ȣ</param>
    /// <returns></returns>
    public ItemSlotUI.OnSlotLeftClickDelegate GetSlotLeftClickDelegate(int _slotInd)
    {
        return itemBag.ItemSlots[_slotInd].onSlotLeftClick;
    }

    /// <summary>
    /// Ư�� ���� ���� �� Ŭ�� �� ȣ��Ǵ� ��������Ʈ�� ������.
    /// </summary>
    /// <param name="_slotInd">� �������� �Ǵ��ϴ� �ε��� ��ȣ</param>
    /// <returns></returns>
    public ItemSlotUI.OnSlotRightClickDelegate GetSlotRightClickDelegate(int _slotInd)
    {
        return itemBag.ItemSlots[_slotInd].onSlotRightClick;
    }

    /// <summary>
    /// �κ��丮 ������ UI�� ������Ʈ�Ѵ�.
    /// </summary>
    /// <param name="_slotInd">������ �ε��� ��ȣ</param>
    /// <param name="_iconPath">������ �̹��� ���</param>
    /// <param name="_ea">����</param>
    public void SetItemSlotUI(int _slotInd, string _iconPath, int _ea)
    {
        itemBag.ItemSlots[_slotInd].SetItemSlot(_iconPath, _ea);
    }
    /// <summary>
    /// ������ ���� ���� UI�� Ȱ��ȭ ���θ� �����Ѵ�
    /// </summary>
    /// <param name="_slotInd">������ �ε���</param>
    /// <param name="_active">Ȱ��ȭ ����</param>
    public void SetItemSlotActive(int _slotInd, bool _active)
    {
        itemBag.ItemSlots[_slotInd].SetSlotDataActive(_active);
    }

    /// <summary>
    /// ������ Ŭ�� �� ���̴� ItemData UI�� ������ ������Ʈ�Ѵ�.
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
    /// ������ ������ UI�� Ȱ��ȭ ���θ� �����Ѵ�
    /// </summary>
    /// <param name="_active">Ȱ��ȭ ����</param>
    public void SetItemDataUIActive(bool _active)
    {
        itemData.SetItemDataActive(_active);
    }

    public void OnClickLogoutButton()
    {
        OnClickLogoutBtn?.Invoke();
    }
}
