using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GG_GameManager : MonoBehaviour
{
    [SerializeField]
    private LoginUIManager UImg = null;
    [SerializeField]
    private string id = string.Empty; //아이디 문자열
    [SerializeField]
    private string password = string.Empty; // 비밀번호 문자열

    private const string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //아이디 비밀번호 받아서 로그인 코루틴 시작

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
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함

        form.AddField("SigninId", _id);

        //웹서버는 비동기 방식
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form)) //post는 보안 //get은 속도
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
