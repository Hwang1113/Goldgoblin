using UnityEngine;
using TMPro;

public class PrintError : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_Error = null;

    public void PrintErrorMessage(string _error)
    {
        gameObject.SetActive(true);

        text_Error.text = _error;
    }
}
