using System.Collections.Generic;
using UnityEngine;
using static LoginUIManager;

public class GG_ItemManager : MonoBehaviour
{
    public class ItemData
    {
        //������ ���� ���� �ۺ� ����
        public int itemNum { get; set; }
        public string itemName { get; set; }
        public string itemInfo { get; set; }
        public string itemRarity { get; set; }
    }

    public class Inventoryslot
    {
        //�κ� ������ ���� �ۺ� ����
        public string ItemNum { get; set; }
        public string EA { get; set; }
    }

    [SerializeField]
    public string Id = string.Empty;

    public void DestroyItem1()
    {

    }
    public void DestroyItem10()
    {

    }
    public void ShowItem()
    {

    }
}
