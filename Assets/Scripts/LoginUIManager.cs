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

    #region ����
    [SerializeField]
    private string id = null;
    private string password = null;
    private string checkPassword = null;
    private string nickname = null;
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
    #endregion
}
