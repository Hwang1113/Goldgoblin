using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FindPasswordManager : MonoBehaviour
{
    private FindPasswordUI findPasswordUI = null;

    private const string searchPasswordUri = "http://127.0.0.1/findpassword.php";

    private void Awake()
    {
        findPasswordUI = GetComponent<FindPasswordUI>();
    }

    private void Start()
    {
        findPasswordUI.onClickSubmit = Submit;
        findPasswordUI.onClickBackToLogin = BackToLogin;
    }

    private void Submit()
    {
        StartCoroutine(SubmitCoroutine());
    }

    private void BackToLogin()
    {
        findPasswordUI.LoginMenu.SetActive(true);

        gameObject.SetActive(false);
    }

    private IEnumerator SubmitCoroutine()
    {
        string id = findPasswordUI.UserId;
        int recoveryInd = findPasswordUI.RecoveryAnswerInd;
        string recoveryAnswer = findPasswordUI.RecoveryAnswer;

        WWWForm form = new WWWForm();
        form.AddField("Id", id);
        form.AddField("AR_Q", recoveryInd);
        form.AddField("AR_A", recoveryAnswer);

        using (UnityWebRequest www = UnityWebRequest.Post(searchPasswordUri, form)) //post�� ���� //get�� �ӵ�
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string data = www.downloadHandler.text;

                if (data == "0")
                {
                    findPasswordUI.PrintErr.PrintErrorMessage("��ġ�ϴ� ���̵�, ������ �����ϴ�.");
                }
                else
                {
                    findPasswordUI.PrintErr.HideErrorMessage();
                    findPasswordUI.ShowPassword(data);
                }
            }
        }
    }
}
