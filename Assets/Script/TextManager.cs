using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class TextManager : MonoBehaviour
{
    public int currentStage;
    public int count = 0;
    public string text;
    
    [SerializeField]
    int tempInt = 0;


    public RectTransform Link;
    public RectTransform Link2;
    public RectTransform Controller;
    public RectTransform LinkCanvas;
    public RectTransform Typing1Canvas;
    public RectTransform Typing2Canvas;
    public RectTransform OptionCanvas;
    public RectTransform KakaoCanvas;

    public Transform sphere;
    public Transform CH1_nomal;
    public Transform CH2_nomal;
    public Transform CH3;
    public Transform Place;
    public Transform Fire;
    public Transform badge1;
    public Transform badge2;
    public Transform badge3;
    public Transform badge4;
    public Transform badge5;
    public Transform badge6;
    public Transform badge7;

    public Image BackGround;
    public Image PopUp;
    public Image Highlight;
    public Image sphereIcon;
    public Image BadgeCase;
    public RawImage TextBox;
    public RawImage NPCTextBox;

    public Text str;
    public Text PopUpText;
    public Text NPCText;
    
    public Animator CHS;
    public Animator NPC;
    public Animator WoodNote;
    
    public Canvas canvas;
    public AudioSource Gayagum;
    public RectTransform CharacterScroll;
    public RectTransform PopUpScroll;
    public RectTransform NPCScroll;

    LinkQuizManager linkQuizManager;

    kakaoTalk Kakaotalk;

    AnimatorStateInfo animInfo;
    private void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        linkQuizManager = FindObjectOfType<LinkQuizManager>();
        Kakaotalk = FindObjectOfType<kakaoTalk>();

        LinkCanvas.transform.gameObject.SetActive(false);
        Typing1Canvas.transform.gameObject.SetActive(false);
        Typing2Canvas.transform.gameObject.SetActive(false);
        OptionCanvas.transform.gameObject.SetActive(false);


        Link.transform.gameObject.SetActive(false);
        Link2.transform.gameObject.SetActive(false);
        PopUp.transform.gameObject.SetActive(false);
        PopUpText.transform.gameObject.SetActive(false);
        PopUpScroll.transform.gameObject.SetActive(false);
        
        
        CH1_nomal.transform.gameObject.SetActive(false);
        CH2_nomal.transform.gameObject.SetActive(false);
        CH3.transform.gameObject.SetActive(true);

        CHS.SetTrigger("teachAnim");

        text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
        if(text.Contains("###"))
        {
            string temp = text.Replace("###", AppManage.Instance.Name);
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
        }
        else
        {
            GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
        }
        switch(currentStage)
        {
            case 1:
                sphereIcon.transform.gameObject.SetActive(false);
                KakaoCanvas.transform.gameObject.SetActive(false);
                sphere.transform.gameObject.SetActive(false);
                Controller.transform.gameObject.SetActive(false);
                WoodNote.transform.gameObject.SetActive(false);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                {
                    NPC.transform.gameObject.SetActive(false);
                    NPCText.transform.gameObject.SetActive(false);
                    NPCTextBox.transform.gameObject.SetActive(false);
                    NPCScroll.transform.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
        if(currentStage==2)
        {
            Fire.transform.gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        switch (currentStage)
        {
            case 1:
                switch (count)
                {
                    case 5:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
            case 4:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
            case 5:
                switch(count)
                {
                    case 0:
                        BackGround.transform.gameObject.SetActive(false);
                        break;
                }
                break;
        }
    }
    
    
    public void forwardDown()
    {
        Text tempText;
        
        if (AppManage.Instance.isComplite)
        {
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 4:
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 5:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 12:
                            if(linkQuizManager.OptionClear==false)
                            {
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                AppManage.Instance.isComplite = true;
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                            }
                            else
                            {
                                tempInt++;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                linkQuizManager.OptionClear = false;
                                str.text = string.Empty;
                                PopUp.transform.gameObject.SetActive(true);
                                PopUpScroll.transform.gameObject.SetActive(true);
                                GameObject.Find("WebManager").SendMessage("initURL", "https://www.chungju.go.kr/tour/contents.do?key=1202");
                                Link.transform.gameObject.SetActive(true);
                                tempText = GameObject.Find("Text").GetComponent<Text>();
                                tempText.text = "중앙탑사적공원과 충주박물관";
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                count++;
                            }
                            break;
                        case 13:
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.chungju.go.kr/tour/contents.do?key=1202");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "중앙탑사적공원과 충주박물관";
                            count++;
                            break;
                        case 14:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge1 = 1;
                                AppManage.Instance.Write_information();

                            }
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            count++;
                            break;
                        case 15:
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 17:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 19:
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 20:
                            switch(tempInt)
                            {
                                case 1:
                                    if(linkQuizManager.OptionClear==false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        CharacterScroll.transform.gameObject.SetActive(false);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.GenerateOptionQuiz(0, tempInt);

                                    }
                                    else
                                    {
                                        tempInt++;
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.OptionClear = false;
                                        linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                    }
                                    break;
                                case 2:
                                    if(linkQuizManager.OptionClear==true)
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        CharacterScroll.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.ClearStage1 == 0)
                                        {
                                            AppManage.Instance.badge4 = 1;
                                            AppManage.Instance.Write_information();
                                          
                                        }
                                        BadgeCase.transform.gameObject.SetActive(true);
                                        for (int k = 0; k < 7; k++)
                                        {
                                            switch (k)
                                            {
                                                case 0:
                                                    if (AppManage.Instance.badge1 == 1)
                                                    {
                                                        badge1.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 1:
                                                    if (AppManage.Instance.badge2 == 1)
                                                    {
                                                        badge2.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 2:
                                                    if (AppManage.Instance.badge3 == 1)
                                                    {
                                                        badge3.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 3:
                                                    if (AppManage.Instance.badge4 == 1)
                                                    {
                                                        badge4.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 4:
                                                    if (AppManage.Instance.badge5 == 1)
                                                    {
                                                        badge5.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 5:
                                                    if (AppManage.Instance.badge6 == 1)
                                                    {
                                                        badge6.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 6:
                                                    if (AppManage.Instance.badge7 == 1)
                                                    {
                                                        badge7.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                            }
                                        }
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 21:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 25:
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 26:
                            if(linkQuizManager.OptionClear==false)
                            {
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 28:
                            if(linkQuizManager.OptionClear==false)
                            {
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 29:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            KakaoCanvas.transform.gameObject.SetActive(true);
                            Kakaotalk.makeTalk(currentStage, count, Language_Text_Script1.Instance.scenario[currentStage].who[count]);
                            break;
                        case 30:
                            if (linkQuizManager.OptionClear == false)
                            {
                                linkQuizManager.GenerateOptionQuiz(0, tempInt);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                KakaoCanvas.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                tempInt++;
                                linkQuizManager.OptionClear = false;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                count++;
                            }
                            break;
                        case 33:
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge5 = 1;
                                AppManage.Instance.Write_information();

                            }
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            count++;
                            break;
                        case 34:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 35:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 36:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            WoodNote.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 38:
                            str.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            WoodNote.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 39:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            WoodNote.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 40:
                            RectTransform _button = FindObjectOfType<EventManager>().button;
                            _button.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 41:
                            break;
                        case 43:
                            WoodNote.transform.gameObject.SetActive(false);
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge2 = 1;
                            }
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            count++;
                            break;
                        case 44:
                            AppManage.Instance.EndStage(1);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 2:
                            switch (tempInt)
                            {
                                case 0:
                                    if (linkQuizManager.LinkClear == false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        NPC.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        CharacterScroll.transform.gameObject.SetActive(false);
                                        LinkCanvas.transform.gameObject.SetActive(true);
                                        linkQuizManager.GenerateLinkQuiz(1,tempInt);
                                       
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        linkQuizManager.LinkClear = false;
                                        tempInt++;
                                        linkQuizManager.GenerateLinkQuiz(1, tempInt);
                                    }
                                    break;
                                case 1:
                                    if(linkQuizManager.LinkClear==true)
                                    {
                                        linkQuizManager.LinkClear = false;
                                        tempInt++;
                                        LinkCanvas.transform.gameObject.SetActive(false);
                                        str.transform.gameObject.SetActive(true);
                                        TextBox.transform.gameObject.SetActive(true);
                                        CharacterScroll.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.ClearStage2 == 0)
                                        {
                                            AppManage.Instance.badge6 = 1;
                                            AppManage.Instance.Write_information();

                                        }
                                        BadgeCase.transform.gameObject.SetActive(true);
                                        for (int k = 0; k < 7; k++)
                                        {
                                            switch (k)
                                            {
                                                case 0:
                                                    if (AppManage.Instance.badge1 == 1)
                                                    {
                                                        badge1.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 1:
                                                    if (AppManage.Instance.badge2 == 1)
                                                    {
                                                        badge2.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 2:
                                                    if (AppManage.Instance.badge3 == 1)
                                                    {
                                                        badge3.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 3:
                                                    if (AppManage.Instance.badge4 == 1)
                                                    {
                                                        badge4.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 4:
                                                    if (AppManage.Instance.badge5 == 1)
                                                    {
                                                        badge5.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 5:
                                                    if (AppManage.Instance.badge6 == 1)
                                                    {
                                                        badge6.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                                case 6:
                                                    if (AppManage.Instance.badge7 == 1)
                                                    {
                                                        badge7.transform.gameObject.SetActive(true);
                                                    }
                                                    break;
                                            }
                                        }
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 3:
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 5:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 7:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 8:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 9:
                            if(linkQuizManager.OptionClear==false)
                            {
                                NPC.transform.gameObject.SetActive(false);
                                str.text = string.Empty;
                                str.transform.gameObject.SetActive(false);
                                CharacterScroll.transform.gameObject.SetActive(false);
                                TextBox.transform.gameObject.SetActive(false);
                                OptionCanvas.transform.gameObject.SetActive(true);
                                linkQuizManager.GenerateOptionQuiz(1, tempInt);
                                if (AppManage.Instance.Gender == 0)
                                {
                                    CH1_nomal.gameObject.SetActive(false);
                                    CH2_nomal.gameObject.SetActive(true);
                                    CH3.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH2Anim");
                                }
                                else
                                {
                                    CH1_nomal.gameObject.SetActive(true);
                                    CH2_nomal.gameObject.SetActive(false);
                                    CH3.gameObject.SetActive(false);
                                    CHS.SetTrigger("CH1Anim");
                                }
                            }
                            else
                            {
                                tempInt++;
                                OptionCanvas.transform.gameObject.SetActive(false);
                                str.transform.gameObject.SetActive(true);
                                TextBox.transform.gameObject.SetActive(true);
                                CharacterScroll.transform.gameObject.SetActive(true);
                                Fire.transform.gameObject.SetActive(true);
                                if (AppManage.Instance.ClearStage2 == 0)
                                {
                                    AppManage.Instance.badge7 = 1;
                                    AppManage.Instance.Write_information();

                                }
                                BadgeCase.transform.gameObject.SetActive(true);
                                for (int k = 0; k < 7; k++)
                                {
                                    switch (k)
                                    {
                                        case 0:
                                            if (AppManage.Instance.badge1 == 1)
                                            {
                                                badge1.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 1:
                                            if (AppManage.Instance.badge2 == 1)
                                            {
                                                badge2.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 2:
                                            if (AppManage.Instance.badge3 == 1)
                                            {
                                                badge3.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 3:
                                            if (AppManage.Instance.badge4 == 1)
                                            {
                                                badge4.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 4:
                                            if (AppManage.Instance.badge5 == 1)
                                            {
                                                badge5.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 5:
                                            if (AppManage.Instance.badge6 == 1)
                                            {
                                                badge6.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                        case 6:
                                            if (AppManage.Instance.badge7 == 1)
                                            {
                                                badge7.transform.gameObject.SetActive(true);
                                            }
                                            break;
                                    }
                                }
                                count++;
                            }
                            break;
                        case 10:
                            Fire.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 11:
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 12:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 16:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 17:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 18:
                            NPC.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 19:
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 21:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 22:
                            if (AppManage.Instance.ClearStage2 == 0)
                            {
                                AppManage.Instance.badge3 = 1;
                                AppManage.Instance.Write_information();

                            }
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            NPC.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 23:
                            SceneManager.LoadScene("Stage2_End");
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 1:
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.chungju.go.kr/tour/contents.do?key=1173");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "탄금대 홈페이지";
                            count++;
                            break;
                        case 2:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPC.SetTrigger("wooAnim");
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            Link.transform.gameObject.SetActive(false);
                            Gayagum.Play();
                            count++;
                            break;
                        case 4:
                            switch(tempInt)
                            {
                                case 0:
                                    if(linkQuizManager.OptionClear==false)
                                    {
                                        str.text = string.Empty;
                                        str.transform.gameObject.SetActive(false);
                                        TextBox.transform.gameObject.SetActive(false);
                                        CharacterScroll.transform.gameObject.SetActive(false);
                                        NPC.transform.gameObject.SetActive(false);
                                        NPCText.text = string.Empty;
                                        NPCText.transform.gameObject.SetActive(false);
                                        NPCTextBox.transform.gameObject.SetActive(false);
                                        NPCScroll.transform.gameObject.SetActive(false);
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.GenerateOptionQuiz(2, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                    }
                                    else
                                    {
                                        tempInt++;
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.OptionClear = false;
                                        linkQuizManager.GenerateOptionQuiz(2, tempInt);
                                    }
                                    break;
                                case 1:
                                    if(linkQuizManager.OptionClear==true)
                                    {
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        NPC.transform.gameObject.SetActive(true);
                                        NPC.SetTrigger("wooAnim");
                                        NPCText.transform.gameObject.SetActive(true);
                                        NPCTextBox.transform.gameObject.SetActive(true);
                                        NPCScroll.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 5:
                            AppManage.Instance.EndStage(3);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            count++;
                            break;
                        case 1:
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPC.SetTrigger("wooSmile");
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            Gayagum.Play();
                            count++;
                            break;
                        case 3:
                            str.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.chungju.go.kr/tour/contents.do?key=1173");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "열두대 홈페이지";
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 4:
                            switch(tempInt)
                            {
                                case 0:
                                    if(linkQuizManager.OptionClear==false)
                                    {
                                        PopUp.transform.gameObject.SetActive(false);
                                        PopUpText.text = string.Empty;
                                        PopUpText.transform.gameObject.SetActive(false);
                                        PopUpScroll.transform.gameObject.SetActive(false);
                                        Link.transform.gameObject.SetActive(false);
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.OptionClear = false;
                                        AppManage.Instance.isComplite = true;
                                        linkQuizManager.GenerateOptionQuiz(3, tempInt);

                                    }
                                    break;
                                case 1:
                                    if(linkQuizManager.OptionClear==true)
                                    {
                                        AppManage.Instance.EndStage(4);
                                    }
                                    break;
                            }
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                case 5:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            count++;
                            break;
                        case 1:
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            Gayagum.Play();
                            str.text = string.Empty;
                            count++;
                            break;
                        case 3:
                            str.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            GameObject.Find("WebManager").SendMessage("initURL", "http://www.suanbo.or.kr/");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "수안보온천 홈페이지";
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            CHS.SetTrigger("teachAnim");
                            count++;
                            break;
                        case 4:
                            switch(tempInt)
                            {
                                case 0:
                                    if (linkQuizManager.LinkClear==false)
                                    {
                                        PopUp.transform.gameObject.SetActive(false);
                                        PopUpText.text = string.Empty;
                                        PopUpText.transform.gameObject.SetActive(false);
                                        PopUpScroll.transform.gameObject.SetActive(false);
                                        Link.transform.gameObject.SetActive(false);
                                        AppManage.Instance.isComplite = true;
                                        LinkCanvas.transform.gameObject.SetActive(true);
                                        linkQuizManager.GenerateLinkQuiz(4, 0);
                                        if (AppManage.Instance.Gender == 0)
                                        {
                                            CH1_nomal.gameObject.SetActive(false);
                                            CH2_nomal.gameObject.SetActive(true);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH2Anim");
                                        }
                                        else
                                        {
                                            CH1_nomal.gameObject.SetActive(true);
                                            CH2_nomal.gameObject.SetActive(false);
                                            CH3.gameObject.SetActive(false);
                                            CHS.SetTrigger("CH1Anim");
                                        }
                                    }
                                    else
                                    {
                                        tempInt++;
                                        linkQuizManager.LinkClear = false;
                                        LinkCanvas.transform.gameObject.SetActive(false);
                                        AppManage.Instance.isComplite = true;
                                        OptionCanvas.transform.gameObject.SetActive(true);
                                        linkQuizManager.GenerateOptionQuiz(4, tempInt);
                                    }
                                    break;
                                case 1:
                                    if(linkQuizManager.OptionClear==true)
                                    {
                                        linkQuizManager.OptionClear = false;
                                        OptionCanvas.transform.gameObject.SetActive(false);
                                        NPC.transform.gameObject.SetActive(true);
                                        NPC.SetTrigger("wooAnim");
                                        NPCText.transform.gameObject.SetActive(true);
                                        NPCTextBox.transform.gameObject.SetActive(true);
                                        NPCScroll.transform.gameObject.SetActive(true);
                                        count++;
                                    }
                                    break;
                            }
                            break;
                        case 5:
                            AppManage.Instance.EndStage(5);
                            break;
                        default:
                            count++;
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch(currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 18:
                        case 19:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 35:
                        case 36:
                            KakaoCanvas.transform.gameObject.SetActive(true);
                            Kakaotalk.makeTalk(currentStage, count, Language_Text_Script1.Instance.scenario[currentStage].who[count]);
                            break;
                        case 13:
                        case 14:
                        case 39:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 30:
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch(count)
                    {
                        case 4:
                        case 5:
                        case 8:
                        case 11:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 20:
                        case 21:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 18:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 1:
                        case 2:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 3:
                        case 4:
                        case 5:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 1:
                        case 4:
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 2:
                        case 3:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 5:
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 5:
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 4:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 3:
                        case 4:
                        case 9:
                        case 12:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 27:
                        case 30:
                        case 32:
                        case 33:
                        case 35:
                        case 36:
                        case 37:
                        case 41:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 1:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 2:
                        case 9:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 3:
                        case 4:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 2:
                        case 3:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 4:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 4:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else
            {
                if (currentStage == 1)
                {
                    CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                }
                else if(currentStage==2)
                {
                    if(count!=2)
                    {
                        CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if(currentStage==4)
                {
                    if(count!=4)
                    {
                        CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else if (currentStage == 5)
                {
                    if (count != 4)
                    {
                        CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                    }
                }
                else
                {
                    CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                }
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 18:
                        case 19:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 30:
                        case 35:
                        case 36:
                            AppManage.Instance.isComplite = true;
                            break;
                        default:
                            AppManage.Instance.isComplite = false;
                            break;
                    }
                    break;
                default:
                    AppManage.Instance.isComplite = false;
                    break;
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }

    public void BeforeDown()
    {
        Text tempText;
        RectTransform _button = FindObjectOfType<EventManager>().button;
        if (AppManage.Instance.isComplite)
        {
            if (count > 0)
            {
                count--;
            }
            else
            {
                SceneManager.LoadScene("SelectMap");
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 4:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            break;
                        case 5:
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            Controller.transform.gameObject.SetActive(true);
                            break;
                        case 6:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            BackGround.transform.gameObject.SetActive(true);
                            sphere.transform.gameObject.SetActive(false);
                            sphereIcon.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(false);
                            break;
                        case 11:
                            linkQuizManager.OptionClear = false;
                            tempInt=0;
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            break;
                        case 12:
                            PopUp.transform.gameObject.SetActive(false);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            Link.transform.gameObject.SetActive(false);
                            tempInt = 0;
                            break;
                        case 14:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(true);
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge1 = 0;
                                AppManage.Instance.Write_information();
                            }
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                        case 15:
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 17:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            break;
                        case 18:
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            break;
                        case 19:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            KakaoCanvas.transform.gameObject.SetActive(true);
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 1;
                            break;
                        case 20:
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            tempInt = 1;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge4 = 0;
                                AppManage.Instance.Write_information();
                            }
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                        case 21:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 22:
                        case 23:
                        case 24:
                        case 35:
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            break;
                        case 25:
                        case 36:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            KakaoCanvas.transform.gameObject.SetActive(true);
                            break;
                        case 26:
                            tempInt = 3;
                            break;
                        case 28:
                            tempInt = 4;
                            break;
                        case 29:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            break;
                        case 30:
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            KakaoCanvas.transform.gameObject.SetActive(true);
                            tempInt = 5;
                            break;
                        case 33:
                            if (AppManage.Instance.ClearStage1 == 0)
                            {
                                AppManage.Instance.badge5 = 0;
                                AppManage.Instance.Write_information();
                            }
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                        case 34:
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            KakaoCanvas.transform.gameObject.SetActive(false);
                            Destroy(Kakaotalk.scrollRect.content.GetChild(Kakaotalk.scrollRect.content.transform.childCount - 1).gameObject);
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 38:
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.text = string.Empty;
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 39:
                            WoodNote.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 40:
                            _button.transform.gameObject.SetActive(false);
                            break;
                        case 41:
                            _button.transform.gameObject.SetActive(true);
                            WoodNote.SetTrigger("WoodAnim");
                            break;
                        case 43:
                            WoodNote.transform.gameObject.SetActive(true);
                            WoodNote.SetTrigger("WoodOpen");
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            break;
                        case 1:
                            NPC.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            LinkCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.LinkClear = false;
                            tempInt = 0;
                            break;
                        case 2:
                            NPC.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            LinkCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.LinkClear = false;
                            tempInt = 0;
                            if (AppManage.Instance.ClearStage2 == 0)
                            {
                                AppManage.Instance.badge6 = 0;
                                AppManage.Instance.Write_information();
                            }
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                        case 3:
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 5:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 7:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 8:
                            tempInt = 2;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            break;
                        case 9:
                            tempInt = 2;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            NPC.transform.gameObject.SetActive(true);
                            Fire.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            if (AppManage.Instance.ClearStage2 == 0)
                            {
                                AppManage.Instance.badge7 = 0;
                                AppManage.Instance.Write_information();
                            }
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                        case 10:
                            Fire.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            BadgeCase.transform.gameObject.SetActive(true);
                            for (int k = 0; k < 7; k++)
                            {
                                switch (k)
                                {
                                    case 0:
                                        if (AppManage.Instance.badge1 == 1)
                                        {
                                            badge1.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 1:
                                        if (AppManage.Instance.badge2 == 1)
                                        {
                                            badge2.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 2:
                                        if (AppManage.Instance.badge3 == 1)
                                        {
                                            badge3.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 3:
                                        if (AppManage.Instance.badge4 == 1)
                                        {
                                            badge4.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 4:
                                        if (AppManage.Instance.badge5 == 1)
                                        {
                                            badge5.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 5:
                                        if (AppManage.Instance.badge6 == 1)
                                        {
                                            badge6.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                    case 6:
                                        if (AppManage.Instance.badge7 == 1)
                                        {
                                            badge7.transform.gameObject.SetActive(true);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 11:
                        case 16:
                        case 21:
                            str.text = string.Empty;
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 12:
                        case 19:
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 17:
                            NPC.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            break;
                        case 18:
                            TextBox.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            break;
                        case 22:
                            NPC.transform.gameObject.SetActive(true);
                            BadgeCase.transform.gameObject.SetActive(false);
                            badge1.transform.gameObject.SetActive(false);
                            badge2.transform.gameObject.SetActive(false);
                            badge3.transform.gameObject.SetActive(false);
                            badge4.transform.gameObject.SetActive(false);
                            badge5.transform.gameObject.SetActive(false);
                            badge6.transform.gameObject.SetActive(false);
                            badge7.transform.gameObject.SetActive(false);
                            break;
                    }
                    break;
                case 3:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 1:
                            Link.transform.gameObject.SetActive(false);
                            break;
                        case 2:
                            str.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            GameObject.Find("WebManager").SendMessage("initURL", "https://www.chungju.go.kr/tour/contents.do?key=1173");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "탄금대 홈페이지";
                            OptionCanvas.transform.gameObject.SetActive(false);
                            Gayagum.Stop();
                            break;
                        case 3:
                            tempInt = 0;
                            linkQuizManager.OptionClear = false;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            str.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            break;
                        case 4:
                            tempInt = 0;
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 1:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            str.text = string.Empty;
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            Gayagum.Stop();
                            break;
                        case 3:
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            tempInt = 0;
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.OptionClear = false;
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 0:
                            Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
                            sphere.transform.gameObject.SetActive(true);
                            sphereIcon.transform.gameObject.SetActive(true);
                            BackGround.transform.gameObject.SetActive(false);
                            Controller.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            break;
                        case 1:
                            TextBox.transform.gameObject.SetActive(true);
                            CharacterScroll.transform.gameObject.SetActive(true);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            Gayagum.Stop();
                            break;
                        case 3:
                            NPC.transform.gameObject.SetActive(true);
                            NPCText.transform.gameObject.SetActive(true);
                            NPCTextBox.transform.gameObject.SetActive(true);
                            NPCScroll.transform.gameObject.SetActive(true);
                            PopUpText.text = string.Empty;
                            PopUp.transform.gameObject.SetActive(false);
                            PopUpText.transform.gameObject.SetActive(false);
                            PopUpScroll.transform.gameObject.SetActive(false);
                            Link.transform.gameObject.SetActive(false);
                            LinkCanvas.transform.gameObject.SetActive(false);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.LinkClear = false;
                            linkQuizManager.OptionClear = false;
                            tempInt = 0;
                            break;
                        case 4:
                            PopUp.transform.gameObject.SetActive(true);
                            PopUpText.transform.gameObject.SetActive(true);
                            PopUpScroll.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            CharacterScroll.transform.gameObject.SetActive(false);
                            NPC.transform.gameObject.SetActive(false);
                            NPCText.text = string.Empty;
                            NPCText.transform.gameObject.SetActive(false);
                            NPCTextBox.transform.gameObject.SetActive(false);
                            NPCScroll.transform.gameObject.SetActive(false);
                            GameObject.Find("WebManager").SendMessage("initURL", "http://www.suanbo.or.kr/");
                            Link.transform.gameObject.SetActive(true);
                            tempText = GameObject.Find("Text").GetComponent<Text>();
                            tempText.text = "수안보온천 홈페이지";
                            LinkCanvas.transform.gameObject.SetActive(false);
                            OptionCanvas.transform.gameObject.SetActive(false);
                            linkQuizManager.LinkClear = false;
                            linkQuizManager.OptionClear = false;
                            tempInt = 0;
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 13:
                        case 14:
                        case 39:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 18:
                        case 19:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 30:
                        case 35:
                        case 36:
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 8:
                        case 11:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 20:
                        case 21:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 18:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 1:
                        case 2:
                            PopUpText.transform.gameObject.SetActive(true);
                            TextBox.transform.gameObject.SetActive(false);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 3:
                        case 4:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 4:
                    switch(count)
                    {
                        case 1:
                            PopUpText.transform.gameObject.SetActive(true);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        case 2:
                        case 3:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 2:
                        case 3:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, NPCText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, NPCText);
                            }
                            break;
                        case 4:
                            PopUpText.transform.gameObject.SetActive(true);
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, PopUpText);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, PopUpText);
                            }
                            break;
                        default:
                            text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                            if (text.Contains("###"))
                            {
                                string temp = text.Replace("###", AppManage.Instance.Name);
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                            }
                            else
                            {
                                GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 3:
                        case 4:
                        case 9:
                        case 12:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 27:
                        case 30:
                        case 32:
                        case 33:
                        case 35:
                        case 36:
                        case 37:
                        case 41:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 2:
                    switch (count)
                    {
                        case 1:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 2:
                        case 9:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 3:
                    switch (count)
                    {
                        case 3:
                        case 4:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 4:
                    switch (count)
                    {
                        case 2:
                        case 3:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 4:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                case 5:
                    switch (count)
                    {
                        case 2:
                        case 3:
                        case 5:
                            if (AppManage.Instance.Gender == 0)
                            {
                                CH1_nomal.gameObject.SetActive(false);
                                CH2_nomal.gameObject.SetActive(true);
                                CH3.gameObject.SetActive(false);
                            }
                            else
                            {
                                CH1_nomal.gameObject.SetActive(true);
                                CH2_nomal.gameObject.SetActive(false);
                                CH3.gameObject.SetActive(false);
                            }
                            break;
                        case 4:
                            break;
                        default:
                            CH1_nomal.gameObject.SetActive(false);
                            CH2_nomal.gameObject.SetActive(false);
                            CH3.gameObject.SetActive(true);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]] == "*")
            {
                if (AppManage.Instance.Gender == 0)
                {
                    CHS.SetTrigger("CH2Anim");
                }
                else
                {
                    CHS.SetTrigger("CH1Anim");
                }
            }
            else
            {
                if (currentStage == 1)
                {
                    CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                }
                else
                {
                    CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                }
            }
            switch (currentStage)
            {
                case 1:
                    switch (count)
                    {
                        case 18:
                        case 19:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 30:
                        case 35:
                        case 36:
                            AppManage.Instance.isComplite = true;
                            break;
                        default:
                            AppManage.Instance.isComplite = false;
                            break;
                    }
                    break;
                default:
                    AppManage.Instance.isComplite = false;
                    break;
            }
        }
        else
        {
            AppManage.Instance.isClicked = true;
        }
    }
    public void ExitCapture()
    {
        if (AppManage.Instance.isComplite)
        {
            if (currentStage == 1)
            {
                GameObject.Find("UIManager").SendMessage("CaptureOff");
                AppManage.Instance.isComplite = false;
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                count++;
                if (AppManage.Instance.Gender == 0)
                {
                    CH1_nomal.gameObject.SetActive(false);
                    CH2_nomal.gameObject.SetActive(true);
                    CH3.gameObject.SetActive(false);
                }
                else
                {
                    CH1_nomal.gameObject.SetActive(true);
                    CH2_nomal.gameObject.SetActive(false);
                    CH3.gameObject.SetActive(false);
                }
                text = Language_Text_Script1.Instance.scenario[currentStage].text[Language_Text_Script1.Instance.scenario[currentStage].Num[count]];
                if (text.Contains("###"))
                {
                    string temp = text.Replace("###", AppManage.Instance.Name);
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(temp, str);
                }
                else
                {
                    GameObject.Find("TypingManager").GetComponent<TypingManager>().TypingText(text, str);
                }

                if (Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]] == "*")
                {
                    if (AppManage.Instance.Gender == 0)
                    {
                        CHS.SetTrigger("CH2Anim");
                    }
                    else
                    {
                        CHS.SetTrigger("CH1Anim");
                    }
                }
                else
                {
                    CHS.SetTrigger(Language_Text_Script1.Instance.scenario[currentStage].anim[Language_Text_Script1.Instance.scenario[currentStage].Num[count]]);
                }
            }
            else if (currentStage == 2)
            {
                SceneManager.LoadScene("Stage2_End");
            }
        }
    }
}
