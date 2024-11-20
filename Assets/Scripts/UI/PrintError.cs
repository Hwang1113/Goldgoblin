using UnityEngine;
using TMPro;

public class PrintError : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_Error = null;

    public void PrintErrorMessage(string _error)
    {
        text_Error.text = _error;
    }
}
