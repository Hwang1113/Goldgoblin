using UnityEngine;
using TMPro;

public class PrintError : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_Error = null;

    public void PrintErrorMessage(string _error)
    {
        if(!text_Error.gameObject.activeInHierarchy)
            text_Error.gameObject.SetActive(true);

        text_Error.text = _error;
    }

    public void HideErrorMessage()
    {
        if (text_Error.gameObject.activeInHierarchy)
            text_Error.gameObject.SetActive(false);
    }
}
