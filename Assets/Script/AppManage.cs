using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AppManage : MonoBehaviour
{
    private const string ACCESS_FINE_LOCATION = "android.permission.ACCESS_FINE_LOCATION";
    private const string WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";
    private const string CAMERA = "android.permission.CAMERA";

    private static AppManage instance = null;
    private static readonly object padlock = new object();

    private AppManage()
    {

    }

    public static AppManage Instance
    {
        get
        {
            lock(padlock)
            {
                if(instance==null)
                {
                    instance = new AppManage();
                }
                return instance;
            }
        }
    }

    string fileName = "Player_information.xml";
    public string filePath;
    public int Gender = -1;//성별, 남 = 0, 여 =1
    public string Name = "";
    //스테이지를 클리어했는가? 선형 = 1,2 비선형 = 3,4,5    값이 = 1일 경우 Clear
    public int ClearStage1=0;
    public int ClearStage2=0;
    public int ClearStage3=0;
    public int ClearStage4=0;
    public int ClearStage5=0;
    //스테이지를 전부 클리어 했는가 true 1, false =0
    public int AllClear = 0;
    public int badge1 = 0;
    public int badge2 = 0;
    public int badge3 = 0;
    public int badge4 = 0;
    public int badge5 = 0;
    public int badge6 = 0;
    public int badge7 = 0;
    public bool isComplite;
    public bool isClicked;
    public bool isloading = false;
    public bool isExit = false;
    public Transform Exit;
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(Exit.gameObject);
        //PlayerPrefs.DeleteAll();
#if UNITY_ANDROID
        filePath = Application.persistentDataPath + "/" + fileName;

#elif UNITY_STANDALONE_WIN
        filePath = Application.dataPath + "/" + fileName;
#endif

    }
    // Use this for initialization
    void Start()
    {
        isloading = true;
        isExit = false;
        Debug.Log(filePath);

        AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions(WRITE_EXTERNAL_STORAGE, CAMERA, ACCESS_FINE_LOCATION);

        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == AndroidRuntimePermissions.Permission.Granted)
            {
                Debug.Log("We have all the permissions!");
            }
            else
            {
                Debug.Log("Some permission are not granted...");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isloading == true)
        {
            Checkingfile();
            isloading = false;
        }	
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name!="Homepage")
            {
                isExit = true;
            }
        }
        if (isExit)
        {
            Exit.transform.gameObject.SetActive(true);
        }
        else
        {
            Exit.transform.gameObject.SetActive(false);
        }
	}

    public void Checkingfile()
    {
        //if ((System.IO.File.Exists(filePath)))
        //{
        //    Debug.Log("Checking file Loading");
        //    LoadInformation();//사용자 정보 로드
        //}
        //else
        //{
        //    Debug.Log("Checking file not Loading");
        //    return;
        //}
        if (PlayerPrefs.HasKey("Name"))
        {
            LoadInformation();
        }
        else
        {
            return;
        }
    }

    public void SelectMan()
    {
        Gender = 0;
    }

    public void SelectWoman()
    {
        Gender = 1;
    }

    public void Write_name(InputField ip)
    {
        Name = ip.text;
    }

    public void EndStage(int stage)
    {
        switch(stage)
        {
            case 1:
                ClearStage1 = 1;
                break;
            case 2:
                ClearStage2 = 1;
                break;
            case 3:
                ClearStage3 = 1;
                break;
            case 4:
                ClearStage4 = 1;
                break;
            case 5:
                ClearStage5 = 1;
                break;
            default:
                break;
        }
        isComplite = false;
        Write_information();
        //LoadInformation();
        if (ClearStage1 == 1 && ClearStage2 == 1 && ClearStage3 == 1 && ClearStage4 == 1 && ClearStage5 == 1)
        {
            if (AllClear == 0)
            {
                AllClear = 1;
                Write_information();
                SceneManager.LoadScene("Epilogue");
            }
            else
            {
                SceneManager.LoadScene("SelectMap");
            }
        }
        else
        {
            SceneManager.LoadScene("SelectMap");
        }
    }

    //public void Write_information()
    //{
    //    Debug.Log("Write_information!!!!!!!!!!!!!!!!!!!");
    //    XmlDocument document = new XmlDocument();
    //    XmlDeclaration xmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
    //    document.AppendChild(xmlDeclaration);

    //    XmlElement infoElement = document.CreateElement("User_Information");
    //    document.AppendChild(infoElement);

    //    XmlElement userElement = (XmlElement)infoElement.AppendChild(document.CreateElement("Field"));
    //    userElement.SetAttribute("Name", Name);
    //    userElement.SetAttribute("Gender", Gender.ToString());
    //    userElement.SetAttribute("Clear_Stage1", ClearStage1.ToString());
    //    userElement.SetAttribute("Clear_Stage2", ClearStage2.ToString());
    //    userElement.SetAttribute("Clear_Stage3", ClearStage3.ToString());
    //    userElement.SetAttribute("Clear_Stage4", ClearStage4.ToString());
    //    userElement.SetAttribute("Clear_Stage5", ClearStage5.ToString());
    //    userElement.SetAttribute("AllClear", AllClear.ToString());
    //    userElement.SetAttribute("badge1", badge1.ToString());
    //    userElement.SetAttribute("badge2", badge2.ToString());
    //    userElement.SetAttribute("badge3", badge3.ToString());
    //    userElement.SetAttribute("badge4", badge4.ToString());
    //    userElement.SetAttribute("badge5", badge5.ToString());
    //    userElement.SetAttribute("badge6", badge6.ToString());
    //    userElement.SetAttribute("badge7", badge7.ToString());
    //    Debug.Log("저장 가즈아~");

    //    document.Save(filePath);

    //}
    public void Write_information()
    {
        PlayerPrefs.SetString("Name", Name);
        PlayerPrefs.SetInt("Gender", Gender);
        PlayerPrefs.SetInt("Clear_Stage1", ClearStage1);
        PlayerPrefs.SetInt("Clear_Stage2", ClearStage2);
        PlayerPrefs.SetInt("Clear_Stage3", ClearStage3);
        PlayerPrefs.SetInt("Clear_Stage4", ClearStage4);
        PlayerPrefs.SetInt("Clear_Stage5", ClearStage5);
        PlayerPrefs.SetInt("AllClear", AllClear);
        PlayerPrefs.SetInt("badge1", badge1);
        PlayerPrefs.SetInt("badge2", badge2);
        PlayerPrefs.SetInt("badge3", badge3);
        PlayerPrefs.SetInt("badge4", badge4);
        PlayerPrefs.SetInt("badge5", badge5);
        PlayerPrefs.SetInt("badge6", badge6);
        PlayerPrefs.SetInt("badge7", badge7);
    }
    //    public void LoadInformation()
    //    {
    //        //StartCoroutine(Process());
    //        string strPath = string.Empty;

    //#if UNITY_STANDALONE_WIN 
    //        strPath += ("file:///");
    //        strPath += (Application.dataPath + "/" + fileName);
    //#elif UNITY_ANDROID
    //        strPath = Application.persistentDataPath + "/" + fileName;
    //#endif
    //        interpret(strPath);
    //    }

    public void LoadInformation()
    {
        Name = PlayerPrefs.GetString("Name");
        Gender = PlayerPrefs.GetInt("Gender");
        ClearStage1 = PlayerPrefs.GetInt("Clear_Stage1");
        ClearStage2 = PlayerPrefs.GetInt("Clear_Stage2");
        ClearStage3 = PlayerPrefs.GetInt("Clear_Stage3");
        ClearStage4 = PlayerPrefs.GetInt("Clear_Stage4");
        ClearStage5 = PlayerPrefs.GetInt("Clear_Stage5");
        AllClear = PlayerPrefs.GetInt("AllClear");
        badge1 = PlayerPrefs.GetInt("badge1");
        badge2 = PlayerPrefs.GetInt("badge2");
        badge3 = PlayerPrefs.GetInt("badge3");
        badge4 = PlayerPrefs.GetInt("badge4");
        badge5 = PlayerPrefs.GetInt("badge5");
        badge6 = PlayerPrefs.GetInt("badge6");
        badge7 = PlayerPrefs.GetInt("badge7");

        GameObject.Find("InputManager").SendMessage("initInputData");
    }
    IEnumerator Process()
    {
        
        string strPath = string.Empty;
        //strPath = "jar:file://"+Application.persistentDataPath+"!/assets/"+fileName;

#if(UNITY_EDITOR||UNITY_STANDALONE_WIN)
        strPath += ("file:///");
        strPath += (Application.dataPath + "/" + fileName);
#elif UNITY_ANDROID
        strPath = "jar:file://" + Application.persistentDataPath + "!/assets/" + fileName;
#endif
        Debug.Log(strPath + " KBY3");
        WWW www = new WWW(strPath);
        Debug.Log(www.text+" KBY4");
        yield return www;
        if (www.isDone)
        {
            interpret(www.text);
        }
        
    }

    private void interpret(string _strSource)
    {
        StringReader stringReader = new StringReader(_strSource);

        stringReader.Read();

        XmlNodeList xmlNodeList = null;

        XmlDocument xmlDoc = new XmlDocument();

        //xmlDoc.LoadXml(stringReader.ReadToEnd());
        xmlDoc.Load(_strSource);
        xmlNodeList = xmlDoc.SelectNodes("User_Information");


        foreach (XmlNode node in xmlNodeList)
        {
            if (node.Name.Equals("User_Information") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    Name = child.Attributes.GetNamedItem("Name").Value;
                    Gender = int.Parse(child.Attributes.GetNamedItem("Gender").Value);
                    ClearStage1 = int.Parse(child.Attributes.GetNamedItem("Clear_Stage1").Value);
                    ClearStage2 = int.Parse(child.Attributes.GetNamedItem("Clear_Stage2").Value);
                    ClearStage3 = int.Parse(child.Attributes.GetNamedItem("Clear_Stage3").Value);
                    ClearStage4 = int.Parse(child.Attributes.GetNamedItem("Clear_Stage4").Value);
                    ClearStage5 = int.Parse(child.Attributes.GetNamedItem("Clear_Stage5").Value);
                    AllClear = int.Parse(child.Attributes.GetNamedItem("AllClear").Value);
                    badge1 = int.Parse(child.Attributes.GetNamedItem("badge1").Value);
                    badge2 = int.Parse(child.Attributes.GetNamedItem("badge2").Value);
                    badge3 = int.Parse(child.Attributes.GetNamedItem("badge3").Value);
                    badge4 = int.Parse(child.Attributes.GetNamedItem("badge4").Value);
                    badge5 = int.Parse(child.Attributes.GetNamedItem("badge5").Value);
                    badge6 = int.Parse(child.Attributes.GetNamedItem("badge6").Value);
                    badge7 = int.Parse(child.Attributes.GetNamedItem("badge7").Value);

                    Debug.Log("Field Name: " + Name);
                    Debug.Log("Field Gender: " + Gender.ToString());
                    Debug.Log("Field Clear_Stage1: " + ClearStage1.ToString());
                    Debug.Log("Field Clear_Stage2: " + ClearStage2.ToString());
                    Debug.Log("Field Clear_Stage3: " + ClearStage3.ToString());
                    Debug.Log("Field Clear_Stage4: " + ClearStage4.ToString());
                    Debug.Log("Field Clear_Stage5: " + ClearStage5.ToString());

                    GameObject.Find("InputManager").SendMessage("initInputData");
                }
            }
        }
    }
    public void OnExitPopup()
    {
        isExit = true;
    }
    public void ExitApp()
    {
        Application.Quit();
    }
    public void ExitCancle()
    {
        if (SceneManager.GetActiveScene().name == "Epilogue")
        {
            isExit = false;
            SceneManager.LoadScene("SelectMap");
        }
        else
        {
            isExit = false;
        }
    }
}
