using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnSlotClickDelegate();

    public OnSlotClickDelegate onSlotClick = null;

    [SerializeField]
    private Image image_ItemIcon = null;
    [SerializeField]
    private TextMeshProUGUI text_ItemCount = null;

    [SerializeField]
    private GameObject dataHolder = null;

    public int slotInd = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogFormat("Click Slot : {0}", gameObject.name);
    }
    public void SetItemSlot(string _imgPath, int _count)
    {
        image_ItemIcon.sprite = Resources.Load<Sprite>(_imgPath);
        text_ItemCount.text = string.Format("X {0}", _count);
    }

    public void SetSlotActivation(bool _activation)
    {
        gameObject.SetActive(_activation);
    }
}
