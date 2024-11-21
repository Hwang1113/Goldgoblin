using System;
using System.Collections.Generic;
using UnityEngine;
using static LoginUIManager;

public class GG_ItemManager : MonoBehaviour
{
    public class ItemData //������ ������ ���� �ʿ��� ������ ���� Ŭ����
    {
        //������ ���� ���� �ۺ� ����
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public string Rarity { get; set; }
        public string Icon { get; set; }
    }

    public class Inventoryslot : IComparable<Inventoryslot> //�κ����� ������ ���� �ʿ��� ������ ���� Ŭ����
    {
        //�κ� ������ ���� �ۺ� ����
        public string ItemNum { get; set; }
        public string EA { get; set; }

        public int CompareTo(Inventoryslot other)
        {
            return int.Parse(ItemNum) - int.Parse(other.ItemNum);
        }
    }
}
