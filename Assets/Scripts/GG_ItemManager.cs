using UnityEngine;
using static LoginUIManager;

public class GG_ItemManager : MonoBehaviour
{

    public delegate void destroyItem1Delegate();


    public destroyItem1Delegate itemDestroy1 = null;
    //������ ���� ���� �ۺ� ����
    public string invenId { get; set; }
    public int itemNum { get; set; }
    public string itemName { get; set; }
    public string itemInfo { get; set; }
    public string itemRarity { get; set; }
    //public string itemIconPath { get; set; }

    [SerializeField]
    public string Id = string.Empty;

    public void DestroyItem1()
    {

    }
    public void DestroyItem10()
    {

    }

}
