using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GG_GameManager : MonoBehaviour
{
    [SerializeField]
    private LoginUIManager UImg = null;
    [SerializeField]
    private string id = string.Empty; //���̵� ���ڿ�
    [SerializeField]
    private string password = string.Empty; // ��й�ȣ ���ڿ�

    private const string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //���̵� ��й�ȣ �޾Ƽ� �α��� �ڷ�ƾ ����

        UImg.onClickSubmitBtn = SigninId; //
    }
    
    private void SigninId()
    {
        id = UImg.Id;
        Debug.Log("id:" + id);

        StartCoroutine(LoginCoroutine(id));
    }

    private IEnumerator LoginCoroutine(string _id)
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����

        form.AddField("SigninId", _id);

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
               // Debug.Log(www.downloadHandler.text);
            }
        }

    }
}
