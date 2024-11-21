using System.Collections.Generic;
using UnityEngine;
using static LoginUIManager;

public class GG_ItemManager : MonoBehaviour
{
    public class ItemData
    {
        //아이템 정보 담을 퍼블릭 변수
        public int itemNum { get; set; }
        public string itemName { get; set; }
        public string itemInfo { get; set; }
        public string itemRarity { get; set; }
    }

    public class Inventoryslot
    {
        //인벤 정보를 담을 퍼블릭 변수
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
