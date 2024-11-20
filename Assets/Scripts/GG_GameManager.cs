using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class GG_GameManager : MonoBehaviour
{
    private bool isDifferentId = false; //���̵� �ߺ����� �ƴ����� �����ϴ� bool ��
    private bool isAllInfoChecked = false; //��� �׸��� true���� �ƴ��� �����ϴ� bool ��

    [SerializeField]
    private LoginUIManager UImg = null;
    [SerializeField]
    private string id = string.Empty; //���̵� ���ڿ�
    [SerializeField]
    private string password = string.Empty; // ��й�ȣ ���ڿ�
    [SerializeField]
    private string nickname = string.Empty; // �г��� ���ڿ�
    [SerializeField]
    private string birthday = string.Empty; // �ֹι�ȣ�� ���ڿ� // 990909
    [SerializeField]
    private int AR_Q = 0; // ����������ȣ ������
    [SerializeField]
    private string AR_A = string.Empty; // �����亯 ���ڿ�



    //player item db �޾ƿ°� ������ ����������
    private string owner = string.Empty; //������ ����
    private string[] item = null; //������(�̸�) �迭
    private string[] ea = null; // ������ ����



    private const string loginUri = "http://127.0.0.1/login.php";
    private const string SameIdUri = "http://127.0.0.1/SameId.php";


    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //���̵� ��й�ȣ �޾Ƽ� �α��� �ڷ�ƾ ����

        UImg.onClickSubmitBtn = SignUpnfos; // SignUp��ư�� UI ���� ������ �븮�ڰ� SignUp_Infos �� �����ϰ� ������ 
        UImg.onClickIDCheckBtn = SameIdCheck;  //�ߺ����� ��ư��  UI���� ������ �븮�ڰ� �ߺ������� ������
        UImg.onSelectSignupIDInputfiled = IdCheckFalse; // ID Inputfield �� Ŭ���ϸ�  isDifferentId = false; 
        UImg.onClickSignUpBtn = GoSignUp; // �α��� â���� SignUp ��ư�� ������ GoSignUp(); 
        //UImg.onClickLoginBtn = //�α��� ��ư�� ������
    }
    private void AllInfoCheck()
    {
        if (isDifferentId/*��� �׸�üũ ���⼭ ��*/)  //��ȣ�ȿ� ���� ��� �ֱ�
        {
            isAllInfoChecked = true;
        }
        else 
        {
            isAllInfoChecked = false;
        }
    }
    private void SignUpnfos() //���� �������� ������ ȸ������ �ڷ�ƾ(SignUpCoroutine)�� ���� 
    {
        AllInfoCheck(); //��� �׸�üũ ���� isAllInfoChecked�� true Ȥ�� false�� ��ȯ

        if (!isAllInfoChecked) 
        {
            Debug.Log("Ȯ�ε��� �ʴ� �׸��� �ֽ��ϴ�");
            return;
        }

        id = UImg.Id;//UI�� ���� Id�� id�� ����
        Debug.Log("id:" + id);

        if (isDifferentId == true)
        {
            StartCoroutine(SignUpCoroutine(id/*,password*/));
        }
        else if (isDifferentId == false)
        {
            Debug.Log("Id �ߺ�Ȯ�� �� ȸ������ ");
        }
    }
    private void SameIdCheck()
    {
        if (isDifferentId == true)
        //���߿� ���̵� �������� �ٲ����� �ٽ� 
        //onValuechanged �� ���� isDifferentId�� false�� �ٲܰ� �ȱ׷��� �ߺ�����üũ�� ���̵� �ٲ� �� ȸ�������� �� ���� 11.20
        {
            Debug.Log("���̵� ���� ����(�ߺ��� �� ����)");
            return;
        }
        if (isDifferentId == false)
        {
            id = UImg.Id;
            StartCoroutine(SameIdCheckCoroutine(id));// �ڷ�ƾ �ȿ��� �ߺ����� Ȯ���ؼ� �ڷ�ƾ���� isDifferentId�� false�� true�� �ٲ���
            if (isDifferentId == true)
                Debug.Log("���̵� ���� ����(�ߺ��� �� ����)");
            else if(isDifferentId == false)
                Debug.Log("���̵� ���� �Ұ���(�ߺ�)");
        }
    }
    private void GoSignUp() //�α��� â�� ����, ȸ������â�� Ų��.
    {
        UImg.SetLoginMenuActivation(false);
        UImg.SetSignupMenuActivation(true);
    }
    private void IdCheckFalse() //isDifferentId = false; �� ����
    {
        isDifferentId = false;
        Debug.Log("���̵� �Է� �� �ߺ�üũ�� �����ÿ�");
    }


    //////////////////////////////////////////////////
    //�����δ� DB������ ���� ���� �ڷ�ƾ��
    //////////////////////////////////////////////////
    private IEnumerator SignUpCoroutine(string _id/*, string _password*/) //������ �������� ������ ���� , ȸ������ �ڷ�ƾ 
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����
        form.AddField("Id", _id);
        //form.AddField("Password", _password);
        //�������� �񵿱� ���
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form)) //post�� ���� //get�� �ӵ�
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
    private IEnumerator SignInCoroutine(string _id/*, string _password*/) //������ �������� ������ ����, �α��� �ڷ�ƾ 
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����
        form.AddField("Id", _id);
        //form.AddField("Password", _password);
        //�������� �񵿱� ���
        using (UnityWebRequest www = UnityWebRequest.Post(loginUri, form)) //post�� ���� //get�� �ӵ�
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
                //�α��� ���� �����ϱ� �������� �� ����ϳ�
                string data = www.downloadHandler.text; //�ϴ� json �������� �ۼ��� "���ڿ�" ������ ������ �޾ƿͼ�
                // ������ȭ�� ���� ���ӸŴ����� ������DB �޾ƿ�, �ۼ��ؾ���
                //
            }
        }
    }

    private IEnumerator SameIdCheckCoroutine(string _id) //���̵� �ߺ����� �ڷ�ƾ
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����
        form.AddField("Id", _id); //������ id�� �Ѱ���
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
                string CheckId = www.downloadHandler.text; //�������� �ٸ��� ������ ���ڿ��� �˷���
                if (CheckId == "false") //������ ������ false�� �亯 ���ֱ����
                {
                    isDifferentId = false;
                }
                else if(CheckId == "true") //������ ������ true�� �亯 ���ֱ����
                {
                    isDifferentId = true;
                }
                else 
                {
                    Debug.Log("������ �̻��ؿ�");
                }
            }
        }
    }
}
