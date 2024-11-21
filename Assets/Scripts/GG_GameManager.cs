using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions; //정규표현식
using Newtonsoft.Json;
using static GG_ItemManager;
using NUnit.Framework;


public class GG_GameManager : MonoBehaviour
{
    private bool isDifferentId = false; //아이디 중복인지 아닌지를 저장하는 bool 값
    private bool isAllInfoChecked = false; //모든 항목이 true인지 아닌지 저장하는 bool 값
    //[SerializeField]
    //private GG_ItemManager Itmg = null;
    [SerializeField]
    private LoginUIManager UImg = null;    
    [SerializeField]
    private InventoryUIManager InvenUImg = null;
    [SerializeField]
    private string id = string.Empty; //아이디 문자열
    [SerializeField]
    private string password = string.Empty; // 비밀번호 문자열
    [SerializeField]
    private string nickname = string.Empty; // 닉네임 문자열
    [SerializeField]
    private int birthday = 0; // 주민번호앞 문자열 // 990909
    [SerializeField]
    private int AR_Q = 0; // 복구질문번호 정수형
    [SerializeField]
    private string AR_A = string.Empty; // 복구답변 문자열

    //player item db 받아온걸 저장할 데이터형들

    private const string loginUri = "http://127.0.0.1/login.php";
    private const string signupUri = "http://127.0.0.1/signup.php";
    private const string sameidUri = "http://127.0.0.1/sameid.php";
    private const string getinvenUri = "http://127.0.0.1/getinven.php";


    private void Start()
    {
        UImg.onClickSubmitBtn = SignUpInfos; // SignUp버튼을 UI 에서 누르면 대리자가 SignUp_Infos 를 실행하게 설정함 
        UImg.onClickIDCheckBtn = SameIdCheck;  //중복방지 버튼을  UI에서 누르면 대리자가 중복방지를 실행함
        UImg.onSelectSignupIDInputfiled = IdCheckFalse; // ID Inputfield 를 클릭하면  isDifferentId = false; 
        UImg.onClickSignUpBtn = GoSignUp; // 로그인 창에서 SignUp 버튼을 누르면 GoSignUp(); 
        UImg.onClickLoginBtn = Login; //로그인 버튼을 누르면 Login() 실행
        UImg.onClickBackToLoginBtn = BacktoLogin; //backtologin 버튼을 누르면 
        InvenUImg.OnClickLogoutBtn = Logout; // 로그아웃
    } // 델리게이트를 통해 버튼 상호작용 구현
    private void Login()
    {
        StartCoroutine(SignInCoroutine());
    }
    private void Logout() // id, password 초기화, inventory UI 비활성화, Login UI 활성화
    {
        id = string.Empty;
        password = string.Empty;
        InvenUImg.gameObject.SetActive(false);// inventory UI 비활성화
        UImg.SetLoginMenuActivation(true);
    }
    private void AllInfoCheck()
    {
        //id = UImg.Id;//UI에 적힌 Id를 id에 복사
        //password = UImg.Password; //UI에 적힌 password 복사
        //nickname = UImg.Nickname;
        //birthday = UImg.BirthDate;
        //AR_Q = UImg.RecoveryInd;
        AR_A = UImg.RecoveryAnswer;
        if (isDifferentId && UImg.IsIdFormatCorrect() && UImg.IsPwFormatCorrect() &&
            UImg.IsPwCheckCorrect() && UImg.IsBirthDateFormatCorrect() && AR_A != string.Empty)  
            //괄호안에 조건 모두 넣기 차례대로 아이디중복, 아이디 포맷,비밀번호 포맷,비밀번호확인, 생년월일 , 질문 답변을 적었으면
        {
            isAllInfoChecked = true;
        }
        else
        {
            isAllInfoChecked = false;
        }
    }// 회원가입 전 모든 항목 확인 
    private void SignUpInfos() //들어온 정보들을 가지고 회원가입 코루틴(SignUpCoroutine)을 시작 
    {
        AllInfoCheck(); //모든 항목체크 시작 isAllInfoChecked를 true 혹은 false로 반환

        if (!isAllInfoChecked)
        {
            Debug.Log("확인되지 않는 항목이 있습니다");

            return;
        }
        StartCoroutine(SignUpCoroutine());

    }
    private void SameIdCheck()
    {
        StartCoroutine(SameIdCheckCoroutine());            // 코루틴 안에서 중복된지 확인해서 코루틴에서 isDifferentId를 false나 true로 바꿔줌
    }// 아이디 중복방지 코루틴을 돌린다.
    private void IdCheckFalse() //isDifferentId = false; 로 만듬
    {
        isDifferentId = false;
        Debug.Log("아이디 입력 후 중복체크를 누르시오");
    }
    private void GoSignUp() //로그인 창을 끈다, 회원가입창을 킨다.
    {
        UImg.SetLoginMenuActivation(false);
        UImg.SetSignupMenuActivation(true);
    }
    private void BacktoLogin() // 회원가입창을 끈다, 로그인창을 킨다.
    {
        UImg.SetSignupMenuActivation(false);
        UImg.SetLoginMenuActivation(true);
    }
    //////////////////////////////////////////////////
    //밑으로는 DB데이터 전달 관련 코루틴만
    //////////////////////////////////////////////////
    private IEnumerator SignInCoroutine() //가져온 정보들을 서버에 전달 , 로그인 코루틴 
    {
        id = UImg.Id;
        password = UImg.Password;
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("Id", id);
        form.AddField("Password", password);
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
                string loginResult = www.downloadHandler.text;
                Debug.Log(loginResult);
                if (loginResult == "0")//로그인시도, 정보대조후 알맞는게 없다
                {
                    UImg.PrintLoginError(); //에러로그를 띄운다.
                }
                if (loginResult == "1")//로그인시도, 정보대조후 알맞는게 있다
                {
                    Debug.Log("로그인 가능"); //InventoryUri 만들어서 호출해야할듯
                    StartCoroutine(GetInventory());

                    //inventory UI 켜기 , 
                }

            }
        }
    }
    private IEnumerator GetInventory() //inventory slot 정보 가져옴
    {
        Debug.Log("접속할 ID :" + id);
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("Id", id);
        using (UnityWebRequest www = UnityWebRequest.Post(/*inventory*/getinvenUri, form)) //!!!!!!!!!!Uri 바꾸기!!!!!!!!!!!!!!!
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string InventoryItems = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
                List<Inventoryslot> invenslot = JsonConvert.DeserializeObject<List<Inventoryslot>>(InventoryItems);
                invenslot.Sort((Inventoryslot a, Inventoryslot b) => { return a.CompareTo(b); }); // 정렬 알고리즘(invenslot(list))
                foreach (Inventoryslot InventoryItem in invenslot) 
                    //invenslot(list)안에 있는 Inventoryslot클래스 형식의 정보 만큼 아래코드를 실행함
                {
                    Debug.Log(InventoryItem.ItemNum + " : " + InventoryItem.EA);
                    //디버그 로그가 잘 확인되면 invenslot에 정보가 잘 담긴 것
                    UImg.SetLoginMenuActivation(false);//로그인 UI 끄고
                    InvenUImg.gameObject.SetActive(true);//인벤토리 UI 켜기
                }
                // List<Inventoryslot> newList = invenslot.OrderBy(p => p.ItemNum).ToList();

            }
        }

    }
    private IEnumerator SignUpCoroutine() //가져온 정보들을 서버에 전달, 회원가입 코루틴 
    {
        id = UImg.Id;//UI에 적힌 Id를 id에 복사
        password = UImg.Password; //UI에 적힌 password 복사
        nickname = UImg.Nickname;
        birthday = UImg.BirthDate;
        AR_Q = UImg.RecoveryInd;
        AR_A = UImg.RecoveryAnswer;


        Debug.Log("id:" + id);
        Debug.Log("password:" + password);
        Debug.Log("Nickname" + nickname);
        Debug.Log("Birthday"+ birthday);
        Debug.Log("AR_Q"+ AR_Q);
        Debug.Log("AR_A"+ AR_A);
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함

        form.AddField("Id", id);
        form.AddField("Password", password);
        form.AddField("Nickname", nickname);
        form.AddField("Birthday", birthday);
        form.AddField("AR_Q", AR_Q);
        form.AddField("AR_A", AR_A);
        //웹서버는 비동기 방식
        using (UnityWebRequest www = UnityWebRequest.Post(signupUri, form)) //post는 보안 //get은 속도
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

                string data = www.downloadHandler.text;
                if (data == "0")    //아이디 생성 실패
                {
                    Debug.Log("계정 생성 실패");
                }
                else if (data == "1") // 계정 생성 성공
                {
                    Debug.Log("계정 생성 성공");
                    //MyInventory =JsonConvert.DeserializeObject<List<InventoryitemDB>>(data);
                    //json 형식으로 작성된 "문자열" 아이템 정보를 받아와서 역직렬화를 통해 게임매니저가 아이템DB 받아옴, 작성해야함
                }
            }
        }
    }
    private IEnumerator SameIdCheckCoroutine() //아이디 중복방지 코루틴
    {
        id = UImg.Id;
        WWWForm form = new WWWForm(); //서버 전달 형태를 정함
        form.AddField("Id", id); //서버에 id를 넘겨줌
        using (UnityWebRequest www = UnityWebRequest.Post(sameidUri, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //string CheckId = www.downloadHandler.text.Replace("\uFEFF", "");
                string CheckId = www.downloadHandler.text; //오염된 데이터
                //string CheckId = "This  is    a   test&nbsp;string  with  unnecessary   spaces."; 
                string pattern = @"(\s|\u00A0)+"; // 정규표현식 패턴 지정 
                CheckId = Regex.Replace(CheckId, pattern,"").Trim();//정화
                Debug.Log(CheckId);
                //foreach(int i in CheckId )// 쓰레기 있는지 확인
                //     Debug.Log(i);
                //서버에서 다른지 같은지 문자열로 알려줌
                if (CheckId == "0") //같은게 있으면 "0"로 답변 해주기로함
                {
                    isDifferentId = false;
                    Debug.Log("같음");
                }
                else if (CheckId == "1") //같은게 없으면 "1"로 답변 해주기로함
                {
                    isDifferentId = true;
                    Debug.Log("다름");
                }
                else
                {
                    Debug.Log("서버가 이상해요");
                }


                if (isDifferentId == true)
                {
                    Debug.Log("아이디 생성 가능(중복된 게 없음)");
                }
                else if (isDifferentId == false)
                {
                    Debug.Log("아이디 생성 불가능(중복)");
                }
            }
        }
    }
}
