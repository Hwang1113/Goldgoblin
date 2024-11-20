using System.Linq;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    #region ��������Ʈ ����
    public delegate void OnClickSubmitBtnDelegate();
    public delegate void OnClickLoginBtnDelegate();
    public delegate void OnClickSignUpBtnDelegate();
    public delegate void OnClickIDCheckBtnDelegate();
    public delegate void OnSelectSignupIDInputfiledDelegate();
    #endregion

    #region ��������Ʈ ����

    /// <summary>
    /// ȸ������ â���� Submit ��ư�� ������ �� ȣ��Ǵ� ��������Ʈ.
    /// </summary>
    public OnClickSubmitBtnDelegate onClickSubmitBtn = null;

    /// <summary>
    /// �α��� â���� ȸ������ ��ư�� ������ �� ȣ��Ǵ� ��������Ʈ.
    /// </summary>
    public OnClickSignUpBtnDelegate onClickSignUpBtn = null;

    /// <summary>
    /// �α��� â���� �α��� ��ư�� ������ �� ȣ��Ǵ� ��������Ʈ.
    /// </summary>
    public OnClickLoginBtnDelegate onClickLoginBtn = null;

    /// <summary>
    /// ȸ������ â���� ���̵� Ȯ�� ��ư�� ������ �� ȣ��Ǵ� ��������Ʈ.
    /// </summary>
    public OnClickIDCheckBtnDelegate onClickIDCheckBtn = null;

    /// <summary>
    /// ȸ������ â���� ���̵� �Է� �ʵ带 ������ �� ȣ��Ǵ� ��������Ʈ.
    /// </summary>
    public OnSelectSignupIDInputfiledDelegate onSelectSignupIDInputfiled = null;

    #endregion

    #region ������
    /// <summary>
    /// ȸ������ �� � ������ ������� �Ǵ��ϱ� ���� ������.
    /// </summary>
    public enum ESignupErrType
    {
        /// <summary>
        /// �Է°��� ���°� ���� ����.
        /// </summary>
        FormatError,
        /// <summary>
        /// ��ġ ����
        /// </summary>
        SameError
    }
    #endregion

    #region ������Ƽ��
    public string Id { get{ return id; } }
    public string Password { get{ return password; } }

    public string Nickname { get{ return nickname; } }

    public string RecoveryAnswer { get{ return recoveryAnswer; } }
    public int RecoveryInd { get{ return recoveryInd; } }
    #endregion

    #region UI ������Ʈ ���� ����
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

    #region ���� ����
    [SerializeField]
    private string id = null;
    private string password = null;
    private string checkPassword = null;
    private string nickname = null;
    private string birthDate = null;
    private string recoveryAnswer = null;
    private int recoveryInd = 0;
    #endregion

    #region UI�� ���ε��Ǵ� �Լ�
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

    #region �ۺ� �Լ�
    /// <summary>
    /// �α��� �� ���̵� ��й�ȣ�� Ʋ���� ��� ���� �޽��� ���.
    /// </summary>
    public void PrintLoginError()
    {
        login_Error?.PrintErrorMessage("���̵� Ȥ�� ��й�ȣ�� Ʋ�Ƚ��ϴ�.");
    }

    /// <summary>
    /// ȸ������ �� ���̵� ������ ������ ��� ���� �޽��� ���.
    /// </summary>
    /// <param name="errType">� �������� Ÿ���� ����. �̿� ���� ���� �޽��� ���.</param>
    public void PrintSignupIdError(ESignupErrType errType)
    {
        switch(errType)
        {
            case ESignupErrType.FormatError:
                signup_idError?.PrintErrorMessage("���̵� ���� ���߻�");
                break;
            case ESignupErrType.SameError:
                signup_idError?.PrintErrorMessage("���� ���̵� �ֻ�");
                break;
        }
    }

    /// <summary>
    /// ȸ������ �� ��й�ȣ ������ ������ ��� ���� �޽��� ���.
    /// </summary>
    /// <param name="errType">� �������� Ÿ�� Ȯ��</param>
    public void PrintSignupPwError(ESignupErrType errType)
    {
        switch (errType)
        {
            case ESignupErrType.FormatError:
                signup_pwError?.PrintErrorMessage("��й�ȣ�� 8~16���̸�, ��/�ҹ��� ���� Ư���� �ϳ��� ���� �մϴ�.");
                break;
            case ESignupErrType.SameError:
                signup_pwError?.PrintErrorMessage("��й�ȣ�� ���� ����.");
                break;
        }
    }

    /// <summary>
    /// ȸ������ �� ������� ������ ������ ��� ���� �޽��� ���.
    /// </summary>
    public void PrintBirthDateError()
    {
        signup_BirthDateError.PrintErrorMessage("YYMMDD �������� �Է��Ͽ��� �մϴ�.");
    }

    /// <summary>
    /// �α��� �޴��� Ȱ��ȭ, ��Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="_active">Ȱ��ȭ ����</param>
    public void SetLoginMenuActivation(bool _active)
    {
        loginMenu.SetActive(_active);
    }

    /// <summary>
    /// ȸ������ �޴��� Ȱ��ȭ, ��Ȱ��ȭ��Ų��.
    /// </summary>
    /// <param name="_active">Ȱ��ȭ ����</param>
    public void SetSignupMenuActivation(bool _active)
    {
        signupMenu.SetActive(_active);
    }

    /// <summary>
    /// ���̵��� ���°� �ùٸ��� Ȯ����.
    /// </summary>
    /// <returns></returns>
    public bool IsIdFormatCorrect()
    {
        // ���̵�� �����̸� �ȵǰ�, 8 ~ 16 �� ���̿��� �Ѵ�.
        if (string.IsNullOrEmpty(id) ||
            id.Length < 8 || id.Length > 16)
            return false;

        for(int i = 0; i < id.Length; ++i)
        {
            // ���ĺ�, ����, -, _ �� ���ȴ�.
            if (!char.IsLetterOrDigit(id[i]) || id[i] != '-' || id[i] != '_')
                return false;
        }

        return true;
    }

    /// <summary>
    /// �н������� ���°� �ùٸ��� Ȯ����.
    /// </summary>
    /// <returns></returns>
    public bool IsPwFormatCorrect()
    {
        // ��й�ȣ�� �����̸� �ȵǰ�, 8 ~ 16 �� ���̿��� �Ѵ�.
        if (string.IsNullOrEmpty(password) ||
            password.Length < 8 || password.Length > 16)
            return false;

        // arr_pwFormat 0 : �ҹ��� 1 : �빮�� 2 : ���� 3 : Ư��
        bool[] arr_pwFormat = new bool[4];

        // ����ϴ� Ư����.
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
    /// ȸ������ �� �н����� Ȯ��â���� ��ġ ���θ� Ȯ����.
    /// </summary>
    /// <returns></returns>
    public bool IsPwCheckCorrect()
    {
        if (password != checkPassword) return false;

        return true;
    }

    /// <summary>
    /// ȸ������ �� ��������� ���°� �ùٸ��� Ȯ����.
    /// </summary>
    /// <returns></returns>
    public bool IsBirthDateFormatCorrect()
    {
        // ��������� 6�ڸ� ���� �������� �Ѵ�.
        if (string.IsNullOrEmpty(birthDate) || birthDate.Contains('-') || birthDate.Length != 6)
            return false;

        int month = int.Parse(birthDate.Substring(2, 2));
        int day = int.Parse(birthDate.Substring(4, 2));

        // ���� 1~12 �� ���̿��� �Ѵ�.
        if(month < 1 || month > 12)
            return false;

        // ���� 1~31 �� ���̿��� �Ѵ�.
        if (day < 1 || day > 31)
            return false;

        return true;
    }

    #endregion

    #region �����̺� �Լ�
    #endregion
}
