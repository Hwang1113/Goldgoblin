using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GG_GameManager : MonoBehaviour
{
    [SerializeField]
    private string username = string.Empty;
    [SerializeField]
    private string password = string.Empty;

    private const string loginUri = "https://127.0.0.1/login.php";

    private void Start()
    {
        StartCoroutine(LoginCoroutine(username, password));
    }
    private IEnumerator LoginCoroutine(string _username, string _password)
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����
        form.AddField("loginUser", _username); //key�� �� value��
        form.AddField("loginPass", _password);

        //�������� �񵿱� ���
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form)) //post�� ���� //get�� �ӵ�
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

    }
}
