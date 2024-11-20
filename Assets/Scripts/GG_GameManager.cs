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
    private string id = string.Empty; //���̵� ���ڿ�
    [SerializeField]
    private string password = string.Empty; // ��й�ȣ ���ڿ�
    [SerializeField]
    private string nickname = string.Empty; // �г��� ���ڿ�
    [SerializeField]
    private string birthday = string.Empty; // ���� ���ڿ�
    [SerializeField]
    private string AR_Q = string.Empty; // �������� ���ڿ�
    [SerializeField]
    private string AR_A = string.Empty; // �����亯 ���ڿ�

    private const string loginUri = "http://127.0.0.1/login.php";

    private void Start()
    {
        //StartCoroutine(LoginCoroutine(id)); //���̵� ��й�ȣ �޾Ƽ� �α��� �ڷ�ƾ ����

        UImg.onClickSubmitBtn = Signin_Infos; // ��ư�� ������ �븮�ڰ� Signin_Infos �� �����ϰ� ������ 
    }
    
    private void Signin_Infos() //���� �������� ������ �ڷ�ƾ(SigninCoroutine)�� ����
    {
        id = UImg.Id;
        Debug.Log("id:" + id);

        StartCoroutine(SigninCoroutine(id/*,password*/));
    }

    private IEnumerator SigninCoroutine(string _id/*, string _password*/) //ȸ������ �ڷ�ƾ
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����

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
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    private IEnumerator LoginCoroutine(string _id/*, string _password*/) //�α��� �ڷ�ƾ
    {
        WWWForm form = new WWWForm(); //���� ���� ���¸� ����

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
                 Debug.Log(www.downloadHandler.text);
                 AR_A = www.downloadHandler.text;
            }
        }
    }
}
