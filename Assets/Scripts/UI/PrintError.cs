using UnityEngine;
using TMPro;

public class PrintError : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text_Error = null;

    public void PrintErrorMessage(string _error)
    {
<<<<<<< HEAD
        gameObject.SetActive(true);

=======
>>>>>>> ad3785e1f9b06cdaf3ed6103915fada86d41ada0
        text_Error.text = _error;
    }
}
