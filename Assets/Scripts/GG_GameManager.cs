using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GG_GameManager : MonoBehaviour
{
    [SerializeField]
    private string username = string.Empty; //아이디 문자열
    [SerializeField]
    private string password = string.Empty; // 비밀번호 문자열

    private const string loginUri = "https://127.0.0.1/login.php";

    private void Start()
    {
        StartCoroutine(LoginCoroutine(username, password)); //아이디 비밀번호 받아서 로그인 코루틴 시작
    }
    private IEnumerator LoginCoroutine(string _username, string _password)
    {
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("loginUser", _username); //key값 랑 value값
        form.AddField("loginPass", _password);

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
                Debug.Log(www.downloadHandler.text);
            }
        }

    }
}
