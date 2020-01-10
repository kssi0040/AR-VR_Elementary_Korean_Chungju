using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
public class LinkQuizManager : MonoBehaviour 
{
    [SerializeField]
    Text QTextPrefab;
    [SerializeField]
    Scrollbar QScrollbarPrefab;

    [Header("Link Quiz")]
    [SerializeField]
    Transform LinkQPanel;
    [SerializeField]
    Transform quizParent;
    [SerializeField]
    Transform ansParent;
    [SerializeField]
    Transform UILineRendererParent;
    [SerializeField]
    GameObject qPanelPrefab;
    [SerializeField]
    GameObject aPanelPrefab;
    [SerializeField]
    GameObject UILineRendererPrefab;

    [Header("Option Quiz")]
    [SerializeField]
    Transform OptionQPanel;
    [SerializeField]
    GameObject OAnsButPrefab;
    [SerializeField]
    Transform OptionAPanel;

    [SerializeField]
    private Text OnotifyMsg;

    [Header("Typing1 Quiz")]
    [SerializeField]
    Transform Typing1QPanel;
    [SerializeField]
    private Text T1notifyMsg;
    [SerializeField]
    private InputField T1InputField;
    [SerializeField]
    private Button T1InputBtn;

    [Header("Typing2 Quiz")]
    [SerializeField]
    Transform Typing2QPanel;
    [SerializeField]
    private GameObject T2AnsPanelPrefab;
    [SerializeField]
    private GameObject T2InputButPrefab;
    [SerializeField]
    private Transform Typing2APanel;

    private List<GameObject> T2ifList = new List<GameObject>();

    //Quiz_XML_Reader quiz_XML_Reader;
    Language_Quiz2 society_Qui2;

    public int LinkCount = 0;
    public bool LinkClear = false;
    public bool OptionClear = false;
    public bool Typing1Clear = false;
    public bool Typing2Clear = false;

    private List<GameObject> linesList = new List<GameObject>();

    void Awake()
    {
        //quiz_XML_Reader = GetComponent<Quiz_XML_Reader>();
        society_Qui2 = GameObject.Find("Quiz_XML_Reader").GetComponent<Language_Quiz2>();
    }

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        //if (society_Qui2.readCompleted)
        //{
        //    //stage숫자, id
        //    //Link Quiz
        //    //GenerateLinkQuiz(4, 0);
        //    //Option Quiz
        //    //GenerateOptionQuiz(0, 0);
        //    //Typing1 Quiz
        //    //GenerateTyping1Quiz(1, 0);
        //    //Typing2 Quiz
        //    //GenerateTyping2Quiz(3, 1);

        //    society_Qui2.readCompleted = false;
        //}
    }

    public void GenerateLinkQuiz(int stage, int id)
    {
        string Quiz = "";
        //OQText.text = string.Empty;
        foreach (Transform child in LinkQPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (RectTransform child in quizParent.GetComponentsInChildren<RectTransform>())
        {
            if (child.transform.gameObject.name == "QuesPanel")
            {
                continue;
            }
            else
            {
                Destroy(child.transform.gameObject);
            }
        }

        foreach (RectTransform child in ansParent.GetComponentsInChildren<RectTransform>())
        {
            if (child.transform.gameObject.name == "AnsPanel")
            {
                continue;
            }
            else
            {
                Destroy(child.transform.gameObject);
            }
        }

        Debug.Log("linesList.Count1: " + linesList.Count);

        LinkCount = 0;

        if (linesList.Count != 0)
        {
            foreach (GameObject item in linesList)
            {
                Destroy(item);
            }

            linesList.Clear();
        }

        Debug.Log("linesList.Count2: " + linesList.Count);

        for (int i = 0; i < society_Qui2.LinkQuizDict.Count; i++)
        {
            if (stage == society_Qui2.LinkQuizDict[i].stage)
            {
                if (id == society_Qui2.LinkQuizDict[i].id)
                {
                    //OQText.text = society_Qui2.OptionQuizDict[i].Text;

                    Quiz += society_Qui2.LinkQuizDict[i].Text + "\n";

                    if (Quiz.Contains("###"))
                    {
                        string temp = Quiz.Replace("###", AppManage.Instance.Name);
                        Quiz = temp;
                    }
                    if (society_Qui2.LinkQuizDict[i].Answer[0] != "*")
                    {
                        GameObject _qPrefab = Instantiate(qPanelPrefab);
                        //_qPrefab.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = i.ToString();
                        _qPrefab.transform.GetChild(1).GetChild(0).name = i.ToString();
                        _qPrefab.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = society_Qui2.LinkQuizDict[i].left[0];
                        _qPrefab.transform.SetParent(quizParent, false);

                        GameObject _aPrefab = Instantiate(aPanelPrefab);
                        //_aPrefab.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = society_Qui2.LinkQuizDict[i].Order[0];
                        _aPrefab.transform.GetChild(1).GetChild(0).GetComponent<Text>().transform.name = society_Qui2.LinkQuizDict[i].Order[0];
                        _aPrefab.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = society_Qui2.LinkQuizDict[i].right[0];
                        _aPrefab.transform.SetParent(ansParent, false);

                        GameObject _lrPrefab = Instantiate(UILineRendererPrefab, UILineRendererParent, false);
                        //_lrPrefab.transform.SetParent(UILineRendererParent, false);

                        linesList.Add(_lrPrefab);

                        LinkCount++;
                    }
                }
            }
        }

        GameObject.Find("UILineConnector").GetComponent<UILineConnector>().m_LineRenderer = new UILineRenderer[LinkCount];
        for (int i = 0; i < linesList.Count; i++)
        {
            GameObject.Find("UILineConnector").GetComponent<UILineConnector>().m_LineRenderer[i] = linesList[i].GetComponent<UILineRenderer>();
        }
        GameObject.Find("UILineConnector").GetComponent<UILineConnector>().isInitialized = true;
        GameObject.Find("UILineConnector").GetComponent<UILineConnector>().index = -1;
        GameObject.Find("UILineConnector").GetComponent<UILineConnector>().isDrawn = false;

       
        Text qText = Instantiate(QTextPrefab, LinkQPanel) as Text;
        qText.text = Quiz;
        Scrollbar qScrollbar = Instantiate(QScrollbarPrefab, LinkQPanel) as Scrollbar;

        LinkQPanel.GetComponent<ScrollRect>().content = qText.rectTransform;
        LinkQPanel.GetComponent<ScrollRect>().verticalScrollbar = qScrollbar;


        //Display Quiz
        //QText.text = Quiz;
    }

    public void GenerateOptionQuiz(int stage, int id)
    {
        //OQText.text = string.Empty;
        foreach (Transform child in OptionQPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        Text qText = Instantiate(QTextPrefab, OptionQPanel) as Text;
        Scrollbar qScrollbar = Instantiate(QScrollbarPrefab, OptionQPanel) as Scrollbar;

        OptionQPanel.GetComponent<ScrollRect>().content = qText.rectTransform;
        OptionQPanel.GetComponent<ScrollRect>().verticalScrollbar = qScrollbar;

        OnotifyMsg.text = string.Empty;
        foreach(Button child in OptionAPanel.GetComponentsInChildren<Button>())
        {
            Destroy(child.transform.gameObject);
        }
        Debug.Log("Count: " +OptionAPanel.transform.childCount);
        for (int i = 0; i < society_Qui2.OptionQuizDict.Count; i++)
        {
            if (stage == society_Qui2.OptionQuizDict[i].stage)
            {
                if (id == society_Qui2.OptionQuizDict[i].id)
                {
                    //OQText.text = society_Qui2.OptionQuizDict[i].Text;
                    qText.text = society_Qui2.OptionQuizDict[i].Text;

                    for (int j = 0; j < society_Qui2.OptionQuizDict[i].Order.Count; j++)
                    {
                        GameObject ansPrefab = Instantiate(OAnsButPrefab);
                        ansPrefab.transform.GetChild(0).GetComponent<Text>().text = society_Qui2.OptionQuizDict[i].Order[j];
                        ansPrefab.GetComponent<Button>().onClick.AddListener(delegate { OptionAnswerSelectionEvent(stage, id, ansPrefab.GetComponent<Button>()); });
                        ansPrefab.transform.SetParent(OptionAPanel, false);
                    }
                }
            }
        }
    }

    public void GenerateTyping1Quiz(int stage, int id)
    {
        foreach (Transform child in Typing1QPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        //T1QText.text = string.Empty;
        Text qText = Instantiate(QTextPrefab, Typing1QPanel) as Text;
        Scrollbar qScrollbar = Instantiate(QScrollbarPrefab, Typing1QPanel) as Scrollbar;

        Typing1QPanel.GetComponent<ScrollRect>().content = qText.rectTransform;
        Typing1QPanel.GetComponent<ScrollRect>().verticalScrollbar = qScrollbar;

        T1InputField.text = string.Empty;
        T1notifyMsg.text = string.Empty;
        for (int i = 0; i < society_Qui2.Typing1QuizDict.Count; i++)
        {
            if (stage == society_Qui2.Typing1QuizDict[i].stage)
            {
                if (id == society_Qui2.Typing1QuizDict[i].id)
                {
                    //T1QText.text = society_Qui2.Typing1QuizDict[i].Text;
                    qText.text = society_Qui2.Typing1QuizDict[i].Text;

                    T1InputBtn.onClick.AddListener(delegate { Typing1AnswerSelectionEvent(stage, id);  });
                }
            }
        }
    }

    public void GenerateTyping2Quiz(int stage, int id)
    {
        foreach (Transform child in Typing2QPanel)
        {
            GameObject.Destroy(child.gameObject);
        }

        string quiz = "";

        Text qText = Instantiate(QTextPrefab, Typing2QPanel) as Text;
        Scrollbar qScrollbar = Instantiate(QScrollbarPrefab, Typing2QPanel) as Scrollbar;

        Typing2QPanel.GetComponent<ScrollRect>().content = qText.rectTransform;
        Typing2QPanel.GetComponent<ScrollRect>().verticalScrollbar = qScrollbar;

        T2ifList.Clear();
        foreach (RectTransform child in Typing2APanel.GetComponentsInChildren<RectTransform>())
        {
            if(child.transform.gameObject.name== "Typing2APanel")
            {
                continue;
            }
            else
            {
                Destroy(child.transform.gameObject);
            }
        }

        for (int i = 0; i < society_Qui2.Typing2QuizDict.Count; i++)
        {
            if (stage == society_Qui2.Typing2QuizDict[i].stage)
            {
                if (id == society_Qui2.Typing2QuizDict[i].id)
                {
                    quiz += society_Qui2.Typing2QuizDict[i].Text + "\n";

                    if (society_Qui2.Typing2QuizDict[i].Answer[0] != "*")
                    {                        
                        GameObject ansPrefab = Instantiate(T2AnsPanelPrefab);

                        T2ifList.Add(ansPrefab);
                        
                        ansPrefab.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                        ansPrefab.transform.SetParent(Typing2APanel, false);
                    }
                }
            }
        }

        //T2QText.text = quiz;
        qText.text = quiz;

        GameObject IButPrefab = Instantiate(T2InputButPrefab);
        IButPrefab.GetComponent<Button>().onClick.AddListener(delegate { Typing2AnswerSelectionEvent(stage, id, T2ifList); });
        IButPrefab.transform.SetParent(Typing2APanel, false);
    }

    private void OptionAnswerSelectionEvent(int stage, int id, Button but)
    {
        int count = 0;
        for (int i = 0; i < society_Qui2.OptionQuizDict.Count; i++)
        {
            if (stage == society_Qui2.OptionQuizDict[i].stage)
            {
                if (id == society_Qui2.OptionQuizDict[i].id)
                {
                    if (society_Qui2.OptionQuizDict[i].Answer.Count > 1)
                    {
                        for (int j = 0; j < society_Qui2.OptionQuizDict[i].Answer.Count; j++)
                        {
                            if (but.transform.GetChild(0).GetComponent<Text>().text == society_Qui2.OptionQuizDict[i].Answer[j])
                            {
                                but.GetComponent<Image>().color = Color.green;

                                StartCoroutine(INotifyMsg(OnotifyMsg, "o", Color.green));

                                but.GetComponent<Button>().interactable = false;

                                count++;
                                if (society_Qui2.OptionQuizDict[stage].Answer.Count == count)
                                {
                                    OptionClear = true;
                                }
                                break;
                            }
                            else
                            {
                                StartCoroutine(INotifyMsg(OnotifyMsg, "x", Color.red));

                            }
                        }
                    }
                    else
                    {
                        if (but.transform.GetChild(0).GetComponent<Text>().text == society_Qui2.OptionQuizDict[i].Answer[0])
                        {
                            but.GetComponent<Image>().color = Color.green;

                            StartCoroutine(INotifyMsg(OnotifyMsg, "o", Color.green));

                            but.GetComponent<Button>().interactable = false;

                            OptionClear = true;
                           
                        }
                        else
                        {
                            StartCoroutine(INotifyMsg(OnotifyMsg, "x", Color.red));
                        }

                    }
                }
            }
        }
       
    }

    public void Typing1AnswerSelectionEvent(int stage, int id)
    {
        for (int i = 0; i < society_Qui2.Typing1QuizDict.Count; i++)
        {
            Debug.Log("Typing1 Event " + society_Qui2.Typing1QuizDict[i].Answer[0]);
     
            if (stage == society_Qui2.Typing1QuizDict[i].stage)
            {
                
                if (id == society_Qui2.Typing1QuizDict[i].id)
                {

                    if (T1InputField.text == society_Qui2.Typing1QuizDict[i].Answer[0])
                    {
                        Debug.Log("Typing1 Event " + society_Qui2.Typing1QuizDict[i].Answer[0]);
     
                        StartCoroutine(INotifyMsg(T1notifyMsg, "o", Color.green));
                        Typing1Clear = true;
                    }
                    else
                    {
                        StartCoroutine(INotifyMsg(T1notifyMsg, "x", Color.red));
                    }
                }
            }
        }
    }

    public void Typing2AnswerSelectionEvent(int stage, int id, List<GameObject> ifList)
    {
        for (int i = 0; i < ifList.Count; i++)
        {

            if (ifList[i].transform.GetChild(1).GetComponent<InputField>().text == society_Qui2.Typing2QuizDict[i+1].Answer[0])
            {
                ifList[i].transform.GetChild(2).GetComponent<Text>().text = "o";
                ifList[i].transform.GetChild(2).GetComponent<Text>().color = Color.green;

                Typing2Clear = true;
            }
            else
            {
                ifList[i].transform.GetChild(2).GetComponent<Text>().text = "x";
                ifList[i].transform.GetChild(2).GetComponent<Text>().color = Color.red;
            }
        }
       
    }

    IEnumerator INotifyMsg(Text uiText, string msg, Color color)
    {
        uiText.text = msg;
        uiText.color = color;

        uiText.enabled = true;
        yield return new WaitForSeconds(1f);
        uiText.enabled = false;
    }

}
