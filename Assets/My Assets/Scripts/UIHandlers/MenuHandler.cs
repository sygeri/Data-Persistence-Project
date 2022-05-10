using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public static MenuHandler Instance;
    public static bool initialized;
    public TMP_InputField input;
    public TextMeshProUGUI inputText;
    public GameObject errorMsg;
    public GameObject Login;
    public GameObject Menu;
    public TextMeshProUGUI welcomeText;

    public Serializer serializer;

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        serializer = GameObject.Find("Serializer").GetComponent<Serializer>();
        if (initialized)
        {
            ShowInitializedMenu();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);       
    }

    public void ShowSettings()
    {
        SceneManager.LoadScene(2);
    }

    public void ShowLeaderBoard()
    {
        SceneManager.LoadScene(3);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void ShowMenu()
    {
        if (inputText.text.Length < 3)
        {
            errorMsg.SetActive(true);
        }
        else
        {
            //Debug.Log(Application.persistentDataPath);
            Login.SetActive(false);
            Menu.SetActive(true);
            serializer.CurrentPlayerName = inputText.text;
            serializer.CurrentPlayerScore = 0;
            serializer.LoadHighScores();
            welcomeText.SetText("Welcome " + inputText.text + ", your best score: " + serializer.CurrentPlayerScore);
            initialized = true;
        }
    }

    public void ShowInitializedMenu()
    {
         Login.SetActive(false);
         Menu.SetActive(true);
         welcomeText.SetText("Welcome " + serializer.myHighScore.playerName + ", your best score: " + serializer.myHighScore.playerHighScore);
    }
}
