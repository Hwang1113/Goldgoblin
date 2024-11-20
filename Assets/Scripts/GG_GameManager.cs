using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GG_GameManager : MonoBehaviour
{
    private bool sameId = false;

    [SerializeField]
    private LoginUIManager UImg = null;
    [SerializeField]
    private string id = string.Empty; //아이디 문자열
    [SerializeField]
    private string password = string.Empty; // 비밀번호 문자열
    [SerializeField]
    private string nickname = string.Empty; // 닉네임 문자열
    [SerializeField]
    private string birthday = string.Empty; // 생일 문자열
    [SerializeField]
    private string AR_Q = string.Empty; // 복구질문 문자열
    [SerializeField]
    private string AR_A = string.Empty; // 복구답변 문자열

    private const string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //아이디 비밀번호 받아서 로그인 코루틴 시작

        UImg.onClickSubmitBtn = Signin_Infos; // 버튼을 누르면 대리자가 Signin_Infos 를 실행하게 설정함 
    }
    
    private void Signin_Infos() //들어온 정보들을 가지고 코루틴(SigninCoroutine)을 시작
    {
        id = UImg.Id;
        Debug.Log("id:" + id);

        StartCoroutine(SigninCoroutine(id/*,password*/));
    }

    private IEnumerator SigninCoroutine(string _id/*, string _password*/) //회원가입 코루틴
    {
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함

        //form.AddField("Password", _password);
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
    private IEnumerator LoginCoroutine(string _id/*, string _password*/) //로그인 코루틴
    {
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함

        //form.AddField("Password", _password);
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
                 AR_A = www.downloadHandler.text;
            }
        }
    }
}
