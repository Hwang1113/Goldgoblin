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

    private const string loginUri = "http://127.0.0.1/login.php";
    private const string SameIdUri = "http://127.0.0.1/SameId.php";


    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //���̵� ��й�ȣ �޾Ƽ� �α��� �ڷ�ƾ ����

        UImg.onClickSubmitBtn = Signin_Infos; // Signin��ư�� UI ���� ������ �븮�ڰ� Signin_Infos �� �����ϰ� ������ 
        UImg.onClickIDCheckBtn = SameIdCheck;  //�ߺ����� ��ư��  UI���� ������ �븮�ڰ� �ߺ������� ������
        UImg.
    }

    private void Signin_Infos() //���� �������� ������ �ڷ�ƾ(SigninCoroutine)�� ���� 
    {
        id = UImg.Id;
        Debug.Log("id:" + id);
        if (isDifferentId == true)
        {
            StartCoroutine(SigninCoroutine(id/*,password*/));
        }
        else
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
            else
                Debug.Log("���̵� ���� �Ұ���(�ߺ�)");
        }
    }


    private IEnumerator SigninCoroutine(string _id/*, string _password*/) //������ �������� ������ ���� �߰�, ȸ������ �ڷ�ƾ 
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
                else
                {
                    isDifferentId = true;
                }
            }
        }
    }
}
