using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GG_GameManager : MonoBehaviour
{
    private bool isDifferentId = false; //아이디 중복인지 아닌지를 저장하는 bool 값
    private bool isAllInfoChecked = false; //모든 항목이 true인지 아닌지 저장하는 bool 값

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



    //player item db 받아온걸 저장할 데이터형들
    private string owner = string.Empty; //아이템 주인
    private string[] item = null; //아이템(이름) 배열
    private string[] ea = null; // 아이템 갯수



    private const string loginUri = "http://127.0.0.1/login.php";
    private const string SameIdUri = "http://127.0.0.1/SameId.php";


    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //아이디 비밀번호 받아서 로그인 코루틴 시작

        UImg.onClickSubmitBtn = SignUpnfos; // SignUp버튼을 UI 에서 누르면 대리자가 SignUp_Infos 를 실행하게 설정함 
        UImg.onClickIDCheckBtn = SameIdCheck;  //중복방지 버튼을  UI에서 누르면 대리자가 중복방지를 실행함
        UImg.onSelectSignupIDInputfiled = IdCheckFalse; // ID Inputfield 를 클릭하면  isDifferentId = false; 
        UImg.onClickSignUpBtn = GoSignUp; // 로그인 창에서 SignUp 버튼을 누르면 GoSignUp(); 
        //UImg.onClickLoginBtn = //로그인 버튼을 누르면
    }
    private void AllInfoCheck()
    {
        if (isDifferentId/*모든 항목체크 여기서 함*/)  //괄호안에 조건 모두 넣기
        {
            isAllInfoChecked = true;
        }
        else 
        {
            isAllInfoChecked = false;
        }
    }
    private void SignUpnfos() //들어온 정보들을 가지고 회원가입 코루틴(SignUpCoroutine)을 시작 
    {
        AllInfoCheck(); //모든 항목체크 시작 isAllInfoChecked를 true 혹은 false로 반환

        if (!isAllInfoChecked) 
        {
            Debug.Log("확인되지 않는 항목이 있습니다");
            return;
        }

        id = UImg.Id;//UI에 적힌 Id를 id에 복사
        Debug.Log("id:" + id);

        if (isDifferentId == true)
        {
            StartCoroutine(SignUpCoroutine(id/*,password*/));
        }
        else if (isDifferentId == false)
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
            else if(isDifferentId == false)
                Debug.Log("아이디 생성 불가능(중복)");
        }
    }
    private void GoSignUp() //로그인 창을 끈다, 회원가입창을 킨다.
    {
        UImg.SetLoginMenuActivation(false);
        UImg.SetSignupMenuActivation(true);
    }
    private void IdCheckFalse() //isDifferentId = false; 로 만듬
    {
        isDifferentId = false;
        Debug.Log("아이디 입력 후 중복체크를 누르시오");
    }


    //////////////////////////////////////////////////
    //밑으로는 DB데이터 전달 관련 코루틴만
    //////////////////////////////////////////////////
    private IEnumerator SignUpCoroutine(string _id/*, string _password*/) //가져온 정보들을 서버에 전달 , 회원가입 코루틴 
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
    private IEnumerator SignInCoroutine(string _id/*, string _password*/) //가져온 정보들을 서버에 전달, 로그인 코루틴 
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
                //로그인 성공 했으니까 서버에서 뭘 대답하냐
                string data = www.downloadHandler.text; //일단 json 형식으로 작성된 "문자열" 아이템 정보를 받아와서
                // 역직렬화를 통해 게임매니저가 아이템DB 받아옴, 작성해야함
                //
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
                else if(CheckId == "true") //같은게 없으면 true로 답변 해주기로함
                {
                    isDifferentId = true;
                }
                else 
                {
                    Debug.Log("서버가 이상해요");
                }
            }
        }
    }
}
