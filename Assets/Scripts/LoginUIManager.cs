using System.Linq;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    #region 델리게이트 정의
    public delegate void OnClickSubmitBtnDelegate();
    public delegate void OnClickLoginBtnDelegate();
    public delegate void OnClickSignUpBtnDelegate();
    public delegate void OnClickIDCheckBtnDelegate();
    public delegate void OnSelectSignupIDInputfiledDelegate();
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

    /// <summary>
    /// 회원가입 창에서 아이디 입력 필드를 선택할 시 호출되는 델리게이트.
    /// </summary>
    public OnSelectSignupIDInputfiledDelegate onSelectSignupIDInputfiled = null;

    #endregion

    #region 열거형
    /// <summary>
    /// 회원가입 시 어떤 문제가 생겼는지 판단하기 위한 열거형.
    /// </summary>
    public enum ESignupErrType
    {
        /// <summary>
        /// 입력값의 형태가 맞지 않음.
        /// </summary>
        FormatError,
        /// <summary>
        /// 일치 오류
        /// </summary>
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
    [SerializeField, Header("--- UI Reference")]
    private PrintError login_Error = null;
    [SerializeField]
    private PrintError signup_idError = null;
    [SerializeField]
    private PrintError signup_pwError = null;
    [SerializeField]
    private PrintError signup_BirthDateError = null;
    [SerializeField]
    private GameObject loginMenu = null;
    [SerializeField]
    private GameObject signupMenu = null;
    #endregion

    #region 내부 변수
    [SerializeField]
    private string id = null;
    private string password = null;
    private string checkPassword = null;
    private string nickname = null;
    private string birthDate = null;
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

    public void InputBirthDate(string _birthDate)
    {
        birthDate = _birthDate;
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

    public void OnSelectSignupId()
    {
        onSelectSignupIDInputfiled?.Invoke();
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
                signup_pwError?.PrintErrorMessage("비밀번호는 8~16자이며, 대/소문자 숫자 특문이 하나씩 들어가야 합니다.");
                break;
            case ESignupErrType.SameError:
                signup_pwError?.PrintErrorMessage("비밀번호가 같지 않음.");
                break;
        }
    }

    /// <summary>
    /// 회원가입 때 생년월일 오류가 생겼을 경우 에러 메시지 출력.
    /// </summary>
    public void PrintBirthDateError()
    {
        signup_BirthDateError.PrintErrorMessage("YYMMDD 형식으로 입력하여야 합니다.");
    }

    /// <summary>
    /// 로그인 메뉴를 활성화, 비활성화시킨다.
    /// </summary>
    /// <param name="_active">활성화 여부</param>
    public void SetLoginMenuActivation(bool _active)
    {
        loginMenu.SetActive(_active);
    }

    /// <summary>
    /// 회원가입 메뉴를 활성화, 비활성화시킨다.
    /// </summary>
    /// <param name="_active">활성화 여부</param>
    public void SetSignupMenuActivation(bool _active)
    {
        signupMenu.SetActive(_active);
    }

    /// <summary>
    /// 아이디의 형태가 올바른지 확인함.
    /// </summary>
    /// <returns></returns>
    public bool IsIdFormatCorrect()
    {
        // 아이디는 공백이면 안되고, 8 ~ 16 자 사이여야 한다.
        if (string.IsNullOrEmpty(id) ||
            id.Length < 8 || id.Length > 16)
            return false;

        for(int i = 0; i < id.Length; ++i)
        {
            // 알파벳, 숫자, -, _ 만 허용된다.
            if (!char.IsLetterOrDigit(id[i]) || id[i] != '-' || id[i] != '_')
                return false;
        }

        return true;
    }

    /// <summary>
    /// 패스워드의 형태가 올바른지 확인함.
    /// </summary>
    /// <returns></returns>
    public bool IsPwFormatCorrect()
    {
        // 비밀번호는 공백이면 안되고, 8 ~ 16 자 사이여야 한다.
        if (string.IsNullOrEmpty(password) ||
            password.Length < 8 || password.Length > 16)
            return false;

        // arr_pwFormat 0 : 소문자 1 : 대문자 2 : 숫자 3 : 특문
        bool[] arr_pwFormat = new bool[4];

        // 허용하는 특문들.
        string allowedSpecialChars = "[!@#$%^&*()_+=\\[{\\]};:<>|./?,-]";

        for (int i = 0; i < password.Length; ++i)
        {
            if (char.IsLower(password[i])) arr_pwFormat[0] = true;
            else if (char.IsUpper(password[i])) arr_pwFormat[1] = true;
            else if (char.IsDigit(password[i])) arr_pwFormat[2] = true;
            else if (allowedSpecialChars.Contains(password[i])) arr_pwFormat[3] = true;
        }

        if (arr_pwFormat.Contains(false)) return false;

        return true;
    }

    /// <summary>
    /// 회원가입 때 패스워드 확인창과의 일치 여부를 확인함.
    /// </summary>
    /// <returns></returns>
    public bool IsPwCheckCorrect()
    {
        if (password != checkPassword) return false;

        return true;
    }

    /// <summary>
    /// 회원가입 때 생년월일의 형태가 올바른지 확인함.
    /// </summary>
    /// <returns></returns>
    public bool IsBirthDateFormatCorrect()
    {
        // 생년월일은 6자리 양의 정수여야 한다.
        if (string.IsNullOrEmpty(birthDate) || birthDate.Contains('-') || birthDate.Length != 6)
            return false;

        int month = int.Parse(birthDate.Substring(2, 2));
        int day = int.Parse(birthDate.Substring(4, 2));

        // 월은 1~12 월 사이여야 한다.
        if(month < 1 || month > 12)
            return false;

        // 일은 1~31 일 사이여야 한다.
        if (day < 1 || day > 31)
            return false;

        return true;
    }

    #endregion

    #region 프라이빗 함수
    #endregion
}
