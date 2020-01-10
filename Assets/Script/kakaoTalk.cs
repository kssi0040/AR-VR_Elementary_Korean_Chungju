using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kakaoTalk : MonoBehaviour
{
    public GameObject PlayerTalk;
    public GameObject KingTalk;
    public GameObject SystemTalk;
    public ScrollRect scrollRect;
    public Sprite kingSprite;
    public Sprite Ch1Sprite;
    public Sprite Ch2Sprite;
    public bool isClicked;
	// Use this for initialization
	void Start ()
    {
        //makeTalk();
        isClicked = false;
    }
    
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClickedScrollBar()
    {
        isClicked = true;
    }

    public void releaseScrollBar()
    {
        isClicked = false;
    }

    public void makeTalk()
    {
        GameObject _PlayerTalk;
        for (int k = 0; k < 50; k++)
        {
            _PlayerTalk = Instantiate(KingTalk);
            _PlayerTalk.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = k.ToString();
            _PlayerTalk.transform.SetParent(scrollRect.content, false);
        }
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
    public void makeTalk(int Stage, int id, string talk)
    {
        GameObject _PlayerTalk;
        string text;
        switch (talk)
        {
            case "teach_talk":
                _PlayerTalk = Instantiate(SystemTalk);
                text = Language_Text_Script1.Instance.scenario[Stage].text[id];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    _PlayerTalk.transform.GetChild(0).GetComponent<Text>().text = temp;
                }
                else
                {
                    _PlayerTalk.transform.GetChild(0).GetComponent<Text>().text = Language_Text_Script1.Instance.scenario[Stage].text[id];
                }
                _PlayerTalk.transform.SetParent(scrollRect.content, false);
                break;
            case "king_talk":
                _PlayerTalk = Instantiate(KingTalk);
                if(id==36)
                {
                    _PlayerTalk.transform.GetChild(0).GetComponent<Image>().sprite = kingSprite;
                }
                text = Language_Text_Script1.Instance.scenario[Stage].text[id];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    _PlayerTalk.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = temp;
                }
                else
                {
                    _PlayerTalk.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Language_Text_Script1.Instance.scenario[Stage].text[id];
                }
                _PlayerTalk.transform.SetParent(scrollRect.content, false);
                break;
            case "player_talk":
                _PlayerTalk = Instantiate(PlayerTalk);
                if(AppManage.Instance.Gender==0)
                {
                    //_PlayerTalk.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("PlayScreen_국어_카톡_3");
                    _PlayerTalk.transform.GetChild(0).GetComponent<Image>().sprite = Ch2Sprite;
                }
                else
                {
                    //_PlayerTalk.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("PlayScreen_국어_카톡_5");
                    _PlayerTalk.transform.GetChild(0).GetComponent<Image>().sprite = Ch1Sprite;
                }
                
                text=Language_Text_Script1.Instance.scenario[Stage].text[id];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    _PlayerTalk.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = temp;
                }
                else
                {
                    _PlayerTalk.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Language_Text_Script1.Instance.scenario[Stage].text[id];
                }
                _PlayerTalk.transform.SetParent(scrollRect.content, false);
                break;

        }
        //scrollRect.verticalNormalizedPosition = 0.0f;
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0.0f;
        //scrollRect.verticalScrollbar.value = 0.0f;
        Canvas.ForceUpdateCanvases();
    }
}
