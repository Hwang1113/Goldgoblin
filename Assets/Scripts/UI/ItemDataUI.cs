using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataUI : MonoBehaviour
{
    [SerializeField]
    private Image image_Icon = null;
    [SerializeField]
    private TextMeshProUGUI text_Name = null;
    [SerializeField]
    private TextMeshProUGUI text_Rarity = null;
    [SerializeField]
    private TextMeshProUGUI text_Desc = null;

    public void SetItemData(string _imgPath, string _name, string _Rarity, string _desc)
    {
        image_Icon.sprite = Resources.Load<Sprite>(_imgPath);
        text_Name.text = _name;
        text_Rarity.text = _Rarity;
        text_Desc.text = _desc;
    }

    public void SetItemDataActivation(bool _activation)
    {
        gameObject.SetActive(_activation);
    }
}
