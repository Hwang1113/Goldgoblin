using TMPro;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    #region 델리게이트 정의
    public delegate void OnClickSubmitBtnDelegate();
    public delegate void OnClickLoginBtnDelegate();
    public delegate void OnClickSignUpBtnDelegate();
    public delegate void OnClickIDCheckBtnDelegate();
    #endregion

    #region 델리게이트 변수

    /// <summary>
    /// 회원가입 창에서 Submit 버튼을 눌렀을 시 호출되는 델리게이트.
    /// </summary>
    public OnClickSubmitBtnDelegate onClickSubmitBtn = null;

    /// <summary>
    /// 로그인 창에서 회원가입 버튼을 눌렀을 시 호출되는 델리게이트.
    /// </summary>
    public OnClickSignUpBtnDelegate onClickSignUpBtn = null;

    /// <summary>
    /// 로그인 창에서 로그인 버튼을 눌렀을 시 호출되는 델리게이트.
    /// </summary>
    public OnClickLoginBtnDelegate onClickLoginBtn = null;

    /// <summary>
    /// 회원가입 창에서 아이디 확인 버튼을 눌렀을 시 호출되는 델리게이트.
    /// </summary>
    public OnClickIDCheckBtnDelegate onClickIDCheckBtn = null;

    #endregion

    #region 열거형
    /// <summary>
    /// 회원가입 시 어떤 문제가 생겼는지 판단하기 위한 열거형.
    /// </summary>
    public enum ESignupErrType
    {
        // 형태 오류
        FormatError,
        // 일치 오류
        SameError
    }
    #endregion

    #region 프로퍼티들
    public string Id { get{ return id; } }
    public string Password { get{ return password; } }

    public string Nickname { get{ return nickname; } }

    public string RecoveryAnswer { get{ return recoveryAnswer; } }
    public int RecoveryInd { get{ return recoveryInd; } }
    #endregion

    #region UI 오브젝트 참조 변수
    [SerializeField]
    private PrintError login_Error = null;
    [SerializeField]
    private PrintError signup_idError = null;
    [SerializeField]
    private PrintError signup_pwError = null;
    #endregion

    #region 변수
    [SerializeField]
    private string id = null;
    private string password = null;
    private string checkPassword = null;
    private string nickname = null;
    private string recoveryAnswer = null;
    private int recoveryInd = 0;
    #endregion

    #region UI와 바인딩되는 함수
    public void InputId(string _id)
    {
        id = _id;
    }

    public void InputPassword(string _pw)
    {
        password = _pw;
    }

    public void InputRecoveryAnswer(string _recoveryAnswer)
    {
        recoveryAnswer = _recoveryAnswer;
    }

    public void OnRecoveryDropdownChanged(int _ind)
    {
        recoveryInd = _ind;
    }

    public void OnClickSubmit()
    {
        onClickSubmitBtn?.Invoke();
    }

    public void OnClickLogin()
    {
        onClickLoginBtn?.Invoke();
    }

    public void OnClickSignUp()
    {
        onClickSignUpBtn?.Invoke();
    }

    public void OnClickIdCheck()
    {
        onClickIDCheckBtn?.Invoke();
    }
    #endregion

    #region 퍼블릭 함수
    /// <summary>
    /// 로그인 때 아이디나 비밀번호를 틀렸을 경우 오류 메시지 출력.
    /// </summary>
    public void PrintLoginError()
    {
        login_Error?.PrintErrorMessage("아이디 혹은 비밀번호가 틀렸습니다.");
    }

    /// <summary>
    /// 회원가입 때 아이디 오류가 생겼을 경우 에러 메시지 출력.
    /// </summary>
    /// <param name="errType">어떤 에러인지 타입을 정함. 이에 따라서 에러 메시지 출력.</param>
    public void PrintSignupIdError(ESignupErrType errType)
    {
        switch(errType)
        {
            case ESignupErrType.FormatError:
                signup_idError?.PrintErrorMessage("아이디 형식 맞추삼");
                break;
            case ESignupErrType.SameError:
                signup_idError?.PrintErrorMessage("같은 아이디가 있삼");
                break;
        }
    }

    /// <summary>
    /// 회원가입 때 비밀번호 오류가 생겼을 경우 에러 메시지 출력.
    /// </summary>
    /// <param name="errType">어떤 오류인지 타입 확인</param>
    public void PrintSignupPwError(ESignupErrType errType)
    {
        switch (errType)
        {
            case ESignupErrType.FormatError:
                signup_idError?.PrintErrorMessage("비밀번호 형식을 맞추세요.");
                break;
            case ESignupErrType.SameError:
                signup_idError?.PrintErrorMessage("비밀번호가 같지 않음.");
                break;
        }
    }
    #endregion
}
