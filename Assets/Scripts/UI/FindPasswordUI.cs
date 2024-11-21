using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class FindPasswordUI : MonoBehaviour
{
    public delegate void OnClickBackToLoginDel();
    public delegate void OnClickSubmitDel();

    public OnClickBackToLoginDel onClickBackToLogin = null;
    public OnClickSubmitDel onClickSubmit = null;

    [SerializeField]
    private TMP_InputField idField = null;
    [SerializeField]
    private TMP_InputField recoveryAnswerField = null;
    [SerializeField]
    private TMP_Dropdown recoveryAnswerDropdown = null;
    [SerializeField]
    private TextMeshProUGUI text_Password = null;
    [SerializeField]
    private GameObject loginMenu = null;

    private string userId = string.Empty;
    private int recoveryAnswerInd = 0;
    private string recoveryAnswer = string.Empty;

    private PrintError printErr = null;

    public string UserId
    {
        get { return userId; }
    }

    public int RecoveryAnswerInd
    {
        get { return recoveryAnswerInd; }
    }

    public string RecoveryAnswer
    {
        get { return recoveryAnswer; }
    }

    public PrintError PrintErr
    {
        get { return printErr; }
    }

    public GameObject LoginMenu
    {
        get { return loginMenu; }
    }

    private void Awake()
    {
        printErr = GetComponent<PrintError>();
    }

    private void OnDisable()
    {
        Init();
    }
    public void OnClickBackToLogin()
    {
        onClickBackToLogin?.Invoke();
    }

    public void OnClickSubmit()
    {
        onClickSubmit?.Invoke();
    }

    public void OnIdFieldChanged(string _id)
    {
        userId = _id;
    }

    public void OnRecoveryDrowdownChanged(int _val)
    {
        recoveryAnswerInd = _val;
    }

    public void OnRecoveryAnswerChanged(string _answer)
    {
        recoveryAnswer = _answer;
    }

    public void ShowPassword(string _password)
    {
        text_Password.text = string.Format("Password : {0}", _password);
    }

    private void Init()
    {
        userId = string.Empty;
        recoveryAnswerInd = 0;
        recoveryAnswer = string.Empty;

        idField.text = string.Empty;
        recoveryAnswerDropdown.value = 0;
        recoveryAnswerField.text = string.Empty;
        text_Password.text = string.Empty;
    }


}
