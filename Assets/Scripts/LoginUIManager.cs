using Unity.VisualScripting;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public delegate void OnClickSubmitBtnDelegate();

    public OnClickSubmitBtnDelegate onClickSubmitBtn;
    public string Id { get{ return id; } }

    [SerializeField]
    private string id = null;

    public void InputId(string _id)
    {
        id = _id;
    }

    public void OnClickSubmit()
    {
        onClickSubmitBtn?.Invoke();
    }
}
