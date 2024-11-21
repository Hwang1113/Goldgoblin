using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnSlotLeftClickDelegate(int _clickedInd);
    public delegate void OnSlotRightClickDelegate(int _clickedInd);

    public OnSlotLeftClickDelegate onSlotLeftClick = null;
    public OnSlotRightClickDelegate onSlotRightClick = null;

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

        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onSlotLeftClick?.Invoke(slotInd);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            onSlotRightClick?.Invoke(slotInd);
        }

    }
    public void SetItemSlot(string _imgPath, int _count)
    {
        Sprite mySprite = Resources.Load<Sprite>(_imgPath);

        if (mySprite == null) Debug.Log("Null!!!");
        else Debug.Log(mySprite.name);

        image_ItemIcon.sprite = Resources.Load<Sprite>(_imgPath);
        text_ItemCount.text = string.Format("X {0}", _count);
    }

    public void SetSlotDataActive(bool _active)
    {
        dataHolder.SetActive(_active);
    }
}
