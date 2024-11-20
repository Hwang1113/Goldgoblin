using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GG_GameManager : MonoBehaviour
{
    private bool isDifferentId = false;

    [SerializeField]
    private LoginUIManager UImg = null;
    [SerializeField]
    private string id = string.Empty; //아이디 문자열
    [SerializeField]
    private string password = string.Empty; // 비밀번호 문자열
    [SerializeField]
    private string nickname = string.Empty; // 닉네임 문자열
    [SerializeField]
    private string birthday = string.Empty; // 주민번호앞 문자열 // 990909
    [SerializeField]
    private int AR_Q = 0; // 복구질문번호 정수형
    [SerializeField]
    private string AR_A = string.Empty; // 복구답변 문자열

    private const string loginUri = "http://127.0.0.1/login.php";
    private const string SameIdUri = "http://127.0.0.1/SameId.php";


    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //아이디 비밀번호 받아서 로그인 코루틴 시작

        UImg.onClickSubmitBtn = Signin_Infos; // Signin버튼을 UI 에서 누르면 대리자가 Signin_Infos 를 실행하게 설정함 
        UImg.onClickIDCheckBtn = SameIdCheck;  //중복방지 버튼을  UI에서 누르면 대리자가 중복방지를 실행함
        UImg.
    }

    private void Signin_Infos() //들어온 정보들을 가지고 코루틴(SigninCoroutine)을 시작 
    {
        id = UImg.Id;
        Debug.Log("id:" + id);
        if (isDifferentId == true)
        {
            StartCoroutine(SigninCoroutine(id/*,password*/));
        }
        else
        {
            Debug.Log("Id 중복확인 후 회원가입 ");
        }
    }
    private void SameIdCheck()
    {
        if (isDifferentId == true)
            //나중에 아이디에 적힌값이 바꼈을때 다시 
            //onValuechanged 등 통해 isDifferentId를 false로 바꿀것 안그러면 중복방지체크후 아이디를 바꾼 후 회원가입할 수 있음 11.20
        {
            Debug.Log("아이디 생성 가능(중복된 게 없음)");
            return;
        }
        if (isDifferentId == false)
        {
            id = UImg.Id; 
            StartCoroutine(SameIdCheckCoroutine(id));// 코루틴 안에서 중복된지 확인해서 코루틴에서 isDifferentId를 false나 true로 바꿔줌
            if (isDifferentId == true)
                Debug.Log("아이디 생성 가능(중복된 게 없음)");
            else
                Debug.Log("아이디 생성 불가능(중복)");
        }
    }


    private IEnumerator SigninCoroutine(string _id/*, string _password*/) //가져온 정보들을 서버에 전달 추가, 회원가입 코루틴 
    {
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("Id", _id);
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
                //Debug.Log(www.downloadHandler.text);
            }
        }
    }

    private IEnumerator SameIdCheckCoroutine(string _id) //아이디 중복방지 코루틴
    {
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("Id", _id); //서버에 id를 넘겨줌
        using (UnityWebRequest www = UnityWebRequest.Post(SameIdUri, form)) 
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
                string CheckId = www.downloadHandler.text; //서버에서 다른지 같은지 문자열로 알려줌
                if (CheckId == "false") //같은게 있으면 false로 답변 해주기로함
                {
                    isDifferentId = false;
                }
                else
                {
                    isDifferentId = true;
                }
            }
        }
    }
}
