using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LockManage : MonoBehaviour
{
    public RectTransform Stage1Lock;
    public RectTransform Stage2Lock;
    public RectTransform Stage3Lock;
    public RectTransform Stage4Lock;
    public RectTransform Stage5Lock;
    public RectTransform Stage1Clear;
    public RectTransform Stage2Clear;
    public RectTransform Stage3Clear;
    public RectTransform Stage4Clear;
    public RectTransform Stage5Clear;
    public Transform badge1;
    public Transform badge2;
    public Transform badge3;
    public Transform badge4;
    public Transform badge5;
    public Transform badge6;
    public Transform badge7;
    public RectTransform PopupCanvas;
    // Use this for initialization
    void Start ()
    {
        PopupCanvas.transform.gameObject.SetActive(false);
        Stage1Lock.transform.gameObject.SetActive(false);
        if(AppManage.Instance.ClearStage1==1)
        {
            Debug.Log("북촌 해금");
            Stage2Lock.transform.gameObject.SetActive(false);
            Stage1Clear.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.ClearStage2==1)
        {
            Debug.Log("나머지 해금");
            Stage2Clear.transform.gameObject.SetActive(true);
            Stage3Lock.transform.gameObject.SetActive(false);
            Stage4Lock.transform.gameObject.SetActive(false);
            Stage5Lock.transform.gameObject.SetActive(false);
            PopupCanvas.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.ClearStage3==1)
        {
            Stage3Clear.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.ClearStage4==1)
        {
            Stage4Clear.transform.gameObject.SetActive(true);
        }
        if(AppManage.Instance.ClearStage5==1)
        {
            Stage5Clear.transform.gameObject.SetActive(true);
        }

        if(AppManage.Instance.badge1==1)
        {
            badge1.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge2 == 1)
        {
            badge2.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge3 == 1)
        {
            badge3.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge4 == 1)
        {
            badge4.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge5 == 1)
        {
            badge5.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge6 == 1)
        {
            badge6.transform.gameObject.SetActive(true);
        }
        if (AppManage.Instance.badge7 == 1)
        {
            badge7.transform.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    
    }

    public void PopupClose()
    {
        PopupCanvas.transform.gameObject.SetActive(false);
    }
}
